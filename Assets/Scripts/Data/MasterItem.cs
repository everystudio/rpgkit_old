using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class MasterItemParam : CsvDataParam
    {
        public int item_id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int sell_price { get; set; }

        public int affect_hp { get; set; }
        public int affect_sp { get; set; }

        public int offense { get; set; }
        public int defense { get; set; }

        public string sprite_name { get; set; }
    }
    public class MasterItem : CsvData<MasterItemParam>
    {
    }
}



