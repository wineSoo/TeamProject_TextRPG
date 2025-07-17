using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class JobSelectScene : Scene
    {
        public JobSelectScene() 
        {
            sb = new StringBuilder();
            options.Add("1. 전사: 강인한 체력으로 적의 공격을 여러 번 버틸 수 있습니다.");
            options.Add("2. 궁수: 뛰어난 기술을 가지고 있어서 치명타율이 높습니다. ");
            options.Add("3. 도적: 빠른 속도로 적의 공격을 좀 더 잘 피할 수 있습니다.");
            options.Add("4. 마법사: 높은 MP를 가지고 있어 스킬을 여러 번 쓸 수 있습니다.");
            optionsLen = options.Count;

        }
        StringBuilder sb;
        int selOptions = 0;

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("직업을 선택해 주세요\n");
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
                        case 0: // 전사
                            Player.Instance.StatInitializer(Player.PlayerJob.Warrior);
                            break;
                        case 1: // 궁수
                            Player.Instance.StatInitializer(Player.PlayerJob.Archer);
                            break;
                        case 2: // 도적
                            Player.Instance.StatInitializer(Player.PlayerJob.Theif);
                            break;
                        case 3: // 마법사
                            Player.Instance.StatInitializer(Player.PlayerJob.Mage);
                            break;
                        default:
                            break;
                    }
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                case ConsoleKey.X:
                    break;
                default:
                    break;
            }
        }
    }
}
