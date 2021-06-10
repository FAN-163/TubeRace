using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{

    /// <summary>
    /// ������� ����� ������� ���������� ���� ����� ��� �����
    /// </summary>
    public abstract class RaceTrack : MonoBehaviour
    {
        /// <summary>
        /// ������ �����
        /// </summary>
        [Header("Base Track properties")]
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        /// <summary>
        /// ����� ���������� ����� �����
        /// </summary>
        /// <returns></returns>
        public abstract float GetTrackLength();

        /// <summary>
        /// ��������������� ������� ������ � 3� ����� ����� �����
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public abstract Vector3 GetPosition(float distance);

        /// <summary>
        /// ����� ���������� ����������� � 3� ������ ����� ����� �����
        /// ����������� � ������ � �����
        /// </summary>
        /// <param name="disatance"></param>
        /// <returns></returns>
        public abstract Vector3 GetDirection(float distance);

        public virtual Quaternion GetRotation(float distance)
        {
            return Quaternion.identity;
        }
    }

}
