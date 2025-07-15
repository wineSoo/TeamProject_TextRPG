using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject
{
    internal class BattleManager
    {
        private Player player;

        public int Gold { get; set; }// 골드
        public int Exp { get; set; } // 경험치

        int battleCount = 0;

        int optionnum = 0;
        
        public BattleManager(Player player)
        {
            this.player = player;
        }

        public void BattleScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                // 몬스터 정보 가져와서 출력 // 3마리
                for (int i = 0; i < enemy.Count; i++)
                {
                    Monster m = enemy[i];

                    if (i == optionnum)
                        Console.WriteLine($"▶ Lv.{m.Lv} {m.Name} (HP: {m.Hp})");
                    else
                        Console.WriteLine($"  Lv.{m.Lv} {m.Name} (HP: {m.Hp})");
                }
                Console.WriteLine();

                Console.WriteLine("[내정보]");
                // 레벨
                Console.WriteLine($"Lv.1 {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/100");
                Console.WriteLine();
                Console.WriteLine("이동: 방향키, 선택:z, 돌아가기: x");
                // 몬스터를 선택하면 공격 시작
                BattleControl();

            }

        }

        public void BattlePhase()
        {   //// 먼저 플레이어 차례 / 플레이어 이름   
            Console.WriteLine($"{player.Name}의 공격!");
            // 몬스터 이름, 플레이어 공격력
            Console.WriteLine($"Lv.2 미니언 을(를) 맞췄습니다. [데미지 : 10]");
            // 해당 몬스터 체력 줄이기
            // 몬스터 이름, HP
            Console.WriteLine($"Lv.2 미니언 \n HP가 10 -> 5");
            // 몬스터가 죽었는지 확인
            if (true) // 몬스터 피가 0이하인지 확인
            {
                // 몬스터 이름, HP
                Console.WriteLine($"Lv.2 미니언 \n HP가 5 -> dead");
                // 죽었으면 경험치, 골드 획득
                Console.WriteLine($"경험치 10 획득!, 골드 5 획득!");
            }
            // 몬스터가 모두 죽었는지 확인
            if (true)
            {
                //모두 죽었으면 종료
                WInBattle();
            }

            //// 몬스터 차례
            // 몬스터 이름
            Console.WriteLine($"Lv.2 미니언의 공격!");
            // 플레이어 이름, 몬스터 공격
            Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : 5]");
            // 플레이어 체력 줄이기
            // 플레이어 레벨, 이름
            Console.WriteLine($"Lv.1 {player.Name}");
            // 플레이어 HP
            Console.WriteLine($"HP 100 -> 95");
            // 플레이어가 죽었는지 확인
            if (true) // 플레이어 피가 0이하인지 확인
            {
                // 게임 오버 처리
                LoseBattle();
            }

        }
        public void WInBattle()
        {
            // 전투 종료
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("Victory");
            // 몇 마리 잡았는지
            Console.WriteLine($"던전에서 몬스터 3마리를 잡았습니다.");
            // 경험치, 이름
            Console.WriteLine($"Lv.1 chad");
            //체력. 골드
            Console.WriteLine($"HP 100 -> 96\n골드 100 ->120");
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

        Random random = new Random();
        // 플레이어가 주는 데미지
        public void BattleDamage()
        {
            // 플레이어 공격력
            // 플레이어 데미지 계산
            int margin = (int)Math.Round(player.AtkPower * 0.1);

            int min = (int)player.AtkPower - margin;
            int max = (int)player.AtkPower + margin + 1;

            int playerAttackdamage = random.Next(min, max);
            // 플레이어 방어력??

            //몬스터 공격력
            int monsterAttack;
        }
        Random randomM = new Random();
        // 몬스터 목록을 가져와서 랜덤으로 섞어주기
        public void MonsterToEnemy()
        {
            List<Monster> enemy = new List<Monster>();

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(MonsterList.AllMonsters.Count);
                enemy.Add(MonsterList.AllMonsters[index]);
            }

        }

        void BattleControl()
        {
            ControlManager.InputKey();

            InputKey input = KeyListener.GetKey();

            switch (input)
            {
                case InputKey.Up:
                    if (optionnum > 0) optionnum--;
                    break;
                case InputKey.Down:
                    if (optionnum < enemies.Length - 1) optionnum++;
                    break;
                case InputKey.Z:
                    BattlePhase();
                    break;
                case InputKey.X:
                    break;
            }
        }
        public enum InputKey
        {
            None,
            Up,
            Down,
            Left,
            Right,
            Z,
            X,
            Escape
        }

        public static class KeyListener
        {
            public static InputKey GetKey()
            {
                if (!Console.KeyAvailable) return InputKey.None;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                return keyInfo.Key switch
                {
                    ConsoleKey.UpArrow => InputKey.Up,
                    ConsoleKey.DownArrow => InputKey.Down,
                    ConsoleKey.LeftArrow => InputKey.Left,
                    ConsoleKey.RightArrow => InputKey.Right,
                    ConsoleKey.Z => InputKey.Z,
                    ConsoleKey.X => InputKey.X,
                    ConsoleKey.Escape => InputKey.Escape,
                    _ => InputKey.None
                };
            }

        }
    }
}

