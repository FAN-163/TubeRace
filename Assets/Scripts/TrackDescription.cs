using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string m_TrackName;
        public string TrackName => m_TrackName;

        [SerializeField] private string m_SceneNickname;
        public string SceneNickname => m_SceneNickname;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        public float TrackLength { get; private set; }


        public void SetTrackLangth(float length)
        {
            TrackLength = length;
        }
    }
}