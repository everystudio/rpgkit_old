using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace rpgkit
{
    public class CharaSlotEquip : MonoBehaviour
    {
        public Button m_btn;
        public TextMeshProUGUI m_txtName;
        public TextMeshProUGUI m_txtLevel;

        public TextMeshProUGUI m_txtWeapon;
        public TextMeshProUGUI m_txtOffence;
        public TextMeshProUGUI m_txtArmor;
        public TextMeshProUGUI m_txtDefence;

        public void Initialize(MasterCharaParam _master, DataCharaParam _data)
        {
            if (m_btn == null)
            {
                m_btn = gameObject.GetComponent<Button>();
            }
            m_txtName.text = _master.name;
            m_txtLevel.text = _data.level.ToString();
        }


    }
}




