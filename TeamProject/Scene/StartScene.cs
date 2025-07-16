using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class StartScene : Scene
    {
        public StartScene()
        {
            sb = new StringBuilder();
            options.Add("1. 상태 보기");
            options.Add($"2. 전투 시작 (현재 진행 : {Player.Instance.DungeonFloor} 층)");
            optionsLen = options.Count;
        }
        StringBuilder sb;
        int selOptions = 0;
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.");
            sb.AppendLine("이제 전투를 시작할 수 있습니다.");
            sb.AppendLine();
            for (int i = 0; i < optionsLen; i++)
            {
                if(selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
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
                    switch (selOptions)
                    {
                        case 0: // 상태 보기
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.StatScene;
                            break;
                        case 1: // 전투 시작
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
                            break;
                        default:
                            break;
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
            options.Clear();
            options.Add("1. 상태 보기");
            options.Add($"2. 전투 시작 (현재 진행 : {Player.Instance.DungeonFloor} 층)");
            optionsLen = options.Count;
        }
    }
}
