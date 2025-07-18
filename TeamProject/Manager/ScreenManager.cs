using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    // WinAPI를 가져와 더블 버퍼를 구현하려 했으나, 한글이 깨지는 이슈로 사용 X
    internal class ScreenManager
    {
        private const int Width = 120;
        private const int Height = 36;

        private IntPtr[] buffers = new IntPtr[2];
        private int currentIndex = 0;

        private CHAR_INFO[] charBuffer; // 유니코드 출력용 버퍼

        private static ScreenManager? instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        private ScreenManager()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // 한글 출력을 위한 설정
            Console.CursorVisible = false;

            for (int i = 0; i < 2; i++)
            {
                buffers[i] = CreateConsoleScreenBuffer(
                    0x40000000 | 0x80000000, // GENERIC_READ | GENERIC_WRITE
                    0x00000001 | 0x00000002, // FILE_SHARE_READ | FILE_SHARE_WRITE
                    IntPtr.Zero,
                    1, // CONSOLE_TEXTMODE_BUFFER
                    IntPtr.Zero);
            }

            charBuffer = new CHAR_INFO[Width * Height];
            Clear(); // 초기 버퍼 초기화
        }

        // 출력용 구조체
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CHAR_INFO
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
            public COORD(short x, short y) { X = x; Y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        // WinAPI 함수
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateConsoleScreenBuffer(
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwFlags,
            IntPtr lpScreenBufferData);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleActiveScreenBuffer(IntPtr hConsoleOutput);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
            IntPtr hConsoleOutput,
            [MarshalAs(UnmanagedType.LPArray), In] CHAR_INFO[] lpBuffer,
            COORD dwBufferSize,
            COORD dwBufferCoord,
            ref SMALL_RECT lpWriteRegion);


        // 화면 초기화 및 출력
        public void Clear(char ch = ' ')
        {
            /*StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    sb.Append(' ');
                sb.Append('\n');
            }*/
            //Console.Clear();

            /*WriteConsoleOutputCharacter(buffers[currentIndex], sb.ToString(), (uint)sb.Length, new COORD(0, 0), out _);
            SetConsoleCursorPosition(buffers[currentIndex], new COORD(0, 0));*/

            /*COORD origin = new COORD(0, 0);
            FillConsoleOutputCharacter(buffers[currentIndex], ' ', Width * Height, origin, out _);*/

            for (int i = 0; i < charBuffer.Length; i++)
            {
                charBuffer[i].UnicodeChar = ch;
                charBuffer[i].Attributes = 7; // 기본 흰색 (White on Black)
            }
        }

        // 지정 좌표에 문자열 출력
        public void Draw(int x, int y, string text)
        {
            int cx = x;
            int cy = y;

            foreach (char ch in text)
            {
                if (ch == '\n')
                {
                    cx = x;
                    cy++;
                }
                else if (ch == '\r')
                {
                    cx = x;
                }
                else
                {
                    if (cx >= 0 && cy >= 0 && cx < Width && cy < Height)
                    {
                        int index = (cy * Width) + cx;
                        charBuffer[index].UnicodeChar = ch;
                        charBuffer[index].Attributes = 7;
                    }
                    cx++;
                }
            }
        }

        // 화면 갱신 (버퍼 스왑)
        public void Flip()
        {
            IntPtr backBuffer = buffers[currentIndex];

            COORD bufferSize = new COORD((short)Width, (short)Height);
            COORD bufferCoord = new COORD(0, 0);
            SMALL_RECT region = new SMALL_RECT
            {
                Left = 0,
                Top = 0,
                Right = (short)(Width - 1),
                Bottom = (short)(Height - 1)
            };

            WriteConsoleOutput(backBuffer, charBuffer, bufferSize, bufferCoord, ref region);
            SetConsoleActiveScreenBuffer(backBuffer);

            currentIndex ^= 1; // 0 ↔ 1
        }
    }
}
