using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject
{
    public class BattleManager
    {
        private InputNameScene inputNameScene;

        int battleCount = 0;

        public BattleManager()
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

            PlayerBattle();
            if (battleCount == 1)
            {
                Console.WriteLine("몬스터가 모두 죽었습니다. 전투를 종료합니다.");
                int battleCount = 0;
                WInBattle();
            }
            else
            {
                EnemyBattle();
            }

        }

        public void PlayerBattle()
        {   // 플레이어 이름   
            Console.WriteLine($"chad의 공격!");
            // 플레이어 공격력
            Console.WriteLine($"Lv.2 미니언 을(를) 맞췄습니다. [데미지 : 10]");
            // 몬스터 이름, HP
            Console.WriteLine($"Lv.2 미니언 \n HP가 10 -> 5");
            // 몬스터가 죽었는지 확인
            Console.WriteLine($"Lv.2 미니언 \n HP가 5 -> dead");
            // 죽었으면 경험치, 골드 획득
            if (true)
            {
                Console.WriteLine($"경험치 10 획득!, 골드 5 획득!");
            }
            if (true) // 몬스터가 모두 죽었는지 확인
            {
                int battleCount = 1;
                Console.WriteLine($"모든 몬스터를 처치했습니다!");
                // 다시 시작 씬으로 돌아가기

            }
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
            // 다시 시작 씬으로 돌아가기

        }
        
        public void WInBattle()
        {
            // 전투 종료
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("Victory");
            // 몇 마리 잡았는지
            Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.");
            // 경험치, 이름
            Console.WriteLine("Lv.1 chad");
            //체력. 골드
            Console.WriteLine("HP 100 -> 96\n골드 100 ->120");
            // 다시 시작 씬으로 돌아가기
            Console.WriteLine("0. 다음");
        }
        public void LoseBattle()
        {
            // 전투 종료
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("You Lose");
            // 경험치, 이름
            Console.WriteLine("Lv.1 chad");
            //체력. 골드
            Console.WriteLine("HP 100 -> 96\n골드 100 ->120");
            // 다시 시작 씬으로 돌아가기
            Console.WriteLine("0. 다음");
        }

        private Random random = new Random();

        public void BattleDamage()
        {
            // 플레이어 공격력
            // 플레이어 데미지 계산
            int margin = (int)Math.Round(playerAttack * 0.1);

            int min = playerAttack - margin;
            int max = playerAttack + margin + 1;

            int playerAttackdamage = random.Next(min, max);
            // 플레이어 방어력?

            //몬스터 공격력 가져오기
            int monsterAttack; // 몬스터 공격력
        }

    }
}

