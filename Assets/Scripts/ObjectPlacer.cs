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


        [Header("1 - 100%, 2 - 50%, 3 - 30% ")]
        [Range(1, 3)]
        [SerializeField] private int m_ChanceGenerateObject = 1;

        [SerializeField] private bool m_RandomizeRotation;
        [SerializeField] private bool m_RandomizePosition;

        private GameObject prefab;

        private void Start()
        {
            float distance = 0;

            for (int i = 0; i < m_NumObjects; i++)
            {

                float random = UnityEngine.Random.Range(0, 100);

                if (m_RandomizePosition)
                {
                    if (random % m_ChanceGenerateObject == 0)
                    {
                        prefab = Instantiate(m_Prefab);
                    }
                }
                else
                {
                    prefab = Instantiate(m_Prefab);
                }

                if (prefab)
                {
                    prefab.transform.position = m_Tarck.GetPosition(distance);
                    prefab.transform.rotation = m_Tarck.GetRotation(distance);
                    
                    if (m_RandomizeRotation)
                    {
                        prefab.transform.Rotate(Vector3.forward, UnityEngine.Random.Range(0, 360), Space.Self);
                    }
                }

                distance += m_Tarck.GetTrackLength() / m_NumObjects;
            }
        }
    }
}
