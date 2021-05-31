using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Undercarriage : MonoBehaviour
    {
        [SerializeField] private Transform m_SpawnleftTrack;
        [SerializeField] private Transform m_SpawnRightTrack;

        public Transform SpawnleftUndercarriage { get => m_SpawnleftTrack; set => m_SpawnleftTrack = value; }
        public Transform SpawnRightUndercarriage { get => m_SpawnRightTrack; set => m_SpawnRightTrack = value; }
    }
}