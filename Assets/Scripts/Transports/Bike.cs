using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;
        
        [Range(0.0f, 100.0f)]
        public float thrust;

        public float afterburnerThrust;

        [Range(0.0f, 100.0f)]
        public float agility;
        public float maxSpeed;

        public float afterburnerMaxSpeedBonus;

        public float afterburnerCoolSpeed;
        public float afterburnerHeatGeneration; //per second
        public float afterburnerMaxHeat;

        [Range(0.0f, 1.0f)]
        public float linearDrag;

        [Range(0.0f, 0.5f)]
        public float rollDrag;

        [Range(0.0f, 1.0f)]
        public float collisionBounceFactor;

        public bool afteburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters m_BikeParametersInitial;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController m_VisualController;

        /// <summary>
        /// ���������� ����� �����. ���������������. �� -1 �� +1.
        /// </summary>
        private float m_ForwardThrustAxis;

        /// <summary>
        /// ������������� �������� ������ ����
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            m_ForwardThrustAxis = val;
        }

        /// <summary>
        /// ���������� ����������� ����� � ������. ���������������� �� -1 �� +1.
        /// </summary>
        private float m_HorizontalThrustAxis;

        /// <summary>
        /// ���/���� �� ����������
        /// </summary>
        public bool EnableAfterburner { get; set; }

        public void SetHorizontalThrustAxis(float val)
        {
            m_HorizontalThrustAxis = val;
        }

        [SerializeField] private RaceTrack m_Track;

        public RaceTrack GetTrack()
        {
            return m_Track;
        }

        private float m_Distance;
        private float m_Velocity;
        private float m_RollAngle;
        private float m_RollAngleModifier = 1.0f;


        public float GetDistance()
        {
            return m_Distance;
        }
        public float GetVelocity()
        {
            return m_Velocity;
        }
        public float GetRollAngle()
        {
            return m_RollAngle;
        }
        

        private void Update()
        {
            UpdateBikePhysics();
            UpdateAfterburnerHeat();
        }
        private float m_AfterburnerHeat;
        
        public float GetNormalizedHeat()
        {
            if(m_BikeParametersInitial.afterburnerMaxHeat > 0)
                return m_AfterburnerHeat / m_BikeParametersInitial.afterburnerMaxHeat;

            return 0;
        }

        public void CoolAfterburner()
        {
            m_AfterburnerHeat = 0;
        }

        private void UpdateAfterburnerHeat()
        {
            //calc heat dissipation
            m_AfterburnerHeat -= m_BikeParametersInitial.afterburnerCoolSpeed * Time.deltaTime;

            if(m_AfterburnerHeat < 0 )
               m_AfterburnerHeat = 0;

            // Check max heat?
            //***
        }
        
        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            

            float FthrustMax = m_BikeParametersInitial.thrust;
            float Vmax = m_BikeParametersInitial.maxSpeed;
            float F = m_ForwardThrustAxis * m_BikeParametersInitial.thrust ;

            if (EnableAfterburner && ConsumeFuelForAfterburner(1.0f * Time.deltaTime))
            {
                m_AfterburnerHeat += m_BikeParametersInitial.afterburnerHeatGeneration * Time.deltaTime;

                F += m_BikeParametersInitial.afterburnerThrust;

                Vmax += m_BikeParametersInitial.afterburnerMaxSpeedBonus;
                FthrustMax += m_BikeParametersInitial.afterburnerThrust;
            }

            //Drag
            F += -m_Velocity * (FthrustMax / Vmax);

            float dv = dt * F;
            ;

            // F=ma
            // F_thrust
            // f_drag
            // F = F_thrust -V * K_drag
            // F_drag = -V * K_drag

            // 0 = F_thrust - Vmax * K_drag
            // V * K_drag = F_thrust 
            // K_drag = F_thrust / Vmax

            m_Velocity += dv;

            float dS = m_Velocity * dt;
            
            //collision
            if(Physics.Raycast(transform.position, transform.forward, dS))
            {
                m_Velocity = -m_Velocity * m_BikeParametersInitial.collisionBounceFactor;
                dS = m_Velocity * dt;
            }

            m_PrevDistance = m_Distance;

            m_Distance += dS;

            if (m_Distance < 0)
                m_Distance = 0;

            m_RollAngle += m_BikeParametersInitial.agility * dt * m_HorizontalThrustAxis;
            m_RollAngle += -m_RollAngle * m_BikeParametersInitial.rollDrag * dt;


            Vector3 bikePos = m_Track.GetPosition(m_Distance);
            Vector3 bikeDir = m_Track.GetDirection(m_Distance);

            
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);

            if (q.z >= m_RollAngleModifier || q.z <= -m_RollAngleModifier)
            {
                m_RollAngle = -m_RollAngle;
            }


            Vector3 trackOffset = q * (Vector3.up * m_Track.Radius);

            transform.position = bikePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);
        }


        private float m_PrevDistance;

        public float GetPrevDistance()
        {
            return m_PrevDistance;
        }

        // 0 - 100
        private float m_Fuel;

        public float GetFuel()
        {
            return m_Fuel;
        }

        public void AddFuel(float amount)
        {
            m_Fuel += amount;

            m_Fuel = Mathf.Clamp(m_Fuel, 0, 100);
        }

        public bool ConsumeFuelForAfterburner(float amount)
        {

            if (m_Fuel <= amount)
                return false;

            m_Fuel -= amount;

            return true;
        }

        public static readonly string Tag = "Bike";
    }
}

