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

            monster = new Monster("대포미니언", 99, 124, 10, 5, "");

        }
        float initialHp = Player.Instance.Hp;
        StringBuilder sb;
        int selOptions = 0;

        //받아와야 하는거
        //현재 살아있는 몬스터 목록
        Monster monster;


        public override void Render()
        {
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

            sb.AppendLine($"Lv.{Player.Instance.Lv} {Player.Instance.Name}");
            sb.AppendLine($"Hp {initialHp} -> {Player.Instance.Hp}");
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

                    // if (Player.Instance.Hp <= 0) SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene; //게임 오버 씬
                    // else if (행동하지 않은 몬스터가 있으면) SceneManager.Instance.SetSceneState = SceneManager.SceneState.EnemyAttckScene // 다음 몬스터 행동
                    // else if (행동하지 않은 몬스터는 없지만 살아있는 몬스터가 있으면) SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene
                    // else (행동하지 않은 몬스터도 없고 살아있는 몬스터도 없으면 VictoryScene
                    break;
                case ConsoleKey.X:
                    break;
                default:
                    break;
            }
        }
    }
}
