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
            sbName = new StringBuilder();
            CreatScene();
            state = InputState.Input;

        }
        public enum InputState
        {
            Input, NextScene
        }
        InputState state { get; set; }

        StringBuilder sb;
        StringBuilder sbName;
        
        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    //state = InputState.NextScene;
                    Console.WriteLine("1초 뒤 다음 씬으로");
                    Console.CursorVisible = false;
                    Thread.Sleep(1000);
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.GameIntroScene;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && sbName.Length > 0)
                {
                    sbName.Length--;
                }
                else if (!char.IsControl(keyInfo.KeyChar)) // 유효한 문자만
                {
                    sbName.Append(keyInfo.KeyChar);
                }

            }
            /*switch (state)
            {
                case InputState.Input:
                    
                    break;
                case InputState.NextScene:
                    Console.WriteLine("1초 뒤 복구");
                    Thread.Sleep(1000);
                    state = InputState.Input;
                    //SceneManager.Instance.SetSceneState = SceneManager.SceneState.;
                    break;
                default:
                    break;
            }*/
        }
        public override void Render()
        {
            sb.Clear();
            sb.Append("이름을 입력해주세요. (Enter로 완료): ");
            /*Console.Write(sb);
            Console.Clear();
            string name = Console.ReadLine();
            sb.AppendLine(name);*/
            //sb.Append(sbName.ToString());
            sb.Append(sbName);
            Console.Write(sb);
        }
        void CreatScene()
        {
            sb.Append("이름을 입력해 주세요: ");
        }
        

    }
}
