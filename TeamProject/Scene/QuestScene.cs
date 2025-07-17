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
            options.Add($"1. 마을을 위협하는 미니언 처치");
            options.Add($"2. 장비를 장착해보자");
            options.Add($"3. 더욱 더 강해지기\n");
            options.Add($"처음으로");
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
                    if (selOptions >= 0 && selOptions <= 2)
                    {
                        QuestManager.Instance.SelectQuestVariable = selOptions+1;
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.SelectQuestScene;
                    }
                    else if (selOptions == 3)
                    {
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    }
                    break;
                case ConsoleKey.X:
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                default:
                    break;
            }

        }
    }
}
