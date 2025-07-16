using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Player
    {

        private static Player? instance;

       
        //속성을 초기화 합니다. 나중에 직업을 구현하면 직업에 따른 초기 능력치를 조절할 수 있을 것 같습니다.
        private Player()
        {
            Lv = 1;
            Name = "이름 없는"; //기본값
            Job = PlayerJob.Warrior;
            AtkPower = 30; // 과제 기본값 10
            DefPower = 5;
            Skill = 15;
            Speed = 10;
            Hp = 100;
            MaxHp = 100;
            Mp = 50;
            MaxMp = 50;
            Gold = 1500;

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

        public enum PlayerJob 
        {
            Warrior, Archer, Theif, Mage
        }
        
        
        //플레이어 속성. 필요하면 추가해서 쓰세용
        public int Lv { get; set; }
        public string Name { get; set; }
        public PlayerJob Job { get; set; }
        public float AtkPower { get; set; }
        public float DefPower { get; set; }
        public float Skill { get; set; } // 치명타율
        public float Speed { get; set; } // 회피율
        public float Hp { get; set; }
        public float MaxHp { get; set; }
        public float Mp { get; set; }
        public float MaxMp { get; set; }

        public int Gold { get; set; }

        public void PlayerGetDamage(int monsterAtk)
        {
            float atkErrorFloat = monsterAtk / 10;
            int atkError = (int)Math.Ceiling(atkErrorFloat);
            Random random = new Random();
            int damage = random.Next(monsterAtk - atkError, monsterAtk + atkError + 1) - (int)Math.Ceiling(DefPower);
            
            if ( damage < 0) damage = 0;

            Hp -= damage;

            if (Hp < 0) Hp = 0;
        }

        public void StatInitializer(PlayerJob selectedjob)
        {
            switch (selectedjob)
            {
                case PlayerJob.Warrior:
                    Job = PlayerJob.Warrior;
                    MaxHp = 150;
                    Hp = MaxHp;
                    break;
                case PlayerJob.Archer:
                    Job = PlayerJob.Archer;
                    Skill = 25;
                    break;
                case PlayerJob.Theif:
                    Speed = 20;
                    Job = PlayerJob.Theif;
                    break;
                case PlayerJob.Mage:
                    Job = PlayerJob.Mage;
                    MaxMp = 100;
                    Mp = MaxMp;
                    break;
                default:
                    break;
            }
        }

    }
}
