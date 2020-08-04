using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class UIControl : Singleton<UIControl>
    {
        public Menu m_Menu;

        private void Start()
        {
            OnUpdate = standby;
        }
        public void standby(bool _bInit)
        {
            if(_bInit)
            {
                Debug.Log("idle.init");
                m_Menu.gameObject.SetActive(false);
            }
            if ( Input .GetKeyDown(KeyCode.X))
            {
                OnUpdate = menu;
            }
        }

        public void menu(bool _bInit)
        {
            if(_bInit)
            {
                Debug.Log("menu.init");
                m_Menu.gameObject.SetActive(true);
                m_Menu.Initialize();

                m_Menu.m_btnClose.onClick.AddListener(() =>
                {
                    OnUpdate = standby;
                    m_Menu.gameObject.SetActive(false);
                });
            }
            //Debug.Log("menu.update");
        }
    }
}


