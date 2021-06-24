using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription m_TrackDescription;
        [SerializeField] private Text m_TrackName;

        private TrackDescription m_ActiveDescription;

        private void Start()
        {
            if (m_TrackDescription != null)
            {
                SetViewValues(m_TrackDescription);
                SetPreviewImage();
            }
        }

        public void OnButtonStartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_ActiveDescription.SceneNickname);
        }

        private void SetViewValues(TrackDescription desc)
        {
            m_ActiveDescription = desc;

            m_TrackName.text = desc.TrackName;
        }

        private void SetPreviewImage()
        {
            transform.GetComponent<Image>().sprite = m_TrackDescription.PreviewImage;
        }

       
    }
}
