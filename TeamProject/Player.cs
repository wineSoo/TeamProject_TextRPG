using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Player
    {

        private static Player? instance;

       
        //속성을 초기화 합니다.
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
        public int DamageTaken(int atk, out bool isHit, out bool isCritical)
        {
            int tmpDam = 0;
            int check = rand.Next(10);
            isCritical = false;

            // 10% 확률로 공격 실패(0~3, 5~9)
            if (check == 6) isHit = false; // 공격 실패 시
            //if (check <= 5) isHit = false; // 테스트용
            else // 공격 성공 시
            {
                int tmpAtk = rand.Next((int)(atk - atk * 0.1f),
                    (int)(atk * 0.1f >= 0.5f ? (int)(atk + atk * 0.1f + 1) : (int)(atk + atk * 0.1f)));
                isHit = true;
                tmpDam = (int)(tmpAtk - DefPower);

                if (tmpDam < 0) tmpDam = 0; // 데미지는 0 밑으로 떨어짐x

                // 치명타 계산
                check = rand.Next(0, 100);
                if (check <= 54)
                {
                    isCritical = true;
                    tmpDam = (int)(tmpDam * 1.6f); // 160% 데미지
                }

                Hp -= tmpDam;
                if (Hp <= 0)
                {
                    Hp = 0;
                }
            }
            return tmpDam;
        }

        // 함수 오버로딩
        public int DamageTaken(ref Skill skill, out bool isHit, out bool isCritical)
        {
            int tmpDam = 0;
            int check = rand.Next(10);
            isCritical = false;
            isHit = true;

            // 스킬 공격은 회피 불가
            if (skill.Type == TeamProject.Skill.SkillType.AttackSkill || check != 6) // 스킬이거나 회피가 발동 안했다면
            {
                int tmpAtk = rand.Next((int)(skill.Atk - skill.Atk * 0.1f),
                        (int)(skill.Atk * 0.1f >= 0.5f ? (int)(skill.Atk + skill.Atk * 0.1f + 1) : (int)(skill.Atk + skill.Atk * 0.1f)));

                tmpDam = (int)(tmpAtk - DefPower);

                if (tmpDam < 0) tmpDam = 0; // 데미지는 0 밑으로 떨어짐x

                // 치명타 계산
                check = rand.Next(0, 100);
                if (check <= 54)
                {
                    isCritical = true;
                    tmpDam = (int)(tmpDam * 1.6f); // 160% 데미지
                }

                Hp -= tmpDam;
                if (Hp <= 0)
                {
                    Hp = 0;
                }
            }
            else isHit = false; // 노멀 공격이며 회피 발동 시, 데미지 계산 x, 
            
            return tmpDam;
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
        public float BattleStartHp { get; set; }

        Random rand = new Random();
        public int Gold { get; set; }
        public int Exp { get; set; }

        public int DungeonFloor { get; set; }

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
        public bool LevelCalculator(int expGained)
        {
            Exp += expGained;
            int expToLevelUP;
            bool isLevelUp = false;
            do
            {
                expToLevelUP = (5 * Lv * Lv + 35 * Lv - 20) / 2;
                if ( expToLevelUP <= Exp)
                {
                    Lv++;
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
