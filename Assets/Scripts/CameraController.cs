using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Bike m_TargetBike;

        [SerializeField] private float m_MinFov = 60.0f;
        [SerializeField] private float m_MaxFov = 85.0f;

        [SerializeField] private float m_ShakeFactor;
        [SerializeField] private AnimationCurve m_ShakeCurve;

        private Vector3 m_InitialLocalPosition;

        private void Start()
        {
            m_InitialLocalPosition = Camera.main.transform.localPosition;
        }

        private void Update()
        {
            UpdateFov();
            UpdateCameraShake();
        }

        private void UpdateCameraShake()
        {
            var cam = Camera.main;
            var t = m_TargetBike.GetNormalizedSpeed();
            var curveValue = m_ShakeCurve.Evaluate(t);

            var randomVector = UnityEngine.Random.insideUnitSphere * m_ShakeFactor;
            randomVector.z = 0;


            cam.transform.localPosition = m_InitialLocalPosition + randomVector * curveValue;
        }

        private void UpdateFov()
        {
            var cam = Camera.main;

            var t = m_TargetBike.GetNormalizedSpeed();

            cam.fieldOfView = Mathf.Lerp(m_MinFov, m_MaxFov, t);
        }
    }
}
