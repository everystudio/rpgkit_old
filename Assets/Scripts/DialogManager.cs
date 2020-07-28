using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace rpgkit
{
    public class DialogManager : StaticInstance<DialogManager>
    {

        public TextMeshProUGUI m_txtDialog;
        public TextMeshProUGUI m_txtName;
        public GameObject m_goDialog;
        public GameObject m_goName;

        public string[] m_strLineArr;
        public string[] m_strGoodByeArr;
        public int m_iCurrentLine;
        public bool justStarted;

        private void show_name( string _strName)
        {
            m_txtName.text = _strName;
            m_goName.SetActive(_strName != "");
        }

        private IEnumerator show_line(string _strLine )
        {
            bool bInput = false;

            m_txtDialog.text = _strLine;
            m_goDialog.SetActive(true);

            yield return null;

            while (!bInput)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    bInput = true;
                }
                yield return null;
            }
        }

        public IEnumerator ShowDialog( string[] _strLineArr , Action _onFinished)
        {
            m_strLineArr = _strLineArr;

            if(_strLineArr.Length == 0)
            {
                m_strLineArr = new string[1];
                m_strLineArr[0] = "//noline";
            }
            m_iCurrentLine = 0;

            do
            {
                string strLine = m_strLineArr[m_iCurrentLine];
                //Debug.Log(strLine);
                if (check_name(strLine, out string strName))
                {
                    show_name(strName);
                }
                else
                {
                    yield return StartCoroutine(show_line(strLine));
                }

                m_iCurrentLine += 1;
            }
            while (m_iCurrentLine < _strLineArr.Length);
            
            m_goDialog.SetActive(false);
            _onFinished.Invoke();
        }

        private bool check_name(string _strLine , out string _strName )
        {
            bool bRet = false;
            _strName = "";
            if (_strLine.StartsWith("//"))
            {
                _strName = _strLine.Replace("//", "");
                bRet = true;
            }
            return bRet;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}



