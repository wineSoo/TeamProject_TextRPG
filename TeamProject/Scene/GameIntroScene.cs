using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    // 게임 스타트 씬에 통합됨 - 현재 사용 X
    internal class GameIntroScene : Scene
    {
        int selOption = 0; // 0: START GAME, 1: QUIT
        readonly string[] menuOptions = { "게임시작", "종료" };
        public GameIntroScene()
        {
            title  = "================================================================================================\n";
            title += "                        **DEMON HUNTER RPG**                               \n";
            title += "\n";
            title += "██████╗ ███████╗███╗   ███╗ ██████╗ ███╗   ██╗\n"; 
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
            title += "================================================================================================\n";
            sb = new StringBuilder();
        }
        int titleIdx = -2;
        string title;
        int speed = 150; // 숫자 높을수록 느리게 출력됩니다.
        StringBuilder sb;
        // 0.3초마다 한 글자 씩 출력 되도록

        public override void Render()
        {
            sb.Clear();
            if (titleIdx < title.Length)
            {
                titleIdx += 20;
                if (titleIdx > title.Length) titleIdx = title.Length;
            }

            for (int i = 0; i < titleIdx; i++)
                sb.Append(title[i]);

            if (titleIdx == title.Length)
            {
                sb.AppendLine();
                // 메뉴 한 줄에 커서 표시
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (selOption == i) sb.Append("▶ ");
                    else sb.Append("  ");
                    sb.Append($"[{i + 1}] {menuOptions[i]}  ");
                }
                sb.AppendLine();
                sb.AppendLine("이동: 방향키, 선택: Z");
                Console.Clear();
                Console.Write(sb.ToString());
                SceneControl();
            }
            else
            {
                Console.Clear();
                Console.Write(sb.ToString());
                Thread.Sleep(speed);
            }
        }

        protected override void SceneControl()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                // ←, → 키로 커서 이동
                if (keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selOption = (selOption - 1 + menuOptions.Length) % menuOptions.Length;
                    Render();
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selOption = (selOption + 1) % menuOptions.Length;
                    Render();
                    break;
                }
                // Z키로 선택
                else if (keyInfo.Key == ConsoleKey.Z)
                {
                    if (selOption == 0)
                    {
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.JobSelectScene;
                    }
                    else if (selOption == 1)
                    {
                        Console.WriteLine("\n게임을 종료합니다. 감사합니다!");
                        Environment.Exit(0);
                    }
                    break;
                }
            }
        }

        public override void SetupScene()
        {
            base.SetupScene();
            sb.Clear();
            titleIdx = -2;
            selOption = 0;
        }
        /*public override void SetupScene()
        {
            base.SetupScene();
            sb.Clear(); // 혹시 다시 인트로 씬으로 오더라도 글자 처음부터 출력되게 하기
            titleIdx = -2;
        }
        */

        /*
        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        }
        */
    }
    
}
