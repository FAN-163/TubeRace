using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class PowerupFuel : Powerup
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] private float m_FuelAmount;

        public override void OnPickedByBike(Bike bike)
        {
            bike.AddFuel(m_FuelAmount);

            // для избегания бесконечного получения бонуса
            // можно добавить таймер обновления бонуса 
            m_FuelAmount -= m_FuelAmount;
        }
    }
}
