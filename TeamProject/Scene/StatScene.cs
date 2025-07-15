using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class StatScene : Scene
    {
        public StatScene()
        {
            sb = new StringBuilder();
            options.Add("0. 나가기");
            optionsLen = options.Count;
        }
        StringBuilder sb;
        int selOptions = 0;
        public override void Render()
        {
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("상태 보기");
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화
            sb.Clear();

            sb.AppendLine("캐릭터의 정보가 표시됩니다.");
            sb.AppendLine();
            sb.Append("Lv. ");
            //sb.AppendLine("01"); // 플레이어 레벨 가져오기 "01" -> Player.instance.lev
            sb.AppendLine(Player.Instance.Lv.ToString());
            sb.Append(Player.Instance.Name); // 플레이어 이름
            sb.AppendLine(" ( 전사 )"); // 플레이어 이름
            sb.Append("공격력 : ");
            sb.AppendLine("10"); // 플레이어 공격력
            sb.Append("방어력 : ");
            sb.AppendLine("5"); // 플레이어 방어력
            sb.Append("체 력 : ");
            sb.AppendLine("100"); // 플레이어 체력
            sb.Append("Gold : ");
            sb.Append("1500"); // 플레이어 골드
            sb.AppendLine(" G");
            sb.AppendLine();
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
                case ConsoleKey.Z: // 나가기 선택
                case ConsoleKey.X: // 나가기
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                default:
                    break;
            }
        }
    }
    
}
