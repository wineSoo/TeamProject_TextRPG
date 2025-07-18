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
        int dungeonFloor = 0;
        private Item rewardItem;
        private bool rewardGiven = false;

        void ResultCalculator()
        {
            dungeonFloor = Player.Instance.DungeonFloor++; // 출력할때 변경하기
            pastLevel = Player.Instance.Level;
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

        private void GiveReward()
        {
            if (!rewardGiven)
            {
                var origin = ItemLibrary.Instance.GetRandomRewardItem();
                rewardItem = new Item(origin); // 복사 생성자 필요
                Player.Instance.AddItem(rewardItem);
                rewardGiven = true;
            }
        }



        public override void Render()
        {
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!! - Result");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.AppendLine("Victory");
            sb.AppendLine();
            sb.AppendLine($"{dungeonFloor}층 던전에서 몬스터 {totalMonsterNumber}마리를 잡았습니다.");
            if (rewardGiven && rewardItem != null)
            {
                sb.AppendLine($"보상 아이템: {rewardItem.Name}");
            }
            sb.AppendLine();
            sb.AppendLine("[캐릭터 정보]");
            if (isLevelUp) sb.AppendLine($"Lv.{pastLevel} {player.Name} -> Lv.{player.Level} {player.Name}");
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
            sb.Clear();

            base.SetupScene();

            //경험치 계산
            ResultCalculator();
            isLevelUp = Player.Instance.LevelCalculator(totalExpGained);

            // 보상 아이템 지급
            rewardGiven = false;
            rewardItem = null;
            GiveReward();

            // 소환된 몬스터 초기화
            MonsterManager.Instance.ClearActiveMonsters();
        }
        public override void FinishScene()
        {
            Player.Instance.BattleFinish();
        }

    }
}
