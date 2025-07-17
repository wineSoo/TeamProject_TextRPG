using System;

namespace TeamProject.CharacterManager
{
    // BossMonster는 단순히 Monster를 상속만 받음

    public class BossMonster : Monster
    {
        // 필요하면 보스 고유 스킬 등 확장 가능
        // public string BossSkill { get; set; }

        public BossMonster() : base() { }

        public BossMonster(string name, int level, float maxHp, float atkPower, float defPower, string description = "")
            : base(name, level, maxHp, atkPower, defPower, description)
        { }       
    }
}