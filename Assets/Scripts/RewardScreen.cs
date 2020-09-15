using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rpgkit
{
    public class RewardScreen : MonoBehaviour
    {

        public int numberOfItemsHeld;
        public int numberOfEquipItemsHeld;

        //Make Instance of this script to be able reference from other scripts!
        public static RewardScreen Instance;

        [Header("Initialization")]
        //Game objects used by this code
        public Text earnedText;
        public Text itemText;
        public GameObject rewardScreen;
        public string[] rewardItems;
        public string[] rewardEquipItems;
        public int xpEarned;
        public int goldEarned;
        //For UI button higlighting
        public Button closeButton;

        [Header("Reward Settings")]
        public bool markQuestComplete;
        public string questToMark;



        // Use this for initialization
        void Start()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenRewardScreen(int xp, int gold, string[] rewards, string[] rewardsequip)
        {
            GameManager.Instance.battleActive = true;

            xpEarned = xp;
            goldEarned = gold;
            rewardItems = rewards;
            rewardEquipItems = rewardsequip;

            earnedText.text = xpEarned + " EXP" + "\n" + goldEarned + " Gold";
            itemText.text = "";

            if (rewardItems.Length == 0 && rewardEquipItems.Length == 0)
            {
                itemText.text += "None";
            }

            //Check if the reward is only item
            if (rewardItems.Length > 0 && rewardEquipItems.Length == 0)
            {
                for (int i = 0; i < rewardItems.Length; i++)
                {
                    itemText.text += rewards[i] + "\n";
                }
            }

            //Check if reward is only equipment
            if (rewardItems.Length == 0 && rewardEquipItems.Length > 0)
            {
                for (int i = 0; i < rewardEquipItems.Length; i++)
                {
                    itemText.text += rewardsequip[i] + "\n";
                }
            }

            //Check if reward is both item and equipment
            if (rewardItems.Length > 0 && rewardEquipItems.Length > 0)
            {
                for (int i = 0; i < rewardItems.Length; i++)
                {
                    itemText.text += rewards[i] + "\n";


                }
                for (int j = 0; j < rewardEquipItems.Length; j++)
                {
                    itemText.text += rewardsequip[j] + "\n";
                }
            }


            rewardScreen.SetActive(true);

            /*
            if (ControlManager.Instance.mobile == false)
            {
                GameMenu.Instance.btn = closeButton;
                GameMenu.Instance.SelectFirstButton();
            }
            */

            GameManager.Instance.gameMenuOpen = true;
        }

        public void CloseRewardScreen()
        {

            //Calculate the amount of items / equipment held in inventory to prevent adding more items if inventory is full
            numberOfItemsHeld = 0;
            numberOfEquipItemsHeld = 0;
            /*
            for (int i = 0; i < GameManager.Instance.itemsHeld.Length; i++)
            {
                if (GameManager.Instance.itemsHeld[i] != "")
                {
                    numberOfItemsHeld++;
                }
            }

            for (int i = 0; i < GameManager.Instance.equipItemsHeld.Length; i++)
            {
                if (GameManager.Instance.equipItemsHeld[i] != "")
                {
                    numberOfEquipItemsHeld++;
                }
            }


            for (int i = 0; i < GameManager.Instance.characterStatus.Length; i++)
            {
                if (GameManager.Instance.characterStatus[i].gameObject.activeInHierarchy && GameManager.Instance.characterStatus[i].currentHP > 0)
                {
                    GameManager.Instance.characterStatus[i].AddExp(xpEarned);
                }
            }

            GameManager.Instance.currentGold += goldEarned;


            if (numberOfItemsHeld < GameManager.Instance.itemsHeld.Length && rewardItems.Length > 0)
            {

                for (int i = 0; i < rewardItems.Length; i++)
                {
                    GameManager.Instance.AddRewardItem(rewardItems[i]);
                }
            }
            if (numberOfItemsHeld + rewardItems.Length > GameManager.Instance.itemsHeld.Length && rewardItems.Length > 0)
            {
                Shop.Instance.promptText.text = "Your item bag is full!";
                StartCoroutine(Shop.Instance.PromptCo());
            }


            if (numberOfEquipItemsHeld < GameManager.Instance.equipItemsHeld.Length && rewardEquipItems.Length > 0)
            {
                for (int i = 0; i < rewardEquipItems.Length; i++)
                {
                    GameManager.Instance.AddRewardEquipItem(rewardEquipItems[i]);
                }
            }
            if (numberOfEquipItemsHeld + rewardEquipItems.Length > GameManager.Instance.equipItemsHeld.Length && rewardEquipItems.Length > 0)
            {
                Shop.Instance.promptText.text = "Your equipment bag is full!";
                StartCoroutine(Shop.Instance.PromptCo());
            }

            if (numberOfItemsHeld + rewardItems.Length > GameManager.Instance.itemsHeld.Length && rewardItems.Length > 0 && numberOfEquipItemsHeld + rewardEquipItems.Length > GameManager.Instance.equipItemsHeld.Length && rewardEquipItems.Length > 0)
            {
                Shop.Instance.promptText.text = "Your item and equipment bag is full!";
                StartCoroutine(Shop.Instance.PromptCo());
            }

            rewardScreen.SetActive(false);
            StartCoroutine(closeRewardScreen());
            GameManager.Instance.gameMenuOpen = false;

            if (markQuestComplete)
            {
                QuestManager.Instance.MarkQuestComplete(questToMark);
            }

            if (ControlManager.Instance.mobile == true)
            {
                GameMenu.Instance.touchMenuButton.SetActive(true);
                GameMenu.Instance.touchController.SetActive(true);
                GameMenu.Instance.touchConfirmButton.SetActive(true);
            }

            AudioManager.Instance.PlayBGM(FindObjectOfType<CameraControl>().musicToPlay);
            */
        }

        public IEnumerator closeRewardScreen()
        {
            yield return new WaitForSeconds(.1f);
            GameManager.Instance.battleActive = false;
        }
    }
}