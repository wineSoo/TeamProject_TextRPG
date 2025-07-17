using System;

namespace TeamProject.CharacterManager
{
    public class Monster : Character
    {
        public bool isDie; // 죽었는지 여부 확인 (true=죽음/false=생존)

        public Monster() : base() { isDie = false; }

        public Monster(string name, int level, float maxHp, float atkPower, float defPower, string description = "")
            : base(name, level, maxHp, atkPower, defPower, description)
        {
            isDie = false;
        }

        public override int DamageTaken(float playerAtk)
        {
            int tmpDam = (int)(playerAtk - DefPower);
            Hp -= tmpDam;
            if (Hp <= 0)
            {
                Hp = 0;
                isDie = true;
            }
            return tmpDam;
        }

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

