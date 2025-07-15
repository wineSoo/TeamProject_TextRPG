using System;

namespace TeamProject
{
    public class Monster
    {
        public string Name;
        public int Level;
        public int MaxHp;
        public int Hp;
        public int Atk;
        public int Def;
        public string Description;


        public Monster(string name, int level, int maxhp, int atk, int def, string description)
        {
            Name = name;
            Level = level;
            MaxHp = maxhp;
            Hp = maxhp;
            Atk = atk;
            Def = def;
            Description = description;
        }

        public void DamageTaken(int damage)
        {
            Hp -= damage;
            if (Hp < 0)
                Hp = 0;
        }

        //몬스터 죽은후 회색처리
        //public void PrintStatusColor()
        //
        //   if (Hp <= 0)
        //       Console.ForegroundColor = ConsoleColor.Gray;
        //   else
        //       Console.ForegroundColor = ConsoleColor.Red;
        //
        //   Console.WriteLine(Status());
        //   Console.ResetColor();
        //

    }
}
