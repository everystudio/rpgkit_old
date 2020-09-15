using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rpgkit
{
    public class ReadHilightedButtonBattle : ReadHilightedButtonBase
    {
        public override void OnSelect(BaseEventData eventData)
        {
            BattleManager.Instance.m_iButtonValue = buttonValue;
            if (GameManager.Instance.itemsHolder[buttonValue] != "")
            {
                BattleManager.Instance.itemSprite.color = new Color(1, 1, 1, 1);
                //BattleManager.Instance.SelectBattleItem(GameManager.Instance.GetItemDetails(GameManager.Instance.itemsHeld[buttonValue]));
            }
            else
            {
                //BattleManager.Instance.battleItemName.text = "No items!";
                //BattleManager.Instance.battleItemDescription.text = "";
                BattleManager.Instance.itemSprite.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
