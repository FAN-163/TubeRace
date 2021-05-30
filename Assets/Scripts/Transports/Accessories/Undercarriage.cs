using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Undercarriage : MonoBehaviour
    {
        [SerializeField] private Transform m_SpawnleftTrack;
        [SerializeField] private Transform m_SpawnRightTrack;

        [Range(0.0f, 50.0f)]
        [SerializeField] private float mass = 10.0f;

        public Transform SpawnleftUndercarriage { get => m_SpawnleftTrack; set => m_SpawnleftTrack = value; }
        public Transform SpawnRightUndercarriage { get => m_SpawnRightTrack; set => m_SpawnRightTrack = value; }
        public float Mass { get => mass; set => mass = value; }
    }
}