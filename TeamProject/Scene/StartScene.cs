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
        }
        StringBuilder sb;
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("스타트 씬");
            Console.Write(sb.ToString());
            SceneControl();
        }

        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
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
            }
        }
    }
}
