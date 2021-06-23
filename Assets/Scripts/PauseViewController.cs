using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{

    public class PauseViewController : MonoBehaviour
    {
        public static readonly string MainMenuScene = "scene_main_menu";

        [SerializeField] private RectTransform m_Content;

        [SerializeField] private RaceController m_RaceController;

        private void Start()
        {
            m_Content.gameObject.SetActive(false);

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_RaceController.IsRaceActive)
                {
                    m_Content.gameObject.SetActive(!m_Content.gameObject.activeInHierarchy);

                    UpdateGameActivity(!m_Content.gameObject.activeInHierarchy);
                }
            }
        }

        private void UpdateGameActivity(bool flag)
        {
            if(flag)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }

        public void OnButtonContiniue()
        {
            UpdateGameActivity(true);
            m_Content.gameObject.SetActive(false);
        }

        public void OnButtonEndRace()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuScene);
        }
    }
}
