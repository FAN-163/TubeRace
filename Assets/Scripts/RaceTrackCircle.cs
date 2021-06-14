using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR

    [CustomEditor(typeof(RaceTrackCircle))]
    public class RaceTrackCircleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                (target as RaceTrackCircle).GenerateTrackDate();
            }
        }
    }
#endif
    public class RaceTrackCircle : RaceTrack
    {
        //[SerializeField] private CurvedTrackPoint[] m_TrackPoints;

        [SerializeField] private float m_TrackRadius;

        [SerializeField] private int m_Division;

        [SerializeField] private Quaternion[] m_TrackSampledRotation;
        [SerializeField] private Vector3[] m_TrackSamplePoints;
        [SerializeField] private float[] m_TrackSampledSegmentLength;
        [SerializeField] private float m_TrackSampledLength;

        [SerializeField] private bool m_DebugDrawBezier;
        [SerializeField] private bool m_DebugSampledPoints;


        private void OnDrawGizmos()
        {
            if (m_DebugDrawBezier)
                DrawCircle();

            if (m_DebugSampledPoints)
                DrawSempledTrackPoints();
        }

        public void GenerateTrackDate()
        {
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();

            var newPoints = GenerateCirclePoints(m_Division);
            var newRotations = GenerateRotations(m_TrackSamplePoints);

            rotations.AddRange(newRotations);
            points.AddRange(newPoints);

            m_TrackSampledRotation = rotations.ToArray();
            m_TrackSamplePoints = points.ToArray();

            // precompute lengths
            {
                m_TrackSampledSegmentLength = new float[m_TrackSamplePoints.Length];

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

        private Quaternion[] GenerateRotations(Vector3[] points)
        {
            List<Quaternion> rotations = new List<Quaternion>();

            float t = 0;

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;

                Vector3 up = Vector3.Lerp(points[i], points[i + 1], t);

                Quaternion rotation = Quaternion.LookRotation(dir, up);

                rotations.Add(rotation);

                t += 1.0f / (points.Length - 1);
            }

            rotations.Add(rotations[0]);

            return rotations.ToArray();
        }

        private Vector3[] GenerateCirclePoints(int division)
        {
            List<Vector3> points = new List<Vector3>();
            float diameter = m_TrackRadius * 2;

            for (int i = 0; i < division; i++)
            {
                float angle = i * Mathf.PI * 2.0f/ division;

                Vector3 newPos = new Vector3(Mathf.Sin(angle) * m_TrackRadius, transform.position.y, Mathf.Cos(angle) * m_TrackRadius);

                points.Add(newPos);
            }
            
            return points.ToArray();
        }

        private void DrawCircle()
        {
            Handles.DrawWireArc(transform.position, transform.up, transform.right, 360.0f, m_TrackRadius);
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

                if (diff < 0)
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
    }
}
