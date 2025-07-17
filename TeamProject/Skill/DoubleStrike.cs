using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class DoubleStrike : Skill
    {
        public DoubleStrike(Character owner) //: base(owner) -> 어차피 재지정해야 하므로 부모의 기본 생성자 호출
        {
            Name = "더블 스트라이크";
            MP = 15;
            ownChar = owner;
            target = 2;
            Type = SkillType.AttackSkill;
            Target = SkillTarget.RandomMulti;
            SetDamge();
        }
        public override void SetDamge()
        {
            // 공격력 * 2
            if (ownChar == null) return;
            skillDamage = (int)(ownChar.AtkPower * 1.5);
            Description = $"{skillDamage}의 데미지로 2명의 적을 랜덤으로 공격합니다.";
        }
    }
}
