using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private int m_NumObjects;
        [SerializeField] private RaceTrack m_Tarck;

        private void Start()
        {
            float distance = 0;

            for (int i = 0; i < m_NumObjects; i++)
            {
                var e = Instantiate(m_Prefab);

                e.transform.position = m_Tarck.GetPosition(distance);
                e.transform.forward = m_Tarck.GetDirection(distance);

                distance += m_Tarck.GetTrackLength() / m_NumObjects;
            }
        }
    }
}
