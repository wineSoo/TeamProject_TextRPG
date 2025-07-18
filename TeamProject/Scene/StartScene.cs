using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject.SceneManager;

namespace TeamProject
{
    internal class StartScene : Scene
    {
        public StartScene()
        {
            title  = "██████╗ ███████╗███╗   ███╗ ██████╗ ███╗   ██╗\n";
            title += "██╔══██╗██╔════╝████╗ ████║██╔═══██╗████╗  ██║\n";
            title += "██║  ██║█████╗  ██╔████╔██║██║   ██║██╔██╗ ██║\n";
            title += "██║  ██║██╔══╝  ██║╚██╔╝██║██║   ██║██║╚██╗██║\n";
            title += "██████╔╝███████╗██║ ╚═╝ ██║╚██████╔╝██║ ╚████║\n";
            title += "╚═════╝ ╚══════╝╚═╝     ╚═╝╚═════╝ ╚═╝  ╚═══╝\n";
            title += "██╗  ██╗██╗   ██╗███╗   ██╗████████╗███████╗██████╗ ███████╗\n";
            title += "██║  ██║██║   ██║████╗  ██║╚══██╔══╝██╔════╝██╔══██╗██╔════╝\n";
            title += "███████║██║   ██║██╔██╗ ██║   ██║   █████╗  ██████╔╝███████╗\n";
            title += "██╔══██║██║   ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗╚════██║\n";
            title += "██║  ██║╚██████╔╝██║ ╚████║   ██║   ███████╗██║  ██║███████║\n";
            title += "╚═╝  ╚═╝╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝\n";
            sb = new StringBuilder();
            sceneState = StartState.Showing;
            SetupScene();
        }
        enum StartState
        {
            Showing, Completed
        }
        StartState sceneState {  get; set; }

        StringBuilder sb;
        int selOptions = 0;
        int titleIdx = -2;
        string title;
        int speed = 150; // 숫자 높을수록 느리게 출력됩니다.
        public override void Render()
        {
            sb.Clear();
            switch (sceneState)
            {
                case StartState.Showing:
                    if (titleIdx < title.Length)
                    {
                        titleIdx += 20;
                        if (titleIdx > title.Length)
                        {
                            titleIdx = title.Length;
                        }
                    }

                    for (int i = 0; i < titleIdx; i++)
                    {
                        sb.Append(title[i]);
                    }
                    if (titleIdx == title.Length) // 다 출력 했다면
                    {
                        Console.WriteLine(sb.ToString());
                        Thread.Sleep(500);
                        sceneState = StartState.Completed;
                    }
                    else // 출력할 게 남았다면
                    {
                        Console.WriteLine(sb.ToString());
                        Thread.Sleep(speed); // ms 마다 한 글자씩
                    }
                    break;
                case StartState.Completed:
                    sb.AppendLine(title);
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
                    break;
                default:
                    break;
            }
            


            /*sb.AppendLine("스파르타 던전에 오신 여러분 환영합니다.");
            sb.AppendLine("이제 전투를 시작할 수 있습니다.");
            sb.AppendLine();*/
            /*for (int i = 0; i < optionsLen; i++)
            {
                if(selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.Write(sb.ToString());
            SceneControl();*/
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
                        case 1: // 인벤 토리
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.InventoryScene;
                            break;
                        case 2: // 전투 시작
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
                            break;
                        case 3: // 퀘스트 씬
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
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
            options.Add("2. 인벤 토리");
            options.Add($"3. 전투 시작 (현재 진행 : {Player.Instance.DungeonFloor} 층)");
            options.Add("4. 퀘스트");
            optionsLen = options.Count;
        }
    }
}
