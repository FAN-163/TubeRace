using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Car : MonoBehaviour
    {
        [Multiline]
        [SerializeField] private string m_ModelName;
        [SerializeField] private float m_EnginePower;

        [Range(0, 10)]
        [SerializeField] private int m_NumSrteeringWheels;

        [HideInInspector]
        [SerializeField] private Color m_Color;

        [SerializeField] private Vector3 m_Pos;
        [SerializeField] private Quaternion m_Rotation;

        [SerializeField] private Transform m_WheelsA;
        [SerializeField] private Transform m_WheelsB;

        [SerializeField] private CustomBehavior m_Custom;

    }
}
