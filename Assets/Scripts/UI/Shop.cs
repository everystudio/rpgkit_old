using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace rpgkit
{
    public class Shop : Singleton<Shop>
    {
        private const int item_limit = 1;
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

            m_btnBuy.onClick.RemoveAllListeners();
            m_btnSellItem.onClick.RemoveAllListeners();
            m_btnSellEquip.onClick.RemoveAllListeners();

            m_btnBuy.onClick.AddListener(() =>
            {
                StartCoroutine(buy());
            });
            m_btnSellItem.onClick.AddListener(() =>
            {
                StartCoroutine(sell(DataManager.Instance.data_item_consume.list));
            });
            m_btnSellEquip.onClick.AddListener(() =>
            {
                StartCoroutine(sell(DataManager.Instance.data_item_equip.list));
            });

            m_btnExit.onClick.RemoveAllListeners();
            m_btnExit.onClick.AddListener(() =>
            {
                close();
                _onFinished.Invoke();
            });
        }
        private void open()
        {
            m_txtGold.text = string.Format("{0}g", DataManager.Instance.gold);
            m_goMenu.SetActive(true);
            m_goGold.SetActive(true);
            m_goDetail.SetActive(false);
            m_goList.SetActive(false);
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
        private void show_buy_prompt(MasterItemParam _master)
        {
            m_goPrompt.SetActive(true);
            m_btnPromptYes.onClick.RemoveAllListeners();
            m_btnPromptYes.onClick.AddListener(() =>
            {
                m_goPrompt.SetActive(false);

                //DataManager.Instance.gold -= _master.price;
                if ( _master.category == "equip")
                {
                    DataItemParam new_item = new DataItemParam();
                    new_item.item_id = _master.item_id;
                    DataManager.Instance.data_item_equip.AddItem(new_item);
                }
                else
                {
                    DataItemParam new_item = new DataItemParam();
                    new_item.item_id = _master.item_id;
                    DataManager.Instance.data_item_consume.AddItem(new_item);
                }
                DataManager.Instance.gold -= _master.price;
                m_txtGold.text = string.Format("{0}g", DataManager.Instance.gold);

            });

            if (_master.price <= DataManager.Instance.gold)
            {
                m_txtPrompt.text = string.Format("{0}で購入しますか", _master.price);
                m_btnPromptYes.interactable = true;
                m_btnPromptNo.interactable = true;
            }
            else
            {
                m_txtPrompt.text = string.Format("<color=red>ゴールドが不足しています</color>");
                m_btnPromptYes.interactable = false;
                m_btnPromptNo.interactable = true;
            }

            //DataManager.Instance.gold -= _master.price;

            bool bHasLimit;
            if (_master.category == "equip")
            {
                bHasLimit = DataManager.Instance.data_item_equip.list.Count < item_limit;
                if (bHasLimit == false)
                {
                    m_txtPrompt.text = string.Format("<color=red>装備が持ちきれません</color>");
                }
            }
            else
            {
                bHasLimit = DataManager.Instance.data_item_consume.list.Count < item_limit;
                if (bHasLimit == false)
                {
                    m_txtPrompt.text = string.Format("<color=red>アイテムが持ちきれません</color>");
                }
            }
            m_btnPromptYes.interactable = bHasLimit;


        }
        private void show_sell_prompt(MasterItemParam _master , DataItemParam _data )
        {
            m_goPrompt.SetActive(true);
            m_btnPromptYes.onClick.RemoveAllListeners();
            m_btnPromptYes.onClick.AddListener(() =>
            {
                m_goPrompt.SetActive(false);

                //DataManager.Instance.gold -= _master.price;
                if (_master.category == "equip")
                {
                    DataManager.Instance.data_item_equip.list.Remove(_data);
                }
                else
                {
                    DataManager.Instance.data_item_consume.list.Remove(_data);
                }
                DataManager.Instance.gold += _master.sell_price;
                m_txtGold.text = string.Format("{0}g", DataManager.Instance.gold);
            });

            m_txtPrompt.text = string.Format("{0}で売却しますか", _master.sell_price);
            m_btnPromptYes.interactable = true;
            m_btnPromptNo.interactable = true;
        }

        private IEnumerator buy()
        {
            m_goList.SetActive(true);
            m_goDetail.SetActive(false);
            m_goPrompt.SetActive(false);
            for ( int i = 0; i < m_shopItemButtonArr.Length; i++)
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
                        show_buy_prompt(master);
                    });
                }
                else
                {
                    m_shopItemButtonArr[i].gameObject.SetActive(false);
                }
            }
            yield return null;
        }
        private IEnumerator sell(List<DataItemParam> _list )
        {
            m_goList.SetActive(true);
            m_goDetail.SetActive(false);
            m_goPrompt.SetActive(false);

            for (int i = 0; i < m_shopItemButtonArr.Length; i++)
            {
                if( i < _list.Count)
                {
                    m_shopItemButtonArr[i].gameObject.SetActive(true);

                    int item_id = _list[i].item_id;
                    DataItemParam data = _list[i];

                    MasterItemParam master = DataManager.Instance.master_item.list.Find(p => p.item_id == item_id);

                    m_shopItemButtonArr[i].m_txtName.text = master.name;
                    m_shopItemButtonArr[i].m_txtPrice.text = master.price.ToString();
                    m_shopItemButtonArr[i].m_btn.onClick.AddListener(() =>
                    {
                        show_detail(master);
                        show_sell_prompt(master , data);
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

