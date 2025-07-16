using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class SkillLibrary
    {
        private static SkillLibrary instance;

        public static SkillLibrary Instance
        {
            get
            {
                if (instance == null)
                    instance = new SkillLibrary();
                return instance;
            }

        }

        private Player player;
        private List<Skill> skills;

        public SkillLibrary()
        {
            this.player = Player.Instance;
            skills = new List<Skill>();
            CreateSkills();
        }

        private void CreateSkills()
        {
            skills.Add(new Skill());
            skills[0].Name = "기본공격";
            skills[0].Atk = (int)player.AtkPower; // 기본 공격력
            skills[0].pp = 100; 
            skills[0].Description = "적에게 기본 공격을 한다.";
            skills[0].Type = Skill.SkillType.Normal;

            skills.Add(new Skill());
            skills[1].Name = "몸통 박치기";
            skills[1].Atk = 20;
            skills[1].pp = 1;
            skills[1].Description = "상대를 향해서 몸 전체를 부딪쳐가며 공격한다.";
            skills[1].Type = Skill.SkillType.AttackSkill;

            skills.Add(new Skill());
            skills[2].Name = "뛰어오르기";
            skills[2].Atk = 5;
            skills[2].pp = 0;
            skills[2].Description = "팔딱거린다.";
            skills[2].Type = Skill.SkillType.AttackSkill;

        }



        public List<Skill> GetAllSkills()
        {
            return new List<Skill>(skills);
        }


    }
}
