using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract string Caliber { get; protected set; }
    }
}