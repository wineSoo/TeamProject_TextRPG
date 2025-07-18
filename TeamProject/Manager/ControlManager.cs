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
        public static float Lerp(float a, float b, float t)
        {
            t = Clamp01(t);
            return a + (b - a) * t;
        }
        public static float Clamp01(float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }
        public static int GetDisplayWidth(string s)
        {
            int width = 0;
            foreach (char c in s)
            {
                if (IsKorean(c))
                    width += 2;
                else
                    width += 1;
            }
            return width;
        }
        public static bool IsKorean(char c)
        {
            // 한글 완성형 범위: U+AC00 ~ U+D7A3
            return c >= 0xAC00 && c <= 0xD7A3;
        }
    }
}
