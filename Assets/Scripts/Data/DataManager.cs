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
		public DataItem data_item_consume;
		public DataItem data_item_equip;

		// 購入に使う金額
		public int gold { get; set; }

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
				Debug.Log("loaderror.data_quest");
			}

			data_item_consume = new DataItem();
			data_item_consume.SetSaveFilename("data/data_item_consume");
			if (data_item_consume.Load() == false)
			{
				Debug.Log("loaderror.data_item_consume");
			}
			data_item_equip = new DataItem();
			data_item_equip.SetSaveFilename("data/data_item_equip");
			if(data_item_equip.Load() == false)
			{
				Debug.Log("loaderror.data_item_equip");
			}

			gold = 100;

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

