using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class MasterCharaParam : CsvDataParam
    {
        public int chara_id { get; set; }
        public string name { get; set; }

        public int hp { get; set; }
        public int sp { get; set; }
        public int strength_base { get; set; }
        public int vital_base { get; set; }

    }
    public class MasterChara : CsvData<MasterCharaParam>
    {
    }
}