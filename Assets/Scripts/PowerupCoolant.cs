using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class PowerupCoolant : Powerup
    {
        public override void OnPickedByBike(Bike bike)
        {
            bike.CoolAfterburner();
        }
    }
}
