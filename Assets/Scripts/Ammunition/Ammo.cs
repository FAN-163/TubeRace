using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public abstract class Ammo : MonoBehaviour
    {
        public abstract string Caliber { get; protected set; }
        public abstract int Speed { get; protected set; }

        public abstract float FlightRange { get; protected set; }
        public bool Capsule { get; protected set; }
       

       protected Vector3 m_StartPosition;
       
       

        protected void Update()
        {
            if(Capsule)
            {
                Shot();
            }
            if((m_StartPosition + transform.position).magnitude > FlightRange)
            {
                Destroy(gameObject);
            }
        }

        

        public void BrokeCapsule(Vector3 startPosition)
        {
            transform.position = startPosition;
            m_StartPosition = startPosition;
            Capsule = true;
        }

        protected void Shot()
        {
            transform.localPosition += new Vector3(0f, 0f, Speed * Time.deltaTime);
        }
    }
}
