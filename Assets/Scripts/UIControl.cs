using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class UIControl : Singleton<UIControl>
    {
        public GameObject m_goMenu;

        private void Start()
        {
            OnUpdate = idle;
        }
        public void idle(bool _bInit)
        {
            if(_bInit)
            {
                Debug.Log("idle.init");
            }
            if( Input .GetKeyDown(KeyCode.X))
            {
                OnUpdate = menu;
            }
        }

        public void menu(bool _bInit)
        {
            if(_bInit)
            {
                Debug.Log("menu.init");
                m_goMenu.SetActive(true);
            }
            Debug.Log("menu.update");
        }
    }
}


