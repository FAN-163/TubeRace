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
            Vector3 trackOffset = q * (Vector3.up * (0));

            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);

        }

        private void Update()
        {
            UpdateBikes();
        }

        private void UpdateBikes()
        {
            foreach (var bikeObject in GameObject.FindGameObjectsWithTag(Bike.Tag))
            {
                Bike bike = bikeObject.GetComponent<Bike>();

                float prev = bike.GetPrevDistance();
                float curr = bike.GetDistance();

                if(prev < m_Distance && curr > m_Distance)
                {
                    //limit angles

                    // bike picks powerup
                    OnPickedByBike(bike);
                }
            }
        }

        public abstract void OnPickedByBike(Bike bike);
    }
}

