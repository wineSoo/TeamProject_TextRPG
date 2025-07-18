using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public float BattleStartHp { get; set; }
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
            skills.Add(new TeamProject.Skill(this));
            skills.Add(new AlphaStrike(this));
            skills.Add(new DoubleStrike(this));
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
                    for (int i = 0; i < skills.Count; i++)
                    {
                        skills[i].SetDamge();
                    }
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
        public void UseSkill()
        {
            Mp -= skills[SelSkillNum].MP;
        }
        public bool CanUseSkill(int skillNum) // 해당 스킬이 사용 가능한가
        {
            if (skillNum >= skills.Count) return false; // 스킬 인덱스 초과

            if (Mp - skills[skillNum].MP < 0) return false; // 사용할 마나가 안되면 false

            //Mp -= skills[skillNum].MP;
            return true;
        }
        // 취소 할 수도 있으니 마나 감소는 플레이어 공격씬 들어갈 때
    }
}