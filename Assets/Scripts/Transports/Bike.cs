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
    /// Controller
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

        private BikeParameters m_EffectiveOarameters;

        //methods that changes model and view

        //каждый метод это небольшое действие сущности
        //это действие должно быть максимально эффективно
        //следует избегать длинных методов, тело метода
        //повторяющиеся действие это кандидат на вынос в отдельный метод;

        private void DoSomething(Vector3 a, string b)
        {
            Debug.Log($"a = {a} | b = {b}");

            m_VisualController.SetupBikeView(m_BikeParametersInitial);
        }

        #region Unity events

       
        private void Update()
        {
          
        }
        #endregion
    }
}

