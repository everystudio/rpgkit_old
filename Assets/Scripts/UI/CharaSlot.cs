using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace rpgkit
{
    public class CharaSlot : MonoBehaviour
    {
        public TextMeshProUGUI m_txtName;
        public TextMeshProUGUI m_txtLevel;
        public Slider m_slExp;
        public Slider m_slHP;
        public Slider m_slSP;
        public TextMeshProUGUI m_txtHP;
        public TextMeshProUGUI m_txtSP;

        public void Initialize(MasterCharaParam _master , DataCharaParam _data)
        {
            m_txtName.text = _master.name;
            m_txtLevel.text = _data.level.ToString();
            m_slHP.value = (float)(_data.hp_current / _data.hp_max);
            m_slSP.value = (float)(_data.sp_current / _data.sp_max);

            m_txtHP.text = string.Format("{0}/{1}", _data.hp_current, _data.hp_max);
            m_txtSP.text = string.Format("{0}/{1}", _data.sp_current, _data.sp_max);

        }

    }
}



