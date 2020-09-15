using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    [System.Serializable]
    public class BattleType
    {

        public string[] enemies;
        public int rewardXP;
        public int rewardGold;
        public string[] rewardItems;
        public string[] rewardEquipItems;
    }

    public class BattleStarter : MonoBehaviour
    {
        [Header("Enemy Settings")]
        public float fEncountRate;
        public BattleType[] randomBattles;

        [Header("Battle Settings")]
        public Sprite sprBattleBG;
        public bool bActivateOnEnter;
        public bool bActivateOnExit;
        public bool bSingleBattle;
        public bool bNoRetreat;
        public bool bCompleteQuest;
        public string strQuestToComplete;

        private bool bInArea;
        public float fCountdown;

        void Start()
        {
            fCountdown = Random.Range(1, fEncountRate);
            //fCountdown = 10;
        }
        private void Update()
        {
            if( bInArea && PlayerControl.Instance.m_bCanMove)
            {
                fCountdown -= Time.deltaTime;

                if (fCountdown < 0f)
                {
                    fCountdown = Random.Range(1, fEncountRate);

                    StartCoroutine(StartBattleCo());
                }

            }
        }



        public IEnumerator StartBattleCo()
        {
            //AudioManager.instance.PlayBGM(BattleManager.instance.battleMusicIntro);

            //ScreenEffect.Instance.FadeToBlack();

            BattleManager.Instance.m_sprBattleBG = sprBattleBG;

            //GameManager.instance.battleActive = true;
            //GameMenu.instance.gotItemMessage.SetActive(false);

            int selectedBattle = Random.Range(0, randomBattles.Length);

            BattleManager.Instance.m_iRewardItems = randomBattles[selectedBattle].rewardItems;
            BattleManager.Instance.m_iRewardEquipItems = randomBattles[selectedBattle].rewardEquipItems;
            BattleManager.Instance.m_iRewardXP = randomBattles[selectedBattle].rewardXP;
            BattleManager.Instance.m_iRewardGold = randomBattles[selectedBattle].rewardGold;

            yield return new WaitForSeconds(1.5f);

            BattleManager.Instance.BattleStart(randomBattles[selectedBattle].enemies, bNoRetreat);
            BattleManager.Instance.UpdateCharacterStatus();
            BattleManager.Instance.UpdateBattle();
            //ScreenFade.instance.FadeFromBlack();

            if (bSingleBattle)
            {
                gameObject.SetActive(false);
            }

            RewardScreen.Instance.markQuestComplete = bCompleteQuest;
            RewardScreen.Instance.questToMark = strQuestToComplete;
        }

    }
}




