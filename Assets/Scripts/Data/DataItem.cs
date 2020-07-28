using System;
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
        private int get_max_serial()
        {
            int iRet = 0;
            foreach(DataItemParam item in list)
            {
                if( iRet <= item.item_serial)
                {
                    iRet = item.item_serial;
                }
            }
            return iRet;
        }
        public void AddItem(DataItemParam _add)
        {
            int iSerial = get_max_serial() + 1;
            _add.item_serial = iSerial;
            list.Add(_add);
        }
    }
}

