using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class DataQuestParam : CsvDataParam
    {
        public int quest_id { get; set; }
        public bool is_completed { get; set; }
    }
    public class DataQuest : CsvData<DataQuestParam>
    {
    }
}



