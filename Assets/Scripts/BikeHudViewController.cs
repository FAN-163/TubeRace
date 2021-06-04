using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race.UI
{

    public class BikeHudViewController : MonoBehaviour
    {
        [SerializeField] private Text m_LabelSpeed;
        [SerializeField] private Text m_LabelDistance;
        [SerializeField] private Text m_LabelRollAngle;
        [SerializeField] private Text m_LabelLerpNumber;

        [SerializeField] private Bike m_Bike;

        private void Update()
        {
            int velocity = (int)m_Bike.GetVelocity();
            m_LabelSpeed.text = "Speed: " + velocity.ToString() + " m/s";

            int distance = (int)m_Bike.GetDistance();
            m_LabelDistance.text = "Distance: " + distance.ToString() + " m";

            int roll = (int)(m_Bike.GetRollAngle());
            m_LabelRollAngle.text = "Angle: " + roll.ToString() + " deg";

            int laps = (int)(m_Bike.GetDistance()/m_Bike.GetTrack().GetTrackLength());
            m_LabelLerpNumber.text = "Laps: " + (laps + 1).ToString();
        }
    }
}
