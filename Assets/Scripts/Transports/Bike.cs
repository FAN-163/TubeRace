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
        
        [Range(0.0f, 10.0f)]
        public float thrust;

        [Range(0.0f, 100.0f)]
        public float agility;
        public float maxSpeed;

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
        /// Управление газом байка. Нормализованное. от -1 до +1.
        /// </summary>
        private float m_ForwardThrustAxis;

        /// <summary>
        /// Устанавливать значение педали газа
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            m_ForwardThrustAxis = val;
        }

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованноею От -1 до +1.
        /// </summary>
        private float m_HorizontalThrustAxis;

        public void SetHorizontalThrustAxis(float val)
        {
            m_HorizontalThrustAxis = val;
        }

        [SerializeField] private RaceTrack m_Track;

        private float m_Distance;
        private float m_Velocity;
        private float m_RollAngle;
        private float m_RollAngleModifier = 1.0f;

        private void Update()
        {
            UpdateBikePhysics();
        }
        


        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            float dv = dt * m_ForwardThrustAxis * m_BikeParametersInitial.thrust;
            
            m_Velocity += dv;

            m_Velocity = Mathf.Clamp(m_Velocity, -m_BikeParametersInitial.maxSpeed, m_BikeParametersInitial.maxSpeed);

            float dS = m_Velocity * dt;
            
            //collision
            if(Physics.Raycast(transform.position, transform.forward, dS))
            {
                m_Velocity = -m_Velocity * m_BikeParametersInitial.collisionBounceFactor;
                dS = m_Velocity * dt;
            }

            m_Distance += dS;

            m_Velocity += -m_Velocity * m_BikeParametersInitial.linearDrag * dt;

            if (m_Distance < 0)
                m_Distance = 0;

            m_RollAngle += m_BikeParametersInitial.agility * dt * m_HorizontalThrustAxis;
            m_RollAngle += -m_RollAngle * m_BikeParametersInitial.rollDrag * dt;


            Vector3 bikePos = m_Track.GetPosition(m_Distance);
            Vector3 bikeDir = m_Track.GetDirection(m_Distance);

            
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);

            //дает возможность байку делать круг по трубе и спускаться с той стороны на которой находится
            if (q.z >= m_RollAngleModifier || q.z <= -m_RollAngleModifier)
            {
                m_RollAngle = -m_RollAngle;
            }


            Vector3 trackOffset = q * (Vector3.up * m_Track.Radius);

            transform.position = bikePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);
        }
    }
}

