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

        [SerializeField] private Quaternion[] m_TrackSampledRotation;
        [SerializeField] private Vector3[] m_TrackSamplePoints;
        [SerializeField] private float[] m_TrackSampledSegmentLength;
        [SerializeField] private float m_TrackSampledLength;

        [SerializeField] private bool m_DebugDrawBezier;
        [SerializeField] private bool m_DebugSampledPoints;

        //public CurvedTrackPoint[] TrackPoints => m_TrackPoints; 

        private void OnDrawGizmos()
        {
            if(m_DebugDrawBezier)
                DrawBezierCurve();

            if(m_DebugSampledPoints)
                DrawSempledTrackPoints();
        }

        public void GenerateTrackDate()
        {
            if (m_TrackPoints.Length < 3)
                return;

            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();

            for (int i = 0; i < m_TrackPoints.Length - 1; i++)
            {
                var newPoints = GenerateBezierPoints(m_TrackPoints[i], m_TrackPoints[i + 1], m_Division);

                var newRotations = GenerateRotations(m_TrackPoints[i].transform, m_TrackPoints[i + 1].transform, newPoints);

                rotations.AddRange(newRotations);
                points.AddRange(newPoints);
            }

            // last points
            var lastPoint = GenerateBezierPoints(m_TrackPoints[m_TrackPoints.Length - 1], m_TrackPoints[0], m_Division);
            var lastRotations = GenerateRotations(m_TrackPoints[m_TrackPoints.Length - 1].transform, m_TrackPoints[0].transform, lastPoint);

            rotations.AddRange(lastRotations);
            points.AddRange(lastPoint);

            m_TrackSampledRotation = rotations.ToArray();
            m_TrackSamplePoints = points.ToArray();

            // precompute lengths
            {
                m_TrackSampledSegmentLength = new float[m_TrackSamplePoints.Length ];

                m_TrackSampledLength = 0;

                for (int i = 0; i < m_TrackSamplePoints.Length - 1; i++)
                {
                    Vector3 a = m_TrackSamplePoints[i];
                    Vector3 b = m_TrackSamplePoints[i + 1];

                    float segmentLength = (b - a).magnitude;

                    m_TrackSampledSegmentLength[i] = segmentLength;
                    m_TrackSampledLength += segmentLength;
                }
            }

            EditorUtility.SetDirty(this);

        }

        private void DrawSempledTrackPoints()
        {
            Handles.DrawAAPolyLine(m_TrackSamplePoints);
        }

        private Quaternion[] GenerateRotations(Transform a, Transform b, Vector3[] points)
        {
            List<Quaternion> rotations = new List<Quaternion>();

            float t = 0;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;

                Vector3 up = Vector3.Lerp(a.up, b.up, t);

                Quaternion rotation = Quaternion.LookRotation(dir, up);

                rotations.Add(rotation);

                t += 1.0f / (points.Length - 1);
            }

            rotations.Add(b.rotation);

            return rotations.ToArray();
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

            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for (int i = 0; i < m_TrackSampledSegmentLength.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLength[i];

                if (diff < 0)
                {
                    return (m_TrackSamplePoints[i + 1] - m_TrackSamplePoints[i]).normalized;
                    //return position
                }
                else
                {
                    distance -= m_TrackSampledSegmentLength[i];
                }
            }

            return Vector3.forward;
        }

        public override Vector3 GetPosition(float distance)
        {
            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for (int i = 0; i < m_TrackSampledSegmentLength.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLength[i];

                if(diff < 0)
                {
                    //return position
                    float t = distance / m_TrackSampledSegmentLength[i];

                    return Vector3.Lerp(m_TrackSamplePoints[i], m_TrackSamplePoints[i + 1], t);
                }
                else
                {
                    distance -= m_TrackSampledSegmentLength[i];
                }
            }

            return Vector3.zero;
        }

        public override Quaternion GetRotation(float distance)
        {
            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for (int i = 0; i < m_TrackSampledSegmentLength.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLength[i];

                if (diff < 0)
                {
                    //return position
                    float t = distance / m_TrackSampledSegmentLength[i];

                    return Quaternion.Slerp(m_TrackSampledRotation[i], m_TrackSampledRotation[i + 1], t);
                }
                else
                {
                    distance -= m_TrackSampledSegmentLength[i];
                }
            }

            return Quaternion.identity;
        }

        public override float GetTrackLength()
        {
            return m_TrackSampledLength;
        }

        public CurvedTrackPoint[] GetTrackPoints()
        {
            return m_TrackPoints;
        }
    }
}
