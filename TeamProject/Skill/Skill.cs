using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    public class Skill
    {
        public string Name; // 스킬 이름
        public int Atk;// 스킬 데이지
        public int PP; // 스킬 사용 횟수
        public int MP; // 소모 마나량
        public string Description; // 스킬 설명
        public SkillType Type; // 스킬 타입
        protected Character? ownChar;
        public int skillDamage { get; protected set; }
        public int target {  get; protected set; }
        public SkillTarget Target { get; protected set; }
        public enum SkillType
        {
            Normal, // 기본 공격
            AttackSkill, // 마법 공격
            HealSKill, // 치유
        }
        public enum SkillTarget
        {
            Single, Multi, RandomMulti
        }
        public Skill()
        {
            Name = "";
            Description = "";
            MP = 0;
        }
        public Skill(string name, int atk, int pp, string description, SkillType type)
        {
            Name = name;
            Atk = atk;
            this.PP = pp;
            Description = description;
            Type = type;
        }
        public Skill(Skill oriSkill, Character owner)
        {
            Name = oriSkill.Name;
            Description = oriSkill.Description;
            Type = oriSkill.Type;
            Atk = oriSkill.Atk;
            PP = oriSkill.PP;
            skillDamage = 0;
            target = 1;
            Target = oriSkill.Target;
            MP = oriSkill.MP;
            this.ownChar = owner;
            SetDamge();
        }

        // 구조 변경용 생성자
        public Skill(Character owner)
        {
            Name = "기본 공격";
            Description = "적에게 기본 공격을 한다.";
            this.ownChar = owner;
            PP = 30;
            Type = SkillType.Normal;
            target = 1;
            MP = 0;
            Target = SkillTarget.Single;
            SetDamge();
        }
        public virtual void SetDamge()
        {
            // 기본 공격 스킬 데미지는 소유자 공격력 그대로
            if (ownChar == null) return;
            skillDamage = (int)ownChar.AtkPower;
        }


        public bool SkillAvailable()
        {
            if (PP <= 0)
            {
                Console.WriteLine($"{Name} 스킬의 PP가 부족합니다!");
                return false;
            }
            PP--;
            Console.WriteLine($"{Name} 스킬을 사용했습니다. 남은 PP: {PP}");
            return true; 
        }
    }
}
