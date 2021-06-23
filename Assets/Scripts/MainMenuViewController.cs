using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class MainMenuViewController : MonoBehaviour
    {
        [SerializeField] private TrackSelectionViewController m_TrackSelectionViewController;
        [SerializeField] private OptionsViewController m_OptionsViewController;

        public void OnButtonNewGame()
        {
            m_TrackSelectionViewController.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnButtonOptions()
        {
            m_OptionsViewController.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }

    }
}
