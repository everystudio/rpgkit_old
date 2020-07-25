using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace rpgkit
{
    public class Shop : StaticInstance<Shop>
    {
        public MasterItemParam[] m_itemLineup;

        public Button m_btnBuy;
        public Button m_btnSellItem;
        public Button m_btnSellEquip;
        public Button m_btnExit;

        public TextMeshProUGUI m_txtGold;
        public ShopItemButton[] m_shopItemButtonArr;
        public ShopItemDetail m_shopItemDetail;


        public void Begin(int[] _lineup , Action _onFinished)
        {

        }
    }
}

