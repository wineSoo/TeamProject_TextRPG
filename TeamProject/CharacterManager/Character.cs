using System;

namespace TeamProject
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string Description { get; set; }

        
        public Character() { }

        
        public Character(string name, int level, int maxHp, int atk, int def, string description)
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            Hp = maxHp;
            Atk = atk;
            Def = def;
            Description = description;
        }

        // 데미지 받기
        public int DamageTaken(int atk)
        {
            int tmpDam = 0;
            tmpDam = (int)(atk - Def);

            Hp -= tmpDam;
            if (Hp < 0)
            {
                Hp = 0;
            }
            return tmpDam;
        }

        // 죽었는지 확인
        public virtual bool IsDead()
        {
            return Hp <= 0;
        }
    }
}