using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class EngineSfxController : MonoBehaviour
    {
        [SerializeField] private AudioSource m_EngineSource;

        [SerializeField] private Bike m_Bike;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_VelocityPitchModifier;
         
        private void Update()
        {
            UpdateEngineSoundSimple();
        }

        private void UpdateEngineSoundSimple()
        {
            m_EngineSource.pitch = 1.0f + m_VelocityPitchModifier * m_Bike.GetNormalizedSpeed();
        }
    }
}
