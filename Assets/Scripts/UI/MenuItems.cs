using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace rpgkit
{
    public class MenuItems : StateBehavor
    {
        public EventSystem es;

        public GameObject m_rootItemsCharaSlot;
        public GameObject m_prefCharaSlot;

        public GameObject m_rootItemList;
        public GameObject m_prefItemButton;

        public Button m_btnUse;
        public Button m_btnDiscard;

        private int m_iSelectedItemSerial;

        private List<MenuItemsButton> item_list;
        private List<CharaSlot> chara_slot_list;

        private void clear_items()
        {
            if (item_list != null)
            {
                foreach (MenuItemsButton btn in item_list)
                {
                    Destroy(btn.gameObject);
                }
                item_list.Clear();
            }
            item_list = new List<MenuItemsButton>();
            for (int i = 0; i < 10; i++)
            {
                GameObject inst = Instantiate(
                    m_prefItemButton,
                    m_rootItemList.transform);
                inst.SetActive(true);
                MenuItemsButton script = inst.GetComponent<MenuItemsButton>();

                if (i < DataManager.Instance.data_item_consume.list.Count)
                {
                    DataItemParam data = DataManager.Instance.data_item_consume.list[i];
                    MasterItemParam master = DataManager.Instance.master_item.list.Find(p => p.item_id == DataManager.Instance.data_item_consume.list[i].item_id);
                    script.m_txtName.text = master.name;
                    script.m_btn.onClick.AddListener(() =>
                    {
                        m_iSelectedItemSerial = data.item_serial;
                        OnUpdate = select_chara;

                        es.SetSelectedGameObject(null);
                        m_btnDiscard.interactable = true;

                        foreach (CharaSlot cs in chara_slot_list)
                        {
                            cs.m_btn.interactable = true;
                        }
                    });
                }
                else
                {
                    script.m_txtName.text = "";
                }
                item_list.Add(script);
            }

        }
        private void clear_charaslot()
        {
            if (chara_slot_list != null)
            {
                foreach (CharaSlot slot in chara_slot_list)
                {
                    Destroy(slot.gameObject);
                }
                chara_slot_list.Clear();
            }
            List<DataCharaParam> party_list = DataManager.Instance.data_chara.list.FindAll(p => 0 < p.position);
            party_list.Sort((a, b) => (a.position - b.position));
            chara_slot_list = new List<CharaSlot>();
            foreach (DataCharaParam data in party_list)
            {
                MasterCharaParam master = DataManager.Instance.master_chara.list.Find(p => p.chara_id == data.chara_id);

                GameObject instance = GameObject.Instantiate(
                    m_prefCharaSlot,
                    Vector3.zero,
                    new Quaternion(0, 0, 0, 0),
                    m_rootItemsCharaSlot.transform);
                instance.SetActive(true);
                CharaSlot script = instance.GetComponent<CharaSlot>();

                script.Initialize(master, data);
                script.m_btn.interactable = false;

                script.m_btn.onClick.AddListener(() =>
                {
                    m_btnUse.interactable = true;
                });
                chara_slot_list.Add(script);
            }

        }

        public void Init()
        {
            OnUpdate = top;
        }
        public void Close()
        {
            OnUpdate = null;
        }

        public void top(bool _bInit)
        {
            if (_bInit)
            {
                m_btnUse.interactable = false;
                m_btnDiscard.interactable = false;

                m_btnUse.onClick.AddListener(() =>
                {
                    DataItemParam data_item = DataManager.Instance.data_item_consume.list.Find(p => p.item_serial == m_iSelectedItemSerial);
                    MasterItemParam master_item = DataManager.Instance.master_item.list.Find(p => p.item_id == data_item.item_id);

                    Debug.Log(string.Format("{0}を使った", master_item.name));

                    DataManager.Instance.data_item_consume.list.Remove(data_item);

                    clear_items();
                    clear_charaslot();
                });
                m_btnDiscard.onClick.AddListener(() =>
                {
                    Debug.Log(m_iSelectedItemSerial);
                    DataItemParam data_item = DataManager.Instance.data_item_consume.list.Find(p => p.item_serial == m_iSelectedItemSerial);
                    MasterItemParam master_item = DataManager.Instance.master_item.list.Find(p => p.item_id == data_item.item_id);

                    Debug.Log(string.Format("{0}を捨てた", master_item.name));

                    DataManager.Instance.data_item_consume.list.Remove(data_item);
                    clear_items();
                    clear_charaslot();
                });




                m_prefItemButton.SetActive(false);
                clear_items();


                // CharaSlot
                m_prefCharaSlot.SetActive(false);
                clear_charaslot();

            }
        }
        public void select_chara(bool _bInit)
        {

        }
    }
}


