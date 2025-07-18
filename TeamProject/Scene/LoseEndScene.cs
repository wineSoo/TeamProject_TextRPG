using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    
    internal class LoseEndScene : Scene
    {
        private Player player;
        public LoseEndScene()
        {
            this.player = Player.Instance;
            sb = new StringBuilder();
            options.Add("처음으로");
            optionsLen = options.Count;
        }

        StringBuilder sb;
        int selOptions = 0;
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!! - Result");
            sb.AppendLine();
            sb.AppendLine("You Lose");
            sb.AppendLine();

            sb.AppendLine($"Lv.{player.Level}");
            sb.AppendLine($"HP {player.BattleStartHp} -> Dead");


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
                        case 0: // 스타트씬으로 
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
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
        public override void FinishScene()
        {
            Player.Instance.BattleFinish();
        }
    }
}

    

