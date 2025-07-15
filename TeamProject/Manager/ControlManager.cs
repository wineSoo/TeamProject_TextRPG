using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TeamProject.InputNameScene;

namespace TeamProject
{
    internal static class ControlManager
    {
        public static void ClearInputBuffer() // 씬 넘어가기전 현재 입력된 모든 입력 값 없애기
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        // 키 입력 예시
        public static void InputKey()
        {
            //if (Console.KeyAvailable)
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
}
