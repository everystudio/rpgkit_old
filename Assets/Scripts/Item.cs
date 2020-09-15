using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class Item : MonoBehaviour
    {

        [Header("Item Type")]
        public bool item;
        public bool revive;
        public bool offense;
        public bool defense;

        [Header("Item Details")]
        public string itemName;
        public string description;
        public int price;
        public int sellPrice;
        public Sprite itemSprite;

        [Header("Item Details")]
        public bool affectHP;
        public bool affectMP;
        public int amountToChange;

        [Header("Weapon/Armor Details")]
        public int offenseStrength;

        public int defenseStrength;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UseBattleItem(int charToUseOn)
        {
            /*
            if (GameManager.Instance.battleActive)
            {
                if (item)
                {
                    //Check if item affects HP but doesn't revive
                    if (affectHP && !revive)
                    {
                        //Check if character has at least 1 HP & isn't fully healed 
                        if (BattleManager.Instance.activeBattlers[charToUseOn].currentHp > 0 && BattleManager.Instance.activeBattlers[charToUseOn].currentHp != BattleManager.Instance.activeBattlers[charToUseOn].maxHP)
                        {
                            //Tell battlemanager that the item is able to be used
                            BattleManager.Instance.usable = true;

                            BattleManager.Instance.UpdateCharacterStatus();
                            BattleManager.Instance.UpdateBattle();

                            BattleManager.Instance.activeBattlers[charToUseOn].currentHp += amountToChange;

                            //Make item healing within the characters max HP
                            if (BattleManager.Instance.activeBattlers[charToUseOn].currentHp > BattleManager.Instance.activeBattlers[charToUseOn].maxHP)
                            {
                                BattleManager.Instance.activeBattlers[charToUseOn].currentHp = BattleManager.Instance.activeBattlers[charToUseOn].maxHP;
                            }

                            GameManager.Instance.RemoveItem(itemName);
                            AudioManager.Instance.PlaySFX(2);
                            BattleManager.Instance.CloseItemCharChoice();
                            BattleManager.Instance.itemMenu.SetActive(false);
                            BattleManager.Instance.battleMenu.SetActive(true);

                        }
                        else
                        {
                            AudioManager.Instance.PlaySFX(3);
                        }

                    }

                    //Check if item affects HP and revives
                    if (affectHP && revive)
                    {
                        //Check if character is defeated before reviving
                        if (BattleManager.Instance.activeBattlers[charToUseOn].currentHp == 0)
                        {
                            //Tell battlemanager that the item is able to be used
                            BattleManager.Instance.usable = true;

                            BattleManager.Instance.UpdateCharacterStatus();
                            BattleManager.Instance.UpdateBattle();

                            BattleManager.Instance.activeBattlers[charToUseOn].currentHp += amountToChange;

                            //Make item healing within the characters max HP
                            if (BattleManager.Instance.activeBattlers[charToUseOn].currentHp > BattleManager.Instance.activeBattlers[charToUseOn].maxHP)
                            {
                                BattleManager.Instance.activeBattlers[charToUseOn].currentHp = BattleManager.Instance.activeBattlers[charToUseOn].maxHP;
                            }

                            GameManager.Instance.RemoveItem(itemName);
                            AudioManager.Instance.PlaySFX(2);
                            BattleManager.Instance.CloseItemCharChoice();
                            BattleManager.Instance.itemMenu.SetActive(false);
                            BattleManager.Instance.battleMenu.SetActive(true);

                        }
                        else
                        {
                            AudioManager.Instance.PlaySFX(3);
                        }

                    }

                    //Check if item affects SP & character is not defeated
                    if (affectMP && BattleManager.Instance.activeBattlers[charToUseOn].currentHp > 0)
                    {
                        //Check if SP needs to be healed
                        if (BattleManager.Instance.activeBattlers[charToUseOn].currentSP != BattleManager.Instance.activeBattlers[charToUseOn].maxSP)
                        {
                            //Tell battlemanager that the item is able to be used
                            BattleManager.Instance.usable = true;

                            BattleManager.Instance.UpdateCharacterStatus();
                            BattleManager.Instance.UpdateBattle();

                            BattleManager.Instance.activeBattlers[charToUseOn].currentSP += amountToChange;

                            //Make item healing within the characters max SP
                            if (BattleManager.Instance.activeBattlers[charToUseOn].currentSP > BattleManager.Instance.activeBattlers[charToUseOn].maxSP)
                            {
                                BattleManager.Instance.activeBattlers[charToUseOn].currentSP = BattleManager.Instance.activeBattlers[charToUseOn].maxSP;
                            }

                            GameManager.Instance.RemoveItem(itemName);
                            AudioManager.Instance.PlaySFX(2);
                            BattleManager.Instance.CloseItemCharChoice();
                            BattleManager.Instance.itemMenu.SetActive(false);
                            BattleManager.Instance.battleMenu.SetActive(true);

                        }
                        else
                        {
                            AudioManager.Instance.PlaySFX(3);
                        }

                    }
                    if (affectMP && BattleManager.Instance.activeBattlers[charToUseOn].currentHp == 0)
                    {
                        AudioManager.Instance.PlaySFX(3);
                    }
                }
            }
            */
        }

        public void Use(int charToUseOn)
        {
            /*
            CharacterStatus selectedChar = GameManager.Instance.characterStatus[charToUseOn];

            if (item)
            {
                if (affectHP && !revive)
                {
                    if (selectedChar.currentHP > 0 && selectedChar.currentHP != selectedChar.maxHP)
                    {
                        selectedChar.currentHP += amountToChange;
                        BattleManager.Instance.affectHP = true;

                        if (selectedChar.currentHP > selectedChar.maxHP)
                        {
                            selectedChar.currentHP = selectedChar.maxHP;
                        }

                        GameManager.Instance.RemoveItem(itemName);
                        AudioManager.Instance.PlaySFX(2);
                        GameMenu.Instance.CompleteUseItem();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(3);
                    }

                }

                if (affectHP && revive)
                {
                    if (selectedChar.currentHP == 0)
                    {
                        selectedChar.currentHP += amountToChange;
                        BattleManager.Instance.affectHP = true;

                        if (selectedChar.currentHP > selectedChar.maxHP)
                        {
                            selectedChar.currentHP = selectedChar.maxHP;
                        }

                        GameManager.Instance.RemoveItem(itemName);
                        AudioManager.Instance.PlaySFX(2);
                        GameMenu.Instance.CompleteUseItem();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(3);
                    }

                }

                if (affectMP && selectedChar.currentHP > 0)
                {
                    if (selectedChar.currentSP != selectedChar.maxSP)
                    {
                        selectedChar.currentSP += amountToChange;
                        BattleManager.Instance.affectSP = true;

                        if (selectedChar.currentSP > selectedChar.maxSP)
                        {
                            selectedChar.currentSP = selectedChar.maxSP;
                        }

                        GameManager.Instance.RemoveItem(itemName);
                        AudioManager.Instance.PlaySFX(2);
                        GameMenu.Instance.CompleteUseItem();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX(3);
                    }

                }
                if (affectMP && selectedChar.currentHP == 0)
                {
                    AudioManager.Instance.PlaySFX(3);
                }

            }

            if (offense)
            {
                if (selectedChar.equippedOffenseItem != "")
                {

                    GameManager.Instance.EquipItem(selectedChar.equippedOffenseItem);
                }

                selectedChar.equippedOffenseItem = itemName;
                selectedChar.offenseStrength = offenseStrength;

                GameManager.Instance.RemoveItem(itemName);
                AudioManager.Instance.PlaySFX(2);
                GameMenu.Instance.CompleteUseItem();
            }

            if (defense)
            {
                if (selectedChar.equippedDefenseItem != "")
                {
                    GameManager.Instance.EquipItem(selectedChar.equippedDefenseItem);
                }

                selectedChar.equippedDefenseItem = itemName;
                selectedChar.defenseStrength = defenseStrength;

                GameManager.Instance.RemoveItem(itemName);
                AudioManager.Instance.PlaySFX(2);
                GameMenu.Instance.CompleteUseItem();
            }
        */

        }
    }

}

