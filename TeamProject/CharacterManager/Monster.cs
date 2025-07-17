using System;

namespace TeamProject
{
    public class Monster : Character
    {
        public Monster() : base() 
        {}

        public Monster(string name, int level, float maxHp, float atkPower, float defPower, string description = "")
            : base(name, level, maxHp, atkPower, defPower, description)
        {}
        public Monster(Character unit) : base(unit) {}

        public override bool IsDead()
        {
            return Hp <= 0 || isDie;
        }

        public string GetStatus()
        {
            if (IsDead())
                return $"Lv.{Level} {Name} Dead";
            else
                return $"Lv.{Level} {Name} HP {Hp}";
        }

        public void PrintStatus()
        {
            if (IsDead())
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(GetStatus());
            Console.ResetColor();
        }
    }
}

