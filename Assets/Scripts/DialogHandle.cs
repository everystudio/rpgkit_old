using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogHandle : MonoBehaviour
    {
        public enum ACTIVATION_TYPE
        {
            BUTTON_PUSH = 0,
            ENTER,
            MAX,
        }
        [Header("Activation")]
        public ACTIVATION_TYPE m_eActivationType;

        [Header("Message Lines")]
        public string[] m_MessageLineArray;
        public string[] m_GoodByeMessageArray;

        [SerializeField]
        public bool m_bFlagActive;

        public bool m_bIsPlaying;

        [Header("Quest Settings")]
        public int m_iMarkQuestId;
        public bool m_bMarkQuestComplete;

        public void FinishedAction()
        {
            if(m_iMarkQuestId != 0)
            {
                DataManager.Instance.MarkQuest(m_iMarkQuestId, m_bMarkQuestComplete);
            }
        }

        public virtual IEnumerator MessageStart(Action _onFinished)
        {
            m_bIsPlaying = true;
            PlayerControl.Instance.m_bCanMove = false;
            yield return StartCoroutine(DialogManager.Instance.ShowDialog(m_MessageLineArray,()=>
            {
                PlayerControl.Instance.m_bCanMove = true;
                m_bIsPlaying = false;

                FinishedAction();

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

                if(m_eActivationType == ACTIVATION_TYPE.ENTER)
                {
                    Debug.Log("enter");
                    StartCoroutine(PlayerControl.Instance.m_dialogHandle.MessageStart(() =>
                    {
                        PlayerControl.Instance.m_dialogHandle = null;
                    }));
                }

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


