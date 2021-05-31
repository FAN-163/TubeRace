using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public abstract class Ammo : MonoBehaviour
    {
        [Header("Скорость снаряда")]
        [Range(0.0f, 200.0f)]
        [SerializeField] private int m_Speed;

        [Header("Дальность полета снаряда")]
        [Range(0.0f, 300.0f)]
        [SerializeField] private float m_FlightRange;

        [Header("калибр")]
        [SerializeField] private string m_Calibre;

        /// <summary>
        /// Скорость снаряда
        /// </summary>
        public int Speed => m_Speed;

        /// <summary>
        /// Дальность полета
        /// </summary>
        public float FlightRange => m_FlightRange;

        /// <summary>
        /// Калибр
        /// </summary>
        public string Caliber => m_Calibre;

        /// <summary>
        /// Капсуль 
        /// </summary>
        private bool m_Capsule;

        protected Vector3 m_StartPosition;
        
       

        protected void Update()
        {
            if(m_Capsule)
            {
                Shot();
            }
            if((m_StartPosition + transform.position).magnitude > FlightRange)
            {
                Destroy(gameObject);
            }
        }


        /// <summary>
        /// Пробиваем капсуль
        /// </summary>
        /// <param name="startPosition"></param>
        public void BrokeCapsule(Vector3 startPosition)
        {
            AmmoToShotPoint(startPosition);
            m_Capsule = true;
        }

        /// <summary>
        /// Выстрел
        /// </summary>
        /// <param name="startPosition"></param>
        protected void Shot()
        {
            transform.localPosition += new Vector3(0f, 0f, Speed * Time.deltaTime);
        }

       
        /// <summary>
        /// Выставляем Ammo в точку выстрела 
        /// </summary>
        /// <param name="startPosition"></param>
        private void AmmoToShotPoint(Vector3 startPosition)
        {
            transform.position = startPosition;
            m_StartPosition = startPosition;
        }

        
    }
}
