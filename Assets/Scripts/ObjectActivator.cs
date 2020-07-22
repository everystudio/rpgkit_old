using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class ObjectActivator : MonoBehaviour
    {
        public GameObject m_targetObject;

        public int m_iCheckQuestId;
        public bool m_bActiveIfCompleted;

        IEnumerator Start()
        {
            while(DataManager.Instance == null)
            {
                yield return null;
            }
            while ( DataManager.Instance.Initialized == false)
            {
                yield return null;
            }

            QuestUpdate();
        }

        public void QuestUpdate()
        {
            if (m_iCheckQuestId != 0)
            {
                bool is_completed = CheckCompletion(DataManager.Instance.data_quest.list);
                SetActiveState(is_completed);
            }
        }

        public bool CheckCompletion(List<DataQuestParam> _listData)
        {
            if (_listData.Find(p => p.quest_id == m_iCheckQuestId) != null)
            {
                return true;
            }
            return false;
        }
        public void SetActiveState(bool _bIsComplete)
        {
            m_targetObject.SetActive(_bIsComplete ? m_bActiveIfCompleted : !m_bActiveIfCompleted);
        }
    }
}


