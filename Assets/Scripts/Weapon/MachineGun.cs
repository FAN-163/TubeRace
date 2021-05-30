using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class MachineGun : Weapon
    {
        public override string Caliber { get => Caliber; protected set => value = "7.62 mm"; }
    }
}