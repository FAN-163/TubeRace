using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private RaceTrack m_Track;
        [SerializeField] private float m_RollAngle;
        [SerializeField] private float m_Distance;

        [Range(-100.0f, 100.0f)]
        [SerializeField] private float m_RollSpeed;

        [Range(0.0f, 20.0f)]
        [SerializeField] private float m_RadiusModifier;

        [SerializeField] private bool OnItsAxis;

        private float m_MaxRollAngle = 360f;

        private void OnValidate()
        {
            SetObstaclePosition();
        }
        private void Update()
        {
            SetObstaclePosition();
        }

        private void SetObstaclePosition()
        {
            float dt = Time.deltaTime;

            Vector3 obstaclePos = m_Track.GetPosition(m_Distance);
            Vector3 obstacleDir = m_Track.GetDirection(m_Distance);

            m_RollAngle += m_RollSpeed * dt;

            //обнуляет значение когда оно больше 360 или меньше -360
            //что бы не росло/уменьшалось до максимальных/минимальных значений float
            if(m_RollAngle >= m_MaxRollAngle || m_RollAngle <= -m_MaxRollAngle)
            {
                m_RollAngle = 0.0f;
            }
            
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (m_RadiusModifier * m_Track.Radius));

            
           
            //Вращение вокруг трека или вокруг собственной оси
            if (!OnItsAxis)
            {
                transform.position = obstaclePos - trackOffset;
                transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
            }
            else
            {
                transform.Rotate(transform.forward * m_RollSpeed * dt);
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 centerlinePos = m_Track.GetPosition(m_Distance);
            Gizmos.DrawSphere(centerlinePos, m_Track.Radius);
        }
    }
}
