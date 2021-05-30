using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class Projectile : Ammo
    {
        [Header("Скорость снаряда")]
        [Range(0.0f, 200.0f)]
        [SerializeField] private int m_Speed;

        [Header("Дальность полета снаряда")]
        [Range(0.0f, 300.0f)]
        [SerializeField] private float m_FlightRange;

        public override string Caliber { get => Caliber; protected set => value = "122 mm"; }
        public override int Speed { get => m_Speed; protected set => m_Speed = value; }
        public override float FlightRange { get => m_FlightRange; protected set => m_FlightRange = value; }
    }
}
