using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR

    [CustomEditor(typeof(SplineMeshProxy))]
    public class SplineMeshProxyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Update"))
            {
                (target as SplineMeshProxy).UpdatePoints();
            }
        }
    }
#endif

    [RequireComponent(typeof(SplineMesh.Spline))]
    public class SplineMeshProxy : MonoBehaviour
    {
        [SerializeField] private RaceTrackCurved m_CurvedTrack;

        [SerializeField] private CurvedTrackPoint m_PointA;
        [SerializeField] private CurvedTrackPoint m_PointB;

        public void UpdatePoints()
        {
            var spline = GetComponent<SplineMesh.Spline>();

            var n0 = spline.nodes[0];
            n0.Position = m_PointA.transform.position;
            n0.Direction = m_PointA.transform.position + m_PointA.transform.forward * m_PointA.GetLegth();

            var n1 = spline.nodes[1];
            n1.Position = m_PointB.transform.position;
            n1.Direction = m_PointB.transform.position + m_PointB.transform.forward * m_PointB.GetLegth();

        }
    }
}
