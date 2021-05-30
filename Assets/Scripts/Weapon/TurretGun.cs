
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class TurretGun : Weapon
    {
        public override string Caliber { get => Caliber; protected set => value = "122 mm"; }
    }
}
