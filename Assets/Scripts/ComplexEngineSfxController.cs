using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class ComplexEngineSfxController : MonoBehaviour
    {
        [SerializeField] private Bike m_Bike;

        [SerializeField] private AudioSource m_SfxLow;
        [SerializeField] private AudioSource m_SfxHigh;
        [SerializeField] private AudioSource m_SfxLoud;

        [SerializeField] private AnimationCurve m_CurveLow; // 0
        [SerializeField] private AnimationCurve m_CurveHigh; // 0.5
        [SerializeField] private AnimationCurve m_CurveLoud; // 1

        [SerializeField] private AudioSource m_SfxSonicBoom;

        public const float PithFactor = 0.3f;

        [SerializeField] private float m_SuperSonicSpeed;
        [SerializeField] private AnimationCurve m_SonicCurve;

        public bool IsSuperSonic { get; private set; }

        public void SetSuperSonic(bool flag)
        {
            if(!IsSuperSonic && flag)
            {
                m_SfxSonicBoom.Play();
            }

            IsSuperSonic = flag;
        }

        private void Update()
        {
            SetSuperSonic(Mathf.Abs(m_Bike.GetVelocity()) > m_SuperSonicSpeed);

            if(m_SfxSonicBoom.isPlaying)
            {
                var t = Mathf.Clamp01(m_SfxSonicBoom.time / m_SfxSonicBoom.clip.length);

                m_SfxSonicBoom.volume = m_SonicCurve.Evaluate(t);
            }

            UpdateDynamicEngineSound();
        }

        private void UpdateDynamicEngineSound()
        {
            if(IsSuperSonic)
            {
                m_SfxLow.volume = 0;
                m_SfxHigh.volume = 0;
                m_SfxLoud.volume = 0;
                return;
            }

            //var t = m_Bike.GetNormalizedSpeed();
            var t = Mathf.Clamp01(m_Bike.GetVelocity() / m_SuperSonicSpeed);

            m_SfxLow.volume = m_CurveLow.Evaluate(t);

            m_SfxLow.pitch = 1.0f + PithFactor * t;

            m_SfxHigh.volume = m_CurveHigh.Evaluate(t);

            m_SfxHigh.pitch = 1.0f + PithFactor * t;

            m_SfxLoud.volume = m_CurveLoud.Evaluate(t);

        }

    }
}
