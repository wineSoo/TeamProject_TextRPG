using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class QuestScene : Scene
    {
       public QuestScene()
        {
            sb = new StringBuilder();
            options.Add("1. 퀘스트 1");
            options.Add("2. 퀘스트 2");
            options.Add("2. 퀘스트 3");
            optionsLen = options.Count;
        }

        StringBuilder sb;
        int selOptions = 0;

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Quest!!");

            sb.AppendLine();
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
                        case 0: // 퀘스트 1

                            break;
                        case 1: // 퀘스트 2

                            break;
                        case 2: // 퀘스트 3

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
    }
}
