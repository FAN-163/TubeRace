using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class CurvedTrackPoint : MonoBehaviour
    {
        [SerializeField] private float m_Length = 1.0f;
        public float GetLegth()
        {
            return m_Length;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;



            Gizmos.DrawSphere(transform.position, 10.0f);
        }
    }
}
