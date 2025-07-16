using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Skill
    {
        public string Name; // 스킬 이름
        public int Atk;// 스킬 데이지
        public int pp; // 스킬 사용 횟수
        public string Description; // 스킬 설명
        public SkillType Type; // 스킬 타입

        public enum SkillType
        {
            Normal, // 기본 공격
            AttackSkill, // 마법 공격
            HealSKill, // 치유
        }
        public Skill() {
        
        }
        public Skill(string name, int atk, int pp, string description, SkillType type)
        {
            Name = name;
            Atk = atk;
            this.pp = pp;
            Description = description;
            Type = type;
        }

        public bool SkillAvailable()
        {
            if (pp <= 0)
            {
                Console.WriteLine($"{Name} 스킬의 PP가 부족합니다!");
                return false;
            }
            pp--;
            Console.WriteLine($"{Name} 스킬을 사용했습니다. 남은 PP: {pp}");
            return true; 
        }
    }
}
