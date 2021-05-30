using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Bullet : Ammo
    {
        [Header("Скорость снаряда")]
        [Range(0.0f, 500.0f)]
        [SerializeField] private int m_Speed;

        [Header("Дальность полета снаряда")]
        [Range(0.0f, 100.0f)]
        [SerializeField] private float m_FlightRange;

        public override string Caliber { get => Caliber; protected set => value = "7.62 mm"; }
        public override int Speed { get => m_Speed; protected set => m_Speed = value; }

        public override float FlightRange { get => m_FlightRange; protected set => m_FlightRange = value; }
    }
}