using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rpgkit
{
    public class BattleNotification : MonoBehaviour
    {
        public float awakeTime;
        private float awakeCounter;
        public Text notificationText;

        void Update()
        {
            if (awakeCounter > 0)
            {
                awakeCounter -= Time.deltaTime;
                if (awakeCounter <= 0)
                {
                    gameObject.SetActive(false);
                    BattleManager.Instance.m_goBattleMenu.SetActive(true);
                }
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            awakeCounter = awakeTime;
        }

    }


}



