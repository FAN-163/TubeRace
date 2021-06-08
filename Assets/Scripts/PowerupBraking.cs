using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class PowerupBraking : Powerup
    {

        [Range(0.0f, 100.0f)]
        [SerializeField] private float m_Amount;

        public override void OnPickedByBike(Bike bike)
        {
            bike.Braking(m_Amount);
        }
    }
}
