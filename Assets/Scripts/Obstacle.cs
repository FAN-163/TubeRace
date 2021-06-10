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

        [Range(-200.0f, 200.0f)]
        [SerializeField] private float m_Velocity;

        [Range(0.0f, 20.0f)]
        [SerializeField] private float m_RadiusModifier;

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
            m_RollAngle %= 360;

            float dt = Time.deltaTime;

            Vector3 obstaclePos = m_Track.GetPosition(m_Distance);
            Vector3 obstacleDir = m_Track.GetDirection(m_Distance);

            float m_AngleSpeed = m_Velocity * dt;
            m_RollAngle += m_AngleSpeed;
            
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (m_RadiusModifier * m_Track.Radius));
     
            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 centerlinePos = m_Track.GetPosition(m_Distance);
            Gizmos.DrawSphere(centerlinePos, m_Track.Radius);
        }
    }
}
