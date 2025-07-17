using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TeamProject.CharacterManager;

namespace TeamProject
{
    internal class EnemyAttackScene : Scene
    {
        
        public EnemyAttackScene()
        {
            sb = new StringBuilder();
            
            options.Add("0. 다음");
            optionsLen = options.Count;

            /* monster1 = new Monster("대포미니언", 1, 124, 10, 5, "");
             monster2 = new Monster("대포미니언", 2, 124, 10, 5, "");
             monster3 = new Monster("대포미니언", 3, 124, 10, 5, "");
             monsters.Add(monster1);
             monsters.Add(monster2);
             monsters.Add(monster3);*/

            sb0 = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 120; j++)
                {
                    sb0.Append(" ");
                }
                sb0.Append("\n");
            }
            tmpS = sb0.ToString();
        }
        
        //float initialHp = Player.Instance.Hp;
        StringBuilder sb;
        int selOptions = 0;
        StringBuilder sb0; // 덮어쓰기 용
        string tmpS;
        // 출력용 임시 저장 변수
        float initialHp;
        int renderDam = 0;
        // 패배 씬으로 넘어가기용 변수
        bool isGameOver = false;

        //받아와야 하는거
        //현재 살아있는 몬스터 목록
        /*Monster monster1;
        Monster monster2;
        Monster monster3;*/
        //List<Monster> monsters = new List<Monster>();
        List<Monster>? monsters;


        public override void Render()
        {
            if (monsters == null) return;

            foreach (Monster monster in monsters)
            {
                if (monster.isDie) continue; // 죽었다면 다음 미니언

                sb.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
                sb.AppendLine("Battle!!");
                Console.Write(sb.ToString());
                Console.ResetColor();// 출력 색 초기화

                sb.Clear();
                sb.AppendLine($"LV.{monster.Level} {monster.Name} 의 공격!");

                initialHp = (int)Player.Instance.Hp;
                renderDam = Player.Instance.DamageTaken(monster.AtkPower);
                sb.Append($"{Player.Instance.Name} 을(를) 맞췄습니다.  ");
                sb.AppendLine($"[데미지: {renderDam}]");
                sb.AppendLine();

                sb.AppendLine($"Lv.{Player.Instance.Level} {Player.Instance.Name}");
                sb.AppendLine($"Hp {(int)initialHp} -> {(int)Player.Instance.Hp}");

                for (int i = 0; i < optionsLen; i++)
                {
                    if (selOptions == i) sb.Append("▶ ");
                    else sb.Append("　 ");
                    sb.AppendLine(options[i]);
                }
                sb.AppendLine();
                sb.AppendLine("이동: 방향키, 선택: z");
                Console.Write(sb.ToString());
                SceneControl();

                Console.SetCursorPosition(0, 0);
                Console.Write(tmpS);
                Console.SetCursorPosition(0, 0);

                // 게임이 종료되면 몬스터 정보도 사라지기 때문에 반복문 종료하기
                if (isGameOver) break;
            }

            if (!isGameOver)
            {
                SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
            }
            else isGameOver = false; // 다음 배틀 상황이 올때 바로 게임 오버가 되지 않도록
        }

        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selOptions != 0) selOptions--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selOptions != optionsLen - 1) selOptions++;
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.Z:
                    // 매 턴마다 플레이어 체력 체크, 체력 0이면 게임 오버 씬으로
                    if (Player.Instance.Hp <= 0) //게임 오버 씬
                    {
                        Console.WriteLine("게임 오버 씬으로 체인지");
                        isGameOver = true;
                        Thread.Sleep(2000);

                        // 소환된 몬스터 초기화
                        MonsterManager.Instance.ClearActiveMonsters();
                        // 테스트로 스타트씬으로 변경
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;

                        //SceneManager.Instance.SetSceneState = SceneManager.SceneState.
                    }
                    break;
                case ConsoleKey.X:
                    break;
                default:
                    break;
            }
        }
        public override void SetupScene()
        {
            base.SetupScene();
            monsters = MonsterManager.Instance.GetActiveMonsters();
        }
    }
}
