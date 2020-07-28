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

        public TextMeshProUGUI m_txtPrompt;
        public Button m_btnPromptYes;
        public Button m_btnPromptNo;

        public GameObject m_goMenu;
        public GameObject m_goGold;
        public GameObject m_goDetail;
        public GameObject m_goList;
        public GameObject m_goPrompt;

        public void Begin(int[] _lineup , Action _onFinished)
        {
            open();

            int lineup_num = 0;

            foreach( int item_id in _lineup)
            {
                if( item_id != 0)
                {
                    lineup_num += 1;
                }
            }

            m_itemLineup = new MasterItemParam[lineup_num];
            for( int i = 0; i < lineup_num; i++)
            {
                MasterItemParam master = DataManager.Instance.master_item.list.Find(p => p.item_id == _lineup[i]);
                //Debug.Log(master);
                //Debug.Log(master.name);
                m_itemLineup[i] = master;
            }

            StartCoroutine(buy());

            m_btnExit.onClick.RemoveAllListeners();
            m_btnExit.onClick.AddListener(() =>
            {
                close();
                _onFinished.Invoke();
            });
        }
        private void open()
        {
            m_goMenu.SetActive(true);
            m_goGold.SetActive(true);
            m_goDetail.SetActive(false);
            m_goList.SetActive(true);
            m_goPrompt.SetActive(false);
        }

        private void close()
        {
            m_goMenu.SetActive(false);
            m_goGold.SetActive(false);
            m_goDetail.SetActive(false);
            m_goList.SetActive(false);
        }

        private void show_detail(MasterItemParam _master)
        {
            m_goDetail.SetActive(true);
            m_shopItemDetail.m_txtName.text = _master.name;
            m_shopItemDetail.m_txtDescription.text = _master.description;
        }
        private void show_prompt(int _iPrice)
        {
            m_goPrompt.SetActive(true);

            m_txtPrompt.text = string.Format("{0}で購入しますか", _iPrice);
        }

        private IEnumerator buy()
        {
            m_btnBuy.onClick.RemoveAllListeners();
            m_btnSellItem.onClick.RemoveAllListeners();
            m_btnSellEquip.onClick.RemoveAllListeners();
            m_btnExit.onClick.RemoveAllListeners();

            for( int i = 0; i < m_shopItemButtonArr.Length; i++)
            {
                if(i < m_itemLineup.Length)
                {
                    m_shopItemButtonArr[i].gameObject.SetActive(true);

                    int item_id = m_itemLineup[i].item_id;

                    m_shopItemButtonArr[i].m_txtName.text = m_itemLineup[i].name;
                    m_shopItemButtonArr[i].m_txtPrice.text = m_itemLineup[i].price.ToString();
                    m_shopItemButtonArr[i].m_btn.onClick.AddListener(() =>
                    {
                        MasterItemParam master = DataManager.Instance.master_item.list.Find(p => p.item_id == item_id);
                        show_detail(master);
                        show_prompt(master.price);
                    });
                }
                else
                {
                    m_shopItemButtonArr[i].gameObject.SetActive(false);
                }
            }



            yield return null;


        }


    }
}

