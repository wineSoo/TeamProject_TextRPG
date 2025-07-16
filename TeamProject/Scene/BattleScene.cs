using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class BattleScene : Scene
    {
        
        public static BattleScene? Instance { get; private set; }

        private StringBuilder sb;
        private int selOptions = 0;     // 몬스터 선택용
        private int actionSelect = 0;   // 공격 스킬 선택용

        private MonsterLibrary monsterLibrary;
        private List<Monster>? enemy;

        private Player player;

        
        private BattleState currentState;

        
        public BattleState CurrentState
        {
            get => currentState;
            set => currentState = value;
        }

        
        public enum BattleState
        {
            SelectAction,   // 공격 스킬 선택
            SelectMonster   // 적 선택
        }

        
        public BattleScene()
        {
            if (Instance == null)
                Instance = this;

            this.player = Player.Instance;
            sb = new StringBuilder();
            monsterLibrary = new MonsterLibrary();
            SetupScene();
        }

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            if (enemy == null) return;

            if (currentState == BattleState.SelectAction)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    Monster m = enemy[i];

                    sb.Append("　 ");
                    sb.AppendLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                sb.AppendLine();
                sb.AppendLine("[내정보]");
                sb.AppendLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
                sb.AppendLine($"HP {player.Hp}/100");
                sb.AppendLine();

                string[] actions = { "공격", "스킬" };
                for (int i = 0; i < actions.Length; i++)
                {
                    if (actionSelect == i) sb.Append("▶ ");
                    else sb.Append("　 ");
                    sb.AppendLine(actions[i]);
                }
                sb.AppendLine();

                sb.AppendLine("이동: 방향키, 선택: z");
            }
            else if (currentState == BattleState.SelectMonster)
            {
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
                sb.AppendLine("이동: 방향키, 선택: z, 취소: x");
            }
            sb.AppendLine();
            sb.AppendLine("[내정보]");
            sb.AppendLine($"Lv.{player.Lv} {player.Name} ({player.Job})");
            sb.AppendLine($"HP {player.Hp}/{player.MaxHp}");
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.Write(sb.ToString());
            SceneControl();

        }
        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (currentState)
            {
                case BattleState.SelectAction:
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (actionSelect != 0) actionSelect--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (actionSelect != 1) actionSelect++;
                            break;
                        case ConsoleKey.Z:
                            if (actionSelect == 0) // 공격 선택
                            {
                                currentState = BattleState.SelectMonster;
                            }
                            else if (actionSelect == 1) // 스킬 선택
                            {
                                SceneManager.Instance.SetSceneState = SceneManager.SceneState.SkillScene;
                            }
                            break;
                    }
                    break;

                case BattleState.SelectMonster:
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (selOptions != 0) selOptions--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (enemy != null && selOptions != enemy.Count - 1) selOptions++;
                            break;
                        case ConsoleKey.Z:
                            MonsterManager.Instance.SelActiveMonstersNum = selOptions;
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                            break;
                            case ConsoleKey.X:
                            currentState = BattleState.SelectAction;
                            break;
                        default:
                            break;
            }
            break;
        }
    }

        public override void SetupScene()
        {
            base.SetupScene();
            if (enemy == null || enemy.Count == 0)
            {
                MonsterManager.Instance.SetBattleMonsters();
                enemy = MonsterManager.Instance.GetActiveMonsters();
                MonsterManager.Instance.SelActiveMonstersNum = -1;
                selOptions = 0; // 몬스터 선택지도 초기화
                Player.Instance.BattleStartHp = Player.Instance.Hp;
            }
        }
    }
}
