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

        private List<Monster>? enemy;
        
        public BattleScene()
        {
            this.player = Player.Instance;
            sb = new StringBuilder();
            SetupScene();
        }
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!!");
            sb.AppendLine();

            if (enemy == null) return;
            for (int i = 0; i < enemy.Count; i++)
            {
                Monster m = enemy[i];

                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

            }
            sb.AppendLine();
            sb.AppendLine("[내정보]");
            sb.AppendLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
            sb.AppendLine($"HP {player.Hp}/100");
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
                    if (enemy != null && selOptions != enemy.Count - 1) selOptions++;
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.Z:
                    MonsterManager.Instance.SelActiveMonstersNum = selOptions;
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                    /*switch (selOptions)
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
                    }*/
                    break;
                case ConsoleKey.X:
                    break;
                default:
                    break;
            }
        }
        public override void SetupScene()
        {
            base.SetupScene();
            if(enemy == null || enemy.Count == 0)
            {
                MonsterManager.Instance.SetBattleMonsters(3);
                enemy = MonsterManager.Instance.GetActiveMonsters();
                MonsterManager.Instance.SelActiveMonstersNum = -1;
            }
        }
    }
}
