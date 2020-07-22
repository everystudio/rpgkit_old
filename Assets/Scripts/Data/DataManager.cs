using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class DataManager : StaticInstance<DataManager>
    {
		public bool Initialized = false;
		public TextAsset m_taMasterQuest;
		public TextAsset m_taMasterItem;

		public MasterQuest master_quest;
		public MasterItem master_item;

		public DataQuest data_quest;
		public DataItem data_item;

		public override void Initialize()
		{
			base.Initialize();
			master_quest = new MasterQuest();
			master_quest.Load(m_taMasterQuest);
			master_item = new MasterItem();
			master_item.Load(m_taMasterItem);

			data_quest = new DataQuest();
			data_quest.SetSaveFilename("data/data_quest");
			if(data_quest.Load()==false)
			{
				Debug.Log("loaderror");
			}
			Initialized = true;
		}

		public void MarkQuest(int _iQuestId , bool _bFlag)
		{
			DataQuestParam quest = data_quest.list.Find(p => p.quest_id == _iQuestId);
			if(quest == null)
			{
				quest = new DataQuestParam();
				quest.quest_id = _iQuestId;
				data_quest.list.Add(quest);
			}
			quest.is_completed = _bFlag;

			ObjectActivator[] objectActivatorArr = FindObjectsOfType<ObjectActivator>();
			foreach( ObjectActivator activator in objectActivatorArr)
			{
				activator.QuestUpdate();
			}

		}
	}
}

