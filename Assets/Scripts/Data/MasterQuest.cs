using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class MasterQuestParam : CsvDataParam
    {
        public int quest_id { get; set; }
        public string name { get; set; }
        public string outline { get; set; }
        public string detail { get; set; }
    }
    public class MasterQuest : CsvData<MasterQuestParam>
    {
    }
}