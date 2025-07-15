using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class BattleScene : Scene
    {
        StringBuilder sb;
        int selOptions = 0;

        private List<Monster> enemy = new List<Monster>();
        private MonsterLibrary monsterLibrary;
        public BattleScene()
        {
            sb = new StringBuilder();
            monsterLibrary = new MonsterLibrary();
        }
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!!");
            sb.AppendLine();

            for (int i = 0; i < enemy.Count; i++)
            {
                Monster m = enemy[i];

                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

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
                    if (selOptions != enemy.Count - 1) selOptions++;
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.Z:
                    switch (selOptions)
                    {
                        case 0: // 1번 몬스터 선택
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                            break;
                        case 1: // 2번 몬스터 선택
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                            break;
                        case 2: // 3번 몬스터 선택
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
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
