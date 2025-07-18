using System;

namespace TeamProject
{
    public abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }         // 통일: Lv(X), Level(O)
        public float MaxHp { get; set; }
        public float Hp { get; set; }
        public float AtkPower { get; set; }    // 통일: Atk(X), AtkPower(O)
        public float DefPower { get; set; }    // 통일: Def(X), DefPower(O)
        public string Description { get; set; }
        public bool isDie { get; private set; } // 죽었는지 여부 확인 (true=죽음/false=생존)

        public MonsterIndex Index { get; set; }

        public enum MonsterIndex
        {
            AA, ShadowImp, DarkGuardian, PaleWhisp, AbyssLord
        }

        protected Random rand = new Random();

        public List<Skill> skills { get; private set; }
        public int SelSkillNum { get; set; }

        protected Character()
        {
            Name = "";
            Level = 1;
            MaxHp = 100;
            Hp = MaxHp;
            AtkPower = 10;
            DefPower = 5;
            Description = "";
            isDie = false;
            skills = new List<Skill>();
            SelSkillNum = 0;
        }

        protected Character(string name, int level, float maxHp, float atkPower, float defPower, MonsterIndex index, string description = "")
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            Hp = maxHp;
            AtkPower = atkPower;
            DefPower = defPower;
            Description = description;
            isDie = false;
            skills = new List<Skill>();
            SelSkillNum = 0;
        }
        protected Character(Character unit)
        {
            Name = unit.Name;
            Level = unit.Level;
            MaxHp = unit.MaxHp;
            Hp = unit.Hp;
            AtkPower = unit.AtkPower;
            DefPower = unit.DefPower;
            Description = unit.Description;
            Index = unit.Index;
            isDie = unit.isDie;
            SelSkillNum = 0;
            skills = new List<Skill>();
            for (int i = 0; i < unit.skills.Count; i++)
            {
                skills.Add(new Skill(unit.skills[i], unit));
            }
        }

        public virtual int DamageTaken(TeamProject.Skill skill, out bool isHit, out bool isCritical)
        {
            int tmpDam = 0;
            int check = rand.Next(10);
            isCritical = false;
            isHit = true;

            // 스킬 공격은 회피 불가
            if (skill.Type == Skill.SkillType.AttackSkill || check != 6) // 스킬이거나 회피가 발동 안했다면
            {
                int tmpAtk = rand.Next((int)(skill.skillDamage - skill.skillDamage * 0.1f),
                        (int)(skill.skillDamage * 0.1f >= 0.5f ? (int)(skill.skillDamage + skill.skillDamage * 0.1f + 1) : (int)(skill.skillDamage + skill.skillDamage * 0.1f)));

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
                    QuestManager.Instance.KillCuntsUp(this.Index);
                    isDie = true;
                }
            }
            else isHit = false; // 노멀 공격이며 회피 발동 시, 데미지 계산 x, 

            return tmpDam;
        }

        public Skill GetUseSkill()
        {
            return skills[SelSkillNum];
        }
    }
}
