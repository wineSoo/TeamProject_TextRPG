using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class BattleScene : Scene
    {
        public BattleScene()
        {
            sb = new StringBuilder();
            options.Add("0. 도망치기");
            optionsLen = options.Count;
        }
        StringBuilder sb;
        int selOptions = 0;

        List<Monster> enemy = new List<Monster>();

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            // 싸울 몬스터 가져옴
            Monster testMonster1 = new Monster("고블린", 2, 30, 5, 2, "작고 약한 몬스터"); //임시
            Monster testMonster2 = new Monster("고블", 2, 30, 5, 2, "작고 약한 몬스터"); //임시
            Monster testMonster3 = new Monster("고", 2, 30, 5, 2, "작고 약한 몬스터"); //임시
            enemy = new List<Monster>();
            enemy.Add(testMonster1);
            enemy.Add(testMonster2);
            enemy.Add(testMonster3);


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
