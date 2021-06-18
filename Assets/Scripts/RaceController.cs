using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private int m_Max_Laps;

        // gameplay mode
        // race - laps
        // time
        public enum RaceMode
        {
            Laps,
            Time,
            LastStanding
        }

        [SerializeField] private RaceMode m_RaceMode;

        [SerializeField] private UnityEvent m_EventRaceStart;
        [SerializeField] private UnityEvent m_EventRaceFinished;

        [SerializeField] private Bike[] m_Bikes;

        [SerializeField] private int m_CountdownTimer;

        public int CountdownTimer => m_CountdownTimer;

        private float m_CountTimer;
        public float CountTimer => m_CountTimer;

        public bool IsRaceActive { get; private set; }

        public void StartRace()
        {
            IsRaceActive = true;

            m_CountTimer = m_CountdownTimer;

            
        }

        public void EndRace()
        {
            IsRaceActive = false;
        }

        private void Start()
        {
            StartRace();
        }

        private void Update()
        {
            if (!IsRaceActive)
                return;

            UpdateRacePrestart();
        }

        private void UpdateRacePrestart()
        {
            if (m_CountTimer > 0)
            {
                m_CountTimer -= Time.deltaTime;

                if (m_CountTimer <= 0)
                {
                    foreach (var e in m_Bikes)
                        e.IsMovementControlIsActive = true;
                }
            }
        }
    }
}
