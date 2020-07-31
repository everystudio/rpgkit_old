using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class DataCharaParam : CsvDataParam
    {
        public int chara_id { get; set; }
        public int party_position { get; set; }
        public int level { get; set; }
        public int hp_current { get; set; }
        public int hp_max { get; set; }
        public int sp_current { get; set; }
        public int sp_max { get; set; }
        public int exp_current { get; set; }

        public int strength { get; set; }
        public int vital { get; set; }

        public int equip_weapon { get; set; }
        public int equip_armor { get; set; }
        public int equip_accessary { get; set; }

        private int build_param_level( int _iBase , int _iLevel )
        {
            float rate = _iLevel * 0.1f;
            return _iBase + Mathf.RoundToInt(_iBase * rate);
        }

        public void Build(int _iLevel , MasterCharaParam _master)
        {
            hp_max = build_param_level(_master.hp, _iLevel);
            sp_max = build_param_level(_master.sp, _iLevel);
            strength = build_param_level(_master.strength_base, _iLevel);
            vital = build_param_level(_master.vital_base, _iLevel);
        }


    }
    public class DataChara : CsvData<DataCharaParam>
    {
    }
}


