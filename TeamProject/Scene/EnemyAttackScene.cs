using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class EnemyAttackScene : Scene
    {
        
        public EnemyAttackScene()
        {
            sb = new StringBuilder();
            
            options.Add("0. 다음");
            optionsLen = options.Count;

            monster1 = new Monster("대포미니언", 1, 124, 10, 5, "");
            monster2 = new Monster("대포미니언", 2, 124, 10, 5, "");
            monster3 = new Monster("대포미니언", 3, 124, 10, 5, "");
            monsters.Add(monster1);
            monsters.Add(monster2);
            monsters.Add(monster3);
        }
        
        float initialHp = Player.Instance.Hp;
        StringBuilder sb;
        int selOptions = 0;

        //받아와야 하는거
        //현재 살아있는 몬스터 목록
        Monster monster1;
        Monster monster2;
        Monster monster3;
        List<Monster> monsters = new List<Monster>();


        public override void Render()
        {

            foreach (Monster monster in monsters)
            {

                StringBuilder sb0 = new StringBuilder();
                for (int i = 0; i < 36; i++)
                {
                    for (int j = 0; j < 120; j++)
                    {
                        sb0.Append(" ");
                    }
                    sb0.Append("\n");
                }
                string tmpS = sb0.ToString();

                // 깜빡임 줄이기 위해 빈공간으로 덮어 쓰기
                Console.SetCursorPosition(0, 0);


                //if(monster.isDied) continue;

                sb.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
                sb.AppendLine("Battle!!");
                Console.Write(sb.ToString());
                Console.ResetColor();// 출력 색 초기화

                sb.Clear();
                sb.AppendLine($"LV.{monster.Level} {monster.Name} 의 공격!");

                sb.Append($"{Player.Instance.Name} 을(를) 맞췄습니다.  ");
                sb.AppendLine($"[데미지:10]");
                sb.AppendLine();

                Player.Instance.Hp -= 10;


                sb.AppendLine($"Lv.{Player.Instance.Lv} {Player.Instance.Name}");
                sb.AppendLine($"Hp {initialHp} -> {Player.Instance.Hp}");

                initialHp = Player.Instance.Hp;

                for (int i = 0; i < optionsLen; i++)
                {
                    if (selOptions == i) sb.Append("▶ ");
                    else sb.Append("　 ");
                    sb.AppendLine(options[i]);
                }
                sb.AppendLine();
                sb.AppendLine("이동: 방향키, 선택: z, 돌아가기: x");
                Console.Write(sb.ToString());
                SceneControl();

                Console.SetCursorPosition(0, 0);
                Console.Write(tmpS);

            }

            // 잠깐 멈췄다가
            // toDo: 적 공격 턴 후, 적이 다 처치되었다면 시작씬으로, 아니라면 다시 배틀씬으로
            // 현재는 그냥 배틀 씬으로
            SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
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

                    // if (Player.Instance.Hp <= 0) //게임 오버 씬
                    // else if (살아있는 몬스터가 있으면) SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene
                    // else (살아있는 몬스터가 없으면) //승리 씬

                    break;
                case ConsoleKey.X:
                    break;
                default:
                    break;
            }
        }
    }
}
