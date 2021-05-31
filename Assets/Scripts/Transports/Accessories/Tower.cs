using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_SpawnDynamicArmor;
        [SerializeField] private Transform m_MainWeaponPointOfShot;
        [SerializeField] private Transform m_SecondaryWeaponPointOfShot;

        public Transform MainWeaponPointOfShot { get => m_MainWeaponPointOfShot; private set => m_MainWeaponPointOfShot = value; }
        public Transform SecondaryWeaponPointOfShot { get => m_SecondaryWeaponPointOfShot; private set => m_SecondaryWeaponPointOfShot = value; }
        public List<Transform> SpawnDynamicArmor { get => m_SpawnDynamicArmor; set => m_SpawnDynamicArmor = value; }
    }
}
