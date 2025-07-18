using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class AlphaStrike : Skill
    {
        public AlphaStrike(Character owner) //: base(owner) -> 어차피 재지정해야 하므로 부모의 기본 생성자 호출
        {
            Name = "알파 스트라이크";
            MP = 10;
            ownChar = owner;
            target = 1;
            Type = SkillType.AttackSkill;
            Target = SkillTarget.Single;
            SetDamge();
        }
        public override void SetDamge()
        {
            // 공격력 * 2
            if (ownChar == null) return;
            skillDamage = (int)(ownChar.AtkPower * 2);
            Description = $"{skillDamage}의 데미지로 하나의 적을 공격합니다.";
        }
    }
}
