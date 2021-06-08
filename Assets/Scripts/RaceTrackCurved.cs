using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR

    [CustomEditor(typeof(RaceTrackCurved))]
    public class RaceTrackCurvedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Generate"))
            {
                (target as RaceTrackCurved).GenerateTrackDate();
            }
        }
    }
#endif

    public class RaceTrackCurved : RaceTrack
    {
        [SerializeField] private CurvedTrackPoint[] m_TrackPoints;

        [SerializeField] private int m_Division;

        [SerializeField] private Vector3[]  m_TrackSamplePoints;

        private void OnDrawGizmos()
        {
            DrawBezierCurve();
            DrawSempledTrackPoints();
        }

        public void GenerateTrackDate()
        {
            if (m_TrackPoints.Length < 3)
                return;

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < m_TrackPoints.Length - 1; i++)
            {
                points.AddRange(GenerateBezierPoints(m_TrackPoints[i], 
                    m_TrackPoints[i + 1], 
                    m_Division));
            }

            points.AddRange(GenerateBezierPoints(
                m_TrackPoints[m_TrackPoints.Length - 1], 
                m_TrackPoints[0], 
                m_Division));

            m_TrackSamplePoints = points.ToArray();


        }



        private void DrawSempledTrackPoints()
        {
            Handles.DrawAAPolyLine(m_TrackSamplePoints);
        }

        private Vector3[] GenerateBezierPoints(
            CurvedTrackPoint a, 
            CurvedTrackPoint b,
            int division)
        {
           return Handles.MakeBezierPoints(
                a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLegth(),
                b.transform.position - b.transform.forward * b.GetLegth(),
                division);
        }

        private void DrawBezierCurve()
        {
            if (m_TrackPoints.Length < 3)
                return;

            for(int i = 0; i < m_TrackPoints.Length - 1; i++)
            {
                DrawTrackPartGizmo(m_TrackPoints[i], m_TrackPoints[i + 1]);
            }

            DrawTrackPartGizmo(m_TrackPoints[m_TrackPoints.Length - 1] , m_TrackPoints[0]);
        }

        private void DrawTrackPartGizmo(CurvedTrackPoint a, CurvedTrackPoint b)
        {

            Handles.DrawBezier(
                a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLegth(),
                b.transform.position - b.transform.forward * b.GetLegth(),
                Color.green,
                Texture2D.whiteTexture,
                1.0f);
        }

        public override Vector3 GetDirection(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 GetPosition(float distance)
        {
            return Vector3.zero;
        }

        public override float GetTrackLength()
        {
            return 1.0f;
        }
    }
}
