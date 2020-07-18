using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogHandle : MonoBehaviour
    {
        [Header("Message Lines")]
        public string[] m_MessageLineArray;
        public string[] m_GoodByeMessageArray;

        [SerializeField]
        private bool m_bFlagActive;

        public bool m_bIsPlaying;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator MessageStart(Action _onFinished)
        {
            m_bIsPlaying = true;
            PlayerControl.Instance.m_bCanMove = false;
            yield return StartCoroutine(DialogManager.Instance.ShowDialog(m_MessageLineArray,()=>
            {
                PlayerControl.Instance.m_bCanMove = true;
                m_bIsPlaying = false;
                _onFinished.Invoke();
            }));
        }


        //Check if player enters trigger zone
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                m_bFlagActive = true;
                PlayerControl.Instance.m_dialogHandle = this;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                m_bFlagActive = false;
                if( PlayerControl.Instance.m_dialogHandle == this)
                {
                    PlayerControl.Instance.m_dialogHandle = null;
                }
            }
        }

    }
}


