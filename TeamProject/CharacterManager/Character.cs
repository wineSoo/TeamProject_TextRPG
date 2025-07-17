using System;

namespace TeamProject
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }         // 통일: Lv(X), Level(O)
        public float MaxHp { get; set; }
        public float Hp { get; set; }
        public float AtkPower { get; set; }    // 통일: Atk(X), AtkPower(O)
        public float DefPower { get; set; }    // 통일: Def(X), DefPower(O)
        public string Description { get; set; }

        protected Character()
        {
            Name = "";
            Level = 1;
            MaxHp = 100;
            Hp = MaxHp;
            AtkPower = 10;
            DefPower = 5;
            Description = "";
        }

        protected Character(string name, int level, float maxHp, float atkPower, float defPower, string description = "")
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            Hp = maxHp;
            AtkPower = atkPower;
            DefPower = defPower;
            Description = description;
        }

        // 공통 대미지 처리
        public virtual int DamageTaken(float attackerAtk)
        {
            int tmpDam = (int)(attackerAtk - DefPower);
            if (tmpDam < 0) tmpDam = 0;

            Hp -= tmpDam;
            if (Hp < 0) Hp = 0;

            return tmpDam;
        }

        public virtual bool IsDead()
        {
            return Hp <= 0;
        }
    }
}
