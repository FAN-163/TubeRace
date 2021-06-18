using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class RaceConditionLaps : RaceCondition
    {
        [SerializeField] private RaceController m_RaceController;

        private void Update()
        {
            if (!m_RaceController.IsRaceActive && IsTriggered)
                return;

            Bike[] bikes = m_RaceController.Bikes;

            foreach(var bike in bikes)
            {
                int laps = (int)(bike.GetDistance() / bike.GetTrack().GetTrackLength());

                if (laps < m_RaceController.MaxLaps)
                    return;
            }

            IsTriggered = true;
        }
    }
}
