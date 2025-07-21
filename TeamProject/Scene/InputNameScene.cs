using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class InputNameScene : Scene
    {
        public InputNameScene()
        {
            sb = new StringBuilder();
            //CreatScene();
            state = InputState.Input;

        }
        public enum InputState
        {
            Input, NextScene
        }
        InputState state { get; set; }

        StringBuilder sb;
        string? stringName;

        //public override void Render()
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine();
            sb.AppendLine("  .-._                                                   _,-,");
            sb.AppendLine("    `._`-._                                           _,-'_,'");
            sb.AppendLine("       `._ `-._                                   _,-' _,'");
            sb.AppendLine("          `._  `-._        __.-----.__        _,-'  _,'");
            sb.AppendLine("             `._   `#===\"\"\"           \"\"\"===#'   _,'");
            sb.AppendLine("                `._/)  ._               _.  (\\_,'");
            sb.AppendLine("                 )*'     **.__     __.**     '*(");
            sb.AppendLine("                 #  .==..__  \"\"   \"\"  __..==,  #");
            sb.AppendLine("                  #   `\"._(_).       .(_)_.'\" #");
            sb.AppendLine("              ╔══════════════════════════════════════╗");
            sb.AppendLine("              ║      Character Name Creation         ║");
            sb.AppendLine("              ╠══════════════════════════════════════╣");
            sb.AppendLine("              ║                                      ║");
            sb.AppendLine("              ║        『 ENTER YOUR NAME 』         ║");
            sb.AppendLine("              ║                                      ║");
            sb.AppendLine("              ║  +------------------------------+    ║");
            sb.Append("              ║  |  ");
            int boxWidth = 26;
            string name = stringName ?? "";
            int padLeft = (boxWidth - name.Length) / 2;
            int padRight = boxWidth - name.Length - padLeft;

            sb.Append(new string(' ', padLeft));
            sb.Append(name);
            sb.Append(new string(' ', padRight));
            sb.AppendLine("  |    ║");
            sb.AppendLine("              ║  +------------------------------+    ║");
            sb.AppendLine("              ║                                      ║");
            sb.AppendLine("              ╠══════════════════════════════════════╣");
            sb.AppendLine("              ║           (Enter로 완료)             ║");
            sb.AppendLine("              ╚══════════════════════════════════════╝");
            sb.AppendLine();

            switch (state)
            {
                case InputState.Input:
                    Console.Write(sb);
                    SceneControl(); // 입력 대기
                    break;
                case InputState.NextScene:
                    sb.AppendLine("                     직업 선택으로 넘어갑니다.");
                    Console.Write(sb);
                    Thread.Sleep(1000);
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.JobSelectScene;
                    break;
                default:
                    break;
            }
        }
        protected override void SceneControl()
        {
            Console.SetCursorPosition(30, 17);

            stringName = Console.ReadLine();
            Console.CursorVisible = false;
            if(stringName != null) Player.Instance.Name = stringName;
            state = InputState.NextScene;
        }

        void CreatScene()
        {
            sb.Append("이름을 입력해 주세요: ");
        }
        public override void FinishScene()
        {
            Console.CursorVisible = false; // 커서 보이기 x
        }

    }   
}
