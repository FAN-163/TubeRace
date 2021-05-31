using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Hull : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_SpawnDynamicArmor;
        [SerializeField] private Transform m_SpawnEngine;
        [SerializeField] private Transform m_SpawnTower;
        [SerializeField] private Transform m_SpawnUndercarriage;

        public List<Transform> SpawnDynamicArmor { get => m_SpawnDynamicArmor; set => m_SpawnDynamicArmor = value; }
        public Transform SpawnEngine { get => m_SpawnEngine; set => m_SpawnEngine = value; }
        public Transform SpawnTower { get => m_SpawnTower; set => m_SpawnTower = value; }
        public Transform SpawnUndercarriage { get => m_SpawnUndercarriage; set => m_SpawnUndercarriage = value; }
    }
}
