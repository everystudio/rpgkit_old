using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rpgkit
{
    public class BattleManager : Singleton<BattleManager>
    {
        public bool bUsable;
        Navigation customNav = new Navigation();

        public int m_iButtonValue;

        private bool m_bBattleActive;

        [HideInInspector]
        public SpriteRenderer m_spriteRenderer;
        [HideInInspector]
        public Sprite m_sprBattleBG;

        [HideInInspector]
        public bool m_bAffectHP = false;
        [HideInInspector]
        public bool m_bAffectSP = false;

        [Header("Initialization")]
        //Game objects used by this code
        public GameObject m_goBattleScene;
        public GameObject[] m_goCharacterSlot;
        public GameObject m_goTargetCharacterMenu;
        public GameObject m_goBattleMenu;
        public GameObject m_goTargetEnemyMenu;
        public GameObject m_goSkillMenu;
        public GameObject m_goItemMenu;
        public GameObject[] m_goCurrentTurnIndicator;

        public ReadHilightedButtonBattle[] m_btnHilightedBattleItem;
        public BattleNotification m_bnBattlePrompts;
        public Image[] portrait;
        private DataCharaParam[] m_playerStatsArr;

        public List<BattleCharacter> activeBattlers = new List<BattleCharacter>();

        //Music
        public int battleMusicIntro;
        public int battleMusic;
        public int victoryMusicIntro;
        public int victoryMusic;


        [Header("Menu Buttons")]
        //This holds the touch back button for mobile input
        public GameObject touchBackButton;

        //These are being used for highlighting the correct target enemy button
        public Button targetEnemyMenuButton0;
        public Button targetEnemyMenuButton1;
        public Button targetEnemyMenuButton2;
        public Button targetEnemyMenuButton3;
        public Button targetEnemyMenuButton4;
        public Button targetEnemyMenuButton5;

        //These are being used for showing the correct number of target enemy buttons depending on how many monsters you are fighting with
        public GameObject objTargetMenuButton0;
        public GameObject objTargetMenuButton1;
        public GameObject objTargetMenuButton2;
        public GameObject objTargetMenuButton3;
        public GameObject objTargetMenuButton4;
        public GameObject objTargetMenuButton5;

        //These are being used for highlighting the correct menu button for non-mobile input
        public Button attackButton;
        public Button skillButton;
        public Button itemButton;
        public Button retreatButton;
        public Button skillButton0;
        public Button itemButton0;
        public Button useItemButton;
        public Button targetCharacterButton1;

        [Header("Battle Positions")]
        //Positions of characters & enemies
        public Transform[] characterPositions;
        public Transform[] enemyPositions;

        [Header("Battle Prefabs")]
        //References to character & enemy prefabs
        public BattleCharacter[] characterPrefabs;
        public BattleCharacter[] enemyPrefabs;

        [Header("Battle Effects")]
        //References to battle effect prefabs
        public GameObject enemyAttackEffect;
        public GameObject characterTurnIndicator;
        public DamageNumber theDamageNumber;

        [Header("Battle Turns")]
        //For indication of the current turn
        public int currentTurn;
        public bool waitForTurn;

        [Header("Skills")]
        //Initiates a list of all available skills
        public BattleSkill[] skillList;
        public int skillCost;

        [Header("Items")]
        //For displaying held items
        public ItemButtonBase[] itemButtons;
        public Button[] itemButtonsB;
        public Image itemSprite;

        //For checking the currently selected item
        public Item activeItem;

        [Header("Battle Settings")]
        //Probability to retreat
        public int retreatRate = 35;
        private bool retreating;

        //Name of the game over scene
        public string gameOverScene;

        [Header("Rewards")]
        //Initialisation of rewards for the current/last battle
        public int m_iRewardXP;
        public int m_iRewardGold;
        public string[] m_iRewardItems;
        public string[] m_iRewardEquipItems;


        [Header("General")]
        //For checking if you are able to retreat from the current battle
        public bool m_bNoRetreat;


        public void BattleStart(string[] enemiesToSpawn, bool setCannotFlee)
        {
            m_goBattleMenu.SetActive(true);

            /*
            //Check if mobile controlls are enabled and hide them during battle
            if (ControlManager.instance.mobile == true)
            {
                if (ControlManager.instance.mobile == true)
                {
                    GameMenu.instance.touchMenuButton.SetActive(false);
                    GameMenu.instance.touchController.SetActive(false);
                    GameMenu.instance.touchConfirmButton.SetActive(false);
                    touchBackButton.SetActive(true);
                }
            }

            if (ControlManager.instance.mobile == false)
            {
                GameMenu.instance.btn = attackButton;
                GameMenu.instance.SelectFirstButton();
            }
            */

            UpdateCharacterStatus();


            if (!m_bBattleActive)
            {
                //Will be true or false depending on the setting within the BattleStarter script
                m_bNoRetreat = setCannotFlee;

                m_bBattleActive = true;

                GameManager.Instance.battleActive = true;

                //Put the battle background sprite into place
                transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
                m_spriteRenderer.sprite = m_sprBattleBG;
                m_goBattleScene.SetActive(true);

                //Play battle music
                StartCoroutine(waitForSound());

                /*
                 * 味方側
                for (int i = 0; i < characterPositions.Length; i++)
                {
                    //Check if caharacter at position i is active
                    if (GameManager.Instance.characterStatus[i].gameObject.activeInHierarchy)
                    {
                        for (int j = 0; j < characterPrefabs.Length; j++)
                        {
                            if (characterPrefabs[j].characterName == GameManager.Instance.characterStatus[i].characterName)
                            {
                                //Instantiate every active character at their i positions
                                BattleCharacter newCaracter = Instantiate(characterPrefabs[j], characterPositions[i].position, characterPositions[i].rotation);
                                newCaracter.transform.parent = characterPositions[i];
                                activeBattlers.Add(newCaracter);

                                //Give each character the correct stats from the GameManager script
                                CharacterStatus character = GameManager.Instance.characterStatus[i];
                                activeBattlers[i].currentHp = character.currentHP;
                                activeBattlers[i].maxHP = character.maxHP;
                                activeBattlers[i].currentSP = character.currentSP;
                                activeBattlers[i].maxSP = character.maxSP;
                                activeBattlers[i].strength = character.strength;
                                activeBattlers[i].defense = character.defence;
                                activeBattlers[i].weaponStrength = character.offenseStrength;
                                activeBattlers[i].armorStrength = character.defenseStrength;
                            }
                        }
                    }
                }
                */

                /*
                 * 敵側
                for (int i = 0; i < enemiesToSpawn.Length; i++)
                {
                    if (enemiesToSpawn[i] != "")
                    {
                        for (int j = 0; j < enemyPrefabs.Length; j++)
                        {
                            if (enemyPrefabs[j].characterName == enemiesToSpawn[i])
                            {
                                //Instantiate every enemy at their i positions
                                BattleCharacter newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
                                newEnemy.transform.parent = enemyPositions[i];
                                activeBattlers.Add(newEnemy);

                            }
                        }
                    }
                }
                */

                /*
                //Randomize turn order
                waitForTurn = true;
                currentTurn = Random.Range(0, activeBattlers.Count);

                if (!activeBattlers[currentTurn].character)
                {
                    attackButton.interactable = false;
                    skillButton.interactable = false;
                    itemButton.interactable = false;
                    retreatButton.interactable = false;
                }
                else
                {
                    attackButton.interactable = true;
                    skillButton.interactable = true;
                    itemButton.interactable = true;
                    retreatButton.interactable = true;
                }
                */

                UpdateCharacterStatus();
            }
        }

        public void UpdateCharacterStatus()
        {
            /*

            playerStats = GameManager.Instance.characterStatus;
            for (int i = 0; i < characterName.Length; i++)
            {
                if (activeBattlers.Count > i)
                {
                    if (activeBattlers[i].character)
                    {
                        BattleCharacter playerData = activeBattlers[i];

                        CharacterSlot[i].SetActive(true);
                        characterName[i].gameObject.SetActive(true);
                        characterName[i].text = playerData.characterName;
                        characterHP[i].text = Mathf.Clamp(playerData.currentHp, 0, int.MaxValue) + "/" + playerData.maxHP;
                        HPSlider[i].maxValue = playerData.maxHP;
                        HPSlider[i].value = playerData.currentHp;
                        characterSP[i].text = Mathf.Clamp(playerData.currentSP, 0, int.MaxValue) + "/" + playerData.maxSP;
                        SPSlider[i].maxValue = playerData.maxSP;
                        SPSlider[i].value = playerData.currentSP;
                        portrait[i].sprite = playerData.portrait;
                        characterLevel[i].text = "Lv " + playerStats[i].level;
                    }
                    else
                    {
                        characterName[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    characterName[i].gameObject.SetActive(false);
                }
            }
            */
        }

        public void UpdateBattle()
        {
            bool allEnemiesDead = true;
            bool allPlayersDead = true;

            for (int i = 0; i < activeBattlers.Count; i++)
            {
                if (activeBattlers[i].currentHp < 0)
                {
                    activeBattlers[i].currentHp = 0;
                }

                if (activeBattlers[i].currentHp == 0)
                {
                    //Show dead character
                    if (activeBattlers[i].character)
                    {
                        //activeBattlers[i].spriteRenderer.sprite = activeBattlers[i].defeatedSprite;
                        activeBattlers[i].anim.SetTrigger("Defeated");

                        activeBattlers[i].defeated = true;
                    }
                    else
                    {
                        activeBattlers[i].EnemyFade();
                    }

                }
                else
                {
                    activeBattlers[i].anim.SetTrigger("Battle_idle");
                    if (activeBattlers[i].character)
                    {
                        allPlayersDead = false;
                        activeBattlers[i].spriteRenderer.sprite = activeBattlers[i].aliveSprite;
                    }
                    else
                    {
                        allEnemiesDead = false;
                    }
                }
            }

            if (allEnemiesDead || allPlayersDead)
            {
                if (allEnemiesDead)
                {
                    //Battle won
                    StartCoroutine(EndBattleCo());
                }
                else
                {
                    //Battle lost
                    StartCoroutine(GameOverCo());
                }
            }
            else
            {
                while (activeBattlers[currentTurn].currentHp == 0)
                {
                    currentTurn++;
                    if (currentTurn >= activeBattlers.Count)
                    {
                        currentTurn = 0;
                    }
                }
            }
        }

        void ResetBattleScene()
        {
            attackButton.interactable = false;
            skillButton.interactable = false;
            itemButton.interactable = false;
            retreatButton.interactable = false;

            //Deactivate battle scene
            m_bBattleActive = false;

            m_goTargetEnemyMenu.SetActive(false);
            m_goSkillMenu.SetActive(false);
        }

        //Coroutine to end a battle
        public IEnumerator EndBattleCo()
        {
            ResetBattleScene();

            yield return new WaitForSeconds(.5f);
            AudioManager.Instance.PlayBGM(victoryMusicIntro);

            //Wait Until Sound has finished playing
            while (AudioManager.Instance.bgm[victoryMusicIntro].isPlaying)
            {
                yield return null;
            }

            AudioManager.Instance.PlayBGM(victoryMusic);

            m_goBattleMenu.SetActive(false);

            yield return new WaitForSeconds(.5f);

            //ScreenEffect.Instance.FadeToBlack();

            yield return new WaitForSeconds(1.5f);

            //Update current HP and SP in GameManager script
            for (int i = 0; i < activeBattlers.Count; i++)
            {
                /*
                if (activeBattlers[i].character)
                {
                    for (int j = 0; j < GameManager.Instance.characterStatus.Length; j++)
                    {
                        if (activeBattlers[i].characterName == GameManager.Instance.characterStatus[j].characterName)
                        {
                            GameManager.Instance.characterStatus[j].currentHP = activeBattlers[i].currentHp;
                            GameManager.Instance.characterStatus[j].currentSP = activeBattlers[i].currentSP;
                        }
                    }
                }
                */
                Destroy(activeBattlers[i].gameObject);
            }

            //ScreenFade.instance.FadeFromBlack();
            m_goBattleScene.SetActive(false);
            activeBattlers.Clear();
            currentTurn = 0;

            /*
            if (retreating)
            {
                GameManager.Instance.battleActive = false;
                retreating = false;
                AudioManager.Instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);

                if (ControlManager.instance.mobile == true)
                {
                    GameMenu.instance.touchMenuButton.SetActive(true);
                    GameMenu.instance.touchController.SetActive(true);
                    GameMenu.instance.touchConfirmButton.SetActive(true);
                }
            }
            else
            {
                RewardScreen.instance.OpenRewardScreen(rewardXP, rewardGold, rewardItems, rewardEquipItems);
            }
            */

            //AudioManager.Instance.PlayBGM(FindObjectOfType<CameraController>().musicToPlay);
        }

        //Coroutine to show game over screen
        public IEnumerator GameOverCo()
        {
            //Reset all managers
            /*
            for (int i = 0; i < EventManager.instance.completedEvents.Length; i++)
            {
                EventManager.instance.completedEvents[i] = false;
            }

            for (int i = 0; i < ChestManager.instance.openedChests.Length; i++)
            {
                ChestManager.instance.openedChests[i] = false;
            }
            */

            /*
            for (int i = 0; i < QuestManager.Instance.completedQuests.Length; i++)
            {
                QuestManager.Instance.completedQuests[i] = false;
            }
            */


            //ResetBattleScene();

            attackButton.interactable = false;
            skillButton.interactable = false;
            itemButton.interactable = false;
            retreatButton.interactable = false;

            /*
            //Deactivate battle scene
            battleActive = false;

            targetEnemyMenu.SetActive(false);
            skillMenu.SetActive(false);

            ScreenFade.instance.FadeToBlack();
            yield return new WaitForSeconds(1.5f);
            //Destroy(activeBattlers[0]);
            battleScene.SetActive(false);
            */

            for (int i = 0; i < activeBattlers.Count; i++)
            {
                Destroy(activeBattlers[i].gameObject);
            }

            activeBattlers.Clear();
            currentTurn = 0;
            GameManager.Instance.battleActive = false;

            //SceneManager.LoadScene(gameOverScene);

            yield return null;
        }



        public IEnumerator waitForSound()
        {
            while (AudioManager.Instance.bgm[battleMusicIntro].isPlaying)
            {
                yield return null;
            }
            AudioManager.Instance.PlayBGM(battleMusic);
            yield break;
        }

    }
}



