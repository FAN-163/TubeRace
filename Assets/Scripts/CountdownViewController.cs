using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class CountdownViewController : MonoBehaviour
    {
        [SerializeField] private RaceController m_RaceController;
        [SerializeField] private Text m_Label;

        private float tGo = 0;

        private void Update()
        {
            int t = (int)m_RaceController.CountTimer;

            if (t != 0 )
            {
                m_Label.text = t.ToString();
            }
            else if(t == 0 && (int)tGo >= 0)
            {
                m_Label.text = "Go";

                tGo -= Time.deltaTime;
            }
            else
            {
                m_Label.text = "";

                gameObject.SetActive(false);
            }
        }
    }
}
