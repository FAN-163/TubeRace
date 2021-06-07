using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Игрок гонки
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private string m_NickName;
        public string Nickname => m_NickName;

        [SerializeField] private Bike m_ActiveBike;


        private void Update()
        {
            ControlBike();
        }

        private void ControlBike()
        {
            m_ActiveBike.SetForwardThrustAxis(0);
            m_ActiveBike.SetHorizontalThrustAxis(0);

            if(Input.GetKey(KeyCode.W))
            {
                m_ActiveBike.SetForwardThrustAxis(1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                m_ActiveBike.SetForwardThrustAxis(-1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                m_ActiveBike.SetHorizontalThrustAxis(-1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                m_ActiveBike.SetHorizontalThrustAxis(1);
            }

            m_ActiveBike.EnableAfterburner = Input.GetKey(KeyCode.Space);
        }
    }
}
