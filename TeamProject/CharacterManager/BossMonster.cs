using System;

namespace TeamProject
{ 
    public class BossMonster : Monster
    {
        public BossMonster() : base() { }

        public BossMonster(string name, int level, float maxHp, float atkPower, float defPower, MonsterIndex index, string description = "")
            : base(name, level, maxHp, atkPower, defPower, index, description)
        {          
        }
        public BossMonster(Character unit) : base(unit) {}

    }
}
