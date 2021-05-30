using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{

    /// <summary>
    /// Класс линейного трека
    /// </summary>
    public class RaceTrackLinear : RaceTrack
    {
        [Header("Linear track properties")]
        [SerializeField] private Transform m_Start;
        [SerializeField] private Transform m_End;

        public override Vector3 GetDirection(float distance)
        {
            distance = Mathf.Clamp(distance, 0, GetTrackLength());

            return (m_End.position - m_Start.position).normalized;
        }

        public override Vector3 GetPosition(float distance)
        {
            distance = Mathf.Clamp(distance, 0, GetTrackLength());

            Vector3 direction = m_End.position - m_Start.position;
            direction = direction.normalized;

            return m_Start.position + direction * distance;
        }

        public override float GetTrackLength()
        {
            Vector3 direction = m_End.position - m_Start.position;
            return direction.magnitude;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(m_Start.position, m_End.position);
        }

        #region Test

        [SerializeField] private float m_TestDistance;
        [SerializeField] private Transform m_TestObject;

        private void OnValidate()
        {
            m_TestObject.position = GetPosition(m_TestDistance);
            m_TestObject.forward = GetDirection(m_TestDistance);
        }

        #endregion
    }
}
