using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    
    public class TestTrackLinear : MonoBehaviour
    {
        [SerializeField] private RaceTrackLinear m_RaceTrackLinear;

        [Range(-50.0f, 50.0f)]
        [SerializeField] private float m_Speed = 0.0f;

        private float m_Distance = 0.1f;


        private void Update()
        {
            Move();
        }
        private void Move()
        {
            m_Distance += m_Speed * Time.deltaTime;
            if (m_Distance >= m_RaceTrackLinear.GetTrackLength())
            {
                m_Distance = default;
            }
            else if(m_Distance < 0)
            {
                m_Distance = m_RaceTrackLinear.GetTrackLength();
            }
            transform.position = m_RaceTrackLinear.GetPosition(m_Distance);
            transform.forward = m_RaceTrackLinear.GetDirection(m_Distance);
        }
    }
}
