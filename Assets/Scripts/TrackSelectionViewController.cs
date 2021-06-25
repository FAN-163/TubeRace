using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{


    public class TrackSelectionViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController m_MainMenuViewController;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            gameObject.SetActive(false);

            m_MainMenuViewController.gameObject.SetActive(true);
        }
    }
}
