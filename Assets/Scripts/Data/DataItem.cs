using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class DataItemParam : CsvDataParam
    {
        public int item_serial { get; set; }
        public int item_id { get; set; }
    }
    public class DataItem : CsvData<DataItemParam>
    {
    }
}

