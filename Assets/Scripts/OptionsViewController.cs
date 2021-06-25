using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class OptionsViewController : MonoBehaviour
    {
        [SerializeField] private Dropdown m_Dropdown;
        [SerializeField] private bool m_IsFullScreen = true;

        private readonly int[] m_Height = new int[] {576, 720, 1080};
        private readonly int[] m_Wigth = new int[] {720, 1280, 1920};

        //private void Update()
        //{
        //    Dropdown.OptionData optionData = new Dropdown.OptionData();
        //    optionData.text = "";
        //    m_Dropdown.options.Add(optionData);
        //    Debug.Log(m_Dropdown.value);
        //    Debug.Log(m_Dropdown.captionText.text);

        //}

        private void Awake()
        {
            gameObject.SetActive(false);
            SetValueDropdown();
            Screen.SetResolution(m_Wigth[0], m_Height[0], m_IsFullScreen);
        }

        public void SetScreenResolution()
        {
            int optionValue = m_Dropdown.value;
            Debug.Log(optionValue);
            Screen.SetResolution(m_Wigth[optionValue], m_Height[optionValue], m_IsFullScreen);
        }

        private void SetValueDropdown()
        {
            for (int i = 0; i < m_Height.Length; i++)
            {
                Dropdown.OptionData optionData = new Dropdown.OptionData();
                optionData.text = $"{m_Wigth[i]} x {m_Height[i]}";
                m_Dropdown.options.Add(optionData);
            }
        }
    }
}
