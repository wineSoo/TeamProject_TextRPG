using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Player : Character
    {
        private static Player? instance;

        public enum PlayerJob { Warrior, Archer, Theif, Mage }
        public PlayerJob Job { get; set; }
        public float Skill { get; set; } // 치명타율
        public float Speed { get; set; } // 회피율
        public float Mp { get; set; }
        public float MaxMp { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int DungeonFloor { get; set; }

        private Player() : base()
        {
            Level = 1;
            Name = "이름 없는";
            Job = PlayerJob.Warrior;
            AtkPower = 30f; //과제 기본값 30
            DefPower = 5f;
            Skill = 15f;
            Speed = 10f;
            MaxHp = 100f;
            Hp = MaxHp;
            MaxMp = 50f;
            Mp = MaxMp;
            Gold = 1500;
            Exp = 0;
            DungeonFloor = 1;   
        }

        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        }

        // 플레이어 전용 랜덤 데미지 함수 (원하면 override)
        public void PlayerGetDamage(float monsterAtk)
        {
            float atkErrorFloat = monsterAtk / 10f;
            int atkError = (int)Math.Ceiling(atkErrorFloat);
            Random random = new Random();
            int damage = random.Next((int)(monsterAtk - atkError), (int)(monsterAtk + atkError) + 1) - (int)Math.Ceiling(DefPower);

            if (damage < 0) damage = 0;
            Hp -= damage;
            if (Hp < 0) Hp = 0;
        }

        public bool LevelCalculator(int expGained)
        {
            Exp += expGained;
            int expToLevelUP;
            bool isLevelUp = false;
            do
            {
                expToLevelUP = (5 * Level * Level + 35 * Level - 20) / 2;
                if (expToLevelUP <= Exp)
                {
                    Level++;
                    AtkPower += 0.5f;
                    DefPower++;
                    isLevelUp = true;
                }
            }
            while (expToLevelUP <= Exp);
            return isLevelUp;
        }

        public void StatInitializer(PlayerJob selectedjob)
        {
            switch (selectedjob)
            {
                case PlayerJob.Warrior:
                    Job = PlayerJob.Warrior;
                    MaxHp = 150f; Hp = MaxHp;
                    break;
                case PlayerJob.Archer:
                    Job = PlayerJob.Archer;
                    Skill = 25f;
                    break;
                case PlayerJob.Theif:
                    Speed = 20f; Job = PlayerJob.Theif;
                    break;
                case PlayerJob.Mage:
                    Job = PlayerJob.Mage;
                    MaxMp = 100f;
                    Mp = MaxMp;
                    break;
            }
        }
    }
}