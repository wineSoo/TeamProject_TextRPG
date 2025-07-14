using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    public class BattleManager
    {

        private void Battle
        {

        }

        public void BattleScene()
        {
            Console.WriteLine("Battle!!");
            // 몬스터 정보 가져와서 출력 // 3마리
            Console.WriteLine($"Lv.2 미니언");
            Console.WriteLine($"Lv.2 미니언");
            Console.WriteLine($"Lv.2 미니언");
            // 몬스터를 선택하면 공격 시작

            Console.WriteLine("[내정보]");
            // 레벨, 이름, 직업, HP
            Console.WriteLine($"Lv.1 chad (전사)");
            Console.WriteLine($"HP 100/100");

        }

        public void PlayerBattle()
        {   // 플레이어 이름   
            Console.WriteLine($"chad의 공격!");
            // 플레이어 공격력
            Console.WriteLine($"Lv.2 미니언 을(를) 맞췄습니다. [데미지 : 10]");
            // 몬스터 이름, HP
            Console.WriteLine($"Lv.2 미니언 \n HP가 10 -> 5");
            // 몬스터가 죽었는지 확인
            // 죽었으면 경험치, 골드 획득
            // 모두 죽었으면 전투 종료
            Console.WriteLine($"Lv.2 미니언 \n HP가 5 -> dead");

        }

        public void EnemyBattle()
        {   // 몬스터 이름
            Console.WriteLine($"Lv.2 미니언의 공격!");
            // 플레이어 이름, 몬스터 공격
            Console.WriteLine($"chad 을(를) 맞췄습니다. [데미지 : 5]");
            // 플레이어 레벨, 이름
            Console.WriteLine($"Lv.1 chad");
            // 플레이어 HP
            Console.WriteLine($"HP 100 -> 95");
            // 적 턴 끝나면 다시 시작 씬

        }



    }
}
