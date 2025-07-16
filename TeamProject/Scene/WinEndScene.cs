using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    
    internal class WinEndScene : Scene
    {
        private Player player;
        public WinEndScene()
        {
            this.player = Player.Instance;
            sb = new StringBuilder();
            options.Add("다음전투");
            options.Add("처음으로");
            optionsLen = options.Count;
        }

        StringBuilder sb;
        int selOptions = 0;
        int totalMonsterNumber;
        int totalExpGained;
        int pastLevel;
        int pastExp;
        bool isLevelUp = false;

        void ResultCalculator()
        {
            Player.Instance.DungeonFloor++;
            pastLevel = Player.Instance.Lv;
            pastExp= Player.Instance.Exp;

            var monsters = MonsterManager.Instance.GetActiveMonsters();
            if (monsters != null)
            {
                totalMonsterNumber = monsters.Count;
                
                foreach (var monster in monsters)
                {
                    if (monster != null)
                    {
                        totalExpGained += monster.Level;
                    }
                }
            }
        }

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!! - Result");
            sb.AppendLine();
            sb.AppendLine("Victory");
            sb.AppendLine();
            sb.AppendLine($"{Player.Instance.DungeonFloor}층 던전에서 몬스터 {totalMonsterNumber}마리를 잡았습니다.");
            sb.AppendLine();
            sb.AppendLine("[캐릭터 정보]");
            if (isLevelUp) sb.AppendLine($"Lv.{pastLevel} {player.Name} -> Lv.{player.Lv} {player.Name}");
                else sb.AppendLine($"Lv.{pastLevel} {player.Name}");
            sb.AppendLine($"HP {player.BattleStartHp} -> {player.Hp}");
            sb.AppendLine($"exp {pastExp} -> {player.Exp}");

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
                        case 0: // 던전 다시 실행
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
                            break;
                        case 1: // 스타트씬으로 
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

        public override void SetupScene()
        {
            base.SetupScene();

            //경험치 계산
            ResultCalculator();
            isLevelUp = Player.Instance.LevelCalculator(totalExpGained);

            // 소환된 몬스터 초기화
            MonsterManager.Instance.ClearActiveMonsters();
        }


    }
}
