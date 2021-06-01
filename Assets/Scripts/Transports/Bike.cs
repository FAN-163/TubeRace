using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;
        
        [Range(0.0f, 10.0f)]
        public float thrust;

        [Range(0.0f, 100.0f)]
        public float agility;
        public float maxSpeed;

        public bool afteburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters m_BikeParametersInitial;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController m_VisualController;

        /// <summary>
        /// Управление газом байка. Нормализованное. от -1 до +1.
        /// </summary>
        private float m_ForwardThrustAxis;

        /// <summary>
        /// Устанавливать значение педали газа
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            m_ForwardThrustAxis = val;
        }

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованноею От -1 до +1.
        /// </summary>
        private float m_HorizontalThrustAxis;

        public void SetHorizontalThrustAxis(float val)
        {
            m_HorizontalThrustAxis = val;
        }

        private void Update()
        {
            MoveBike();
        }

        private void MoveBike()
        {
            float currentForwardVelocity = m_ForwardThrustAxis * m_BikeParametersInitial.maxSpeed;
            Vector3 forwadMoveDelta = transform.forward * currentForwardVelocity * Time.deltaTime;

            transform.position += forwadMoveDelta; 
        }
    }
}

