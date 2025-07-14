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
        public override void Render()
        {
            sb.Clear();
            sb.Append("이름을 입력해주세요. (Enter로 완료): ");
            sb.AppendLine(sbName.ToString());
            Console.Write(sb);
        }

        public override void Update()
        {
            switch (state)
            {
                case InputState.Input:
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            state = InputState.NextScene;
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace && sbName.Length > 0)
                        {
                            sbName.Length--;
                        }
                        else if (!char.IsControl(keyInfo.KeyChar)) // 유효한 문자만
                        {
                            sbName.Append(keyInfo.KeyChar);
                        }

                        /*switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                //Console.WriteLine("↑ 위쪽 방향키 입력됨");
                                break;
                            case ConsoleKey.DownArrow:
                                //Console.WriteLine("↓ 아래쪽 방향키 입력됨");
                                break;
                            case ConsoleKey.LeftArrow:
                                //Console.WriteLine("← 왼쪽 방향키 입력됨");
                                break;
                            case ConsoleKey.RightArrow:
                                //Console.WriteLine("→ 오른쪽 방향키 입력됨");
                                break;
                            case ConsoleKey.Escape:
                                //Console.WriteLine("종료합니다.");
                                break;
                            case ConsoleKey.Z:
                                //Console.WriteLine("z");
                                break;
                            case ConsoleKey.X:
                                //Console.WriteLine("x");
                                break;
                            default:
                                //Console.WriteLine($"다른 키 입력됨: {keyInfo.Key}"
                                break;
                        }*/
                    }
                    break;
                case InputState.NextScene:
                    Console.WriteLine("1초 뒤 복구");
                    Thread.Sleep(1000);
                    state = InputState.Input;
                    //SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                default:
                    break;
            }
        }
        void CreatScene()
        {
            sb.Append("이름을 입력해 주세요: ");
        }
        

    }
}
