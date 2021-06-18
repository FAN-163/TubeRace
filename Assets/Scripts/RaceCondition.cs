using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public abstract class RaceCondition : MonoBehaviour
    {
       public bool IsTriggered { get; protected set; }

        public virtual void OnRaceStart()
        {

        }

        public virtual void OnRaceEnd()
        {

        }
    }
}