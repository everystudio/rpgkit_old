using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rpgkit
{
    public class Menu : StateBehavor
    {
        public Button m_btnClose;
        public Button m_btnItems;
        public Button m_btnEquip;
        public Button m_btnSkills;
        public Button m_btnStatus;

        public GameObject m_rootCharaSlot;
        public GameObject m_prefCharaSlot;

        public MenuItems menu_items;
        public MenuEquips menu_equips;

        public void Initialize()
        {
            OnUpdate = top;

            m_btnItems.onClick.RemoveAllListeners();
            m_btnEquip.onClick.RemoveAllListeners();
            m_btnSkills.onClick.RemoveAllListeners();
            m_btnStatus.onClick.RemoveAllListeners();

            m_btnItems.onClick.AddListener(() =>
            {
                OnUpdate = items;
            });
            m_btnEquip.onClick.AddListener(() =>
            {
                OnUpdate = equip;
            });
            m_btnSkills.onClick.AddListener(() =>
            {
                OnUpdate = skills;
            });
            m_btnStatus.onClick.AddListener(() =>
            {
                OnUpdate = status;
            });
        }


        public void top(bool _bInit)
        {
            if (_bInit)
            {
                menu_items.gameObject.SetActive(false);



                m_prefCharaSlot.SetActive(false);
                List<DataCharaParam> party_list = DataManager.Instance.data_chara.list.FindAll(p => 0 < p.position);
                party_list.Sort((a, b) => (a.position - b.position));

                foreach (DataCharaParam data in party_list)
                {
                    MasterCharaParam master = DataManager.Instance.master_chara.list.Find(p => p.chara_id == data.chara_id);

                    GameObject instance = GameObject.Instantiate(
                        m_prefCharaSlot,
                        Vector3.zero,
                        new Quaternion(0, 0, 0, 0),
                        m_rootCharaSlot.transform);
                    instance.SetActive(true);
                    CharaSlot script = instance.GetComponent<CharaSlot>();

                    script.Initialize(master, data);
                }

            }
        }
        public void items(bool _bInit)
        {
            if(_bInit)
            {
                m_rootCharaSlot.SetActive(false);

                menu_items.gameObject.SetActive(true);
                menu_items.Init();
            }

        }
        public void equip(bool _bInit)
        {
            if (_bInit)
            {
                m_rootCharaSlot.SetActive(false);

                menu_items.gameObject.SetActive(true);
                menu_items.Init();
            }

        }
        public void skills(bool _bInit)
        {

        }
        public void status(bool _bInit)
        {

        }


    }
}



