using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public abstract class Powerup : MonoBehaviour
    {
        [SerializeField] private RaceTrack m_Track;
        [SerializeField] private float m_RollAngle;
        [SerializeField] private float m_Distance;

        [Range(0.0f, 20.0f)]
        [SerializeField] private float m_RadiusModifier;


        private float m_StartPoint;
        private float m_EndPoint;
        private float m_OffsetAngle;
        private float m_Test;

        private void Awake()
        {
            CalculateSize();
        }

        private void OnValidate()
        {
            SetPowerPosition();
        }

        private void SetPowerPosition()
        {
            float dt = Time.deltaTime;

            Vector3 obstaclePos = m_Track.GetPosition(m_Distance);
            Vector3 obstacleDir = m_Track.GetDirection(m_Distance);

            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (m_RadiusModifier * m_Track.Radius));

            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);

        }

        private void Update()
        {
            UpdateBikes();
//            Debug.Log(m_OffsetAngle);
        }

        private void UpdateBikes()
        {
            foreach (var bikeObject in GameObject.FindGameObjectsWithTag(Bike.Tag))
            {
                Bike bike = bikeObject.GetComponent<Bike>();

                float prev = bike.GetPrevDistance();
                float curr = bike.GetDistance();

                // изменено дл€ бонусов/штрафов имеющих длительное действие
                if (prev < m_EndPoint && curr > m_StartPoint)
                {
                    // limit angles
                    // ƒумал сделать установку offset через инспектор, но потом решил
                    // сделать так, чтобы углы зависели от размера puwerup
                    if ((m_RollAngle - m_OffsetAngle) < bike.GetRollAngle() &&
                        (m_RollAngle + m_OffsetAngle) > bike.GetRollAngle())
                    {
                        // bike picks powerup
                        OnPickedByBike(bike);
                    }
                }
            }
        }

        /// <summary>
        /// определ€ем начальную и конечную точку в которой будет действовать бонус или штраф
        /// и угол отклонени€ от центра поверапа,
        /// </summary>
        protected void CalculateSize()
        {
            var bounds = GetComponentInChildren<Renderer>().bounds.size;

            m_StartPoint = m_Distance - bounds.z / 2;
            m_EndPoint = m_Distance + bounds.z / 2;

            m_OffsetAngle = bounds.x;
        }

        public abstract void OnPickedByBike(Bike bike);
    }
}

