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

        private CurvedTrackPoint[] m_CurvedTrackPoints;

        public void UpdatePoints()
        {
            var spline = GetComponent<SplineMesh.Spline>();
            m_CurvedTrackPoints = m_CurvedTrack.GetTrackPoints();
            
            spline.nodes.Clear();

            for (int i = 0; i < m_CurvedTrackPoints.Length; i++)
            {

                Vector3 pos = m_CurvedTrackPoints[i].transform.position;
                Vector3 dir = m_CurvedTrackPoints[i].transform.position + m_CurvedTrackPoints[i].transform.forward * m_CurvedTrackPoints[i].GetLegth();

                SplineMesh.SplineNode newSplinNode = new SplineMesh.SplineNode(pos, dir);
                    
                spline.AddNode(newSplinNode);
            }

            Vector3 pos0 = m_CurvedTrackPoints[0].transform.position;
            Vector3 dir0 = m_CurvedTrackPoints[0].transform.position + m_CurvedTrackPoints[0].transform.forward * m_CurvedTrackPoints[0].GetLegth();

            SplineMesh.SplineNode newSplinNode0 = new SplineMesh.SplineNode(pos0, dir0);

            spline.AddNode(newSplinNode0);
        }

        

        //public void UpdatePoints()
        //{
        //    var spline = GetComponent<SplineMesh.Spline>();
            

        //    var n0 = spline.nodes[0];
        //    n0.Position = m_PointA.transform.position;
        //    n0.Direction = m_PointA.transform.position + m_PointA.transform.forward * m_PointA.GetLegth();

        //    var n1 = spline.nodes[1];
        //    n1.Position = m_PointB.transform.position;
        //    n1.Direction = m_PointB.transform.position + m_PointB.transform.forward * m_PointB.GetLegth();
            
        //    var n2 = spline.nodes[2];
        //    n2.Position = m_PointC.transform.position;
        //    n2.Direction = m_PointC.transform.position + m_PointC.transform.forward * m_PointC.GetLegth();
           
        //    var n3 = spline.nodes[3];
        //    n3.Position = m_PointD.transform.position;
        //    n3.Direction = m_PointD.transform.position + m_PointD.transform.forward * m_PointD.GetLegth();

        //    var n4 = spline.nodes[4];
        //    n4.Position = m_PointE.transform.position;
        //    n4.Direction = m_PointE.transform.position + m_PointE.transform.forward * m_PointE.GetLegth();
           
        //    var n5 = spline.nodes[5];
        //    n5.Position = m_PointF.transform.position;
        //    n5.Direction = m_PointF.transform.position + m_PointF.transform.forward * m_PointF.GetLegth();

        //}
    }
}
