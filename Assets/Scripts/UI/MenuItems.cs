using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private List<CharaSlot> chara_slot_list;

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
                m_prefItemButton.SetActive(false);
                for (int i = 0; i < 10; i++)
                {
                    GameObject inst = Instantiate(
                        m_prefItemButton,
                        m_rootItemList.transform);
                    inst.SetActive(true);
                    MenuItemsButton script = inst.GetComponent<MenuItemsButton>();

                    if (i < DataManager.Instance.data_item_consume.list.Count )
                    {
                        MasterItemParam master = DataManager.Instance.master_item.list.Find(p => p.item_id == DataManager.Instance.data_item_consume.list[i].item_id);
                        script.m_txtName.text = master.name;
                        script.m_btn.onClick.AddListener(() =>
                        {
                            OnUpdate = select_chara;
                        });
                    }
                    else
                    {
                        script.m_txtName.text = "";
                    }
                }


                // CharaSlot
                m_prefCharaSlot.SetActive(false);
                List<DataCharaParam> party_list = DataManager.Instance.data_chara.list.FindAll(p => 0 < p.position);
                party_list.Sort((a, b) => (b.position - a.position));
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
                    chara_slot_list.Add(script);
                }


            }
        }
        public void select_chara(bool _bInit)
        {
            if(_bInit)
            {
                // 初期選択
                es.SetSelectedGameObject(chara_slot_list[0].gameObject);
            }

        }
    }
}


