using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    [System.Serializable]
    public class BattleSkill
    {

        public string skillName;
        public int skillPower;
        public int skillCost;
        public AttackEffect effect;
    }
}

