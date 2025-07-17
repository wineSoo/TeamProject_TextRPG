using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.CharacterManager;

namespace TeamProject
{
    internal class BattleScene : Scene
    {
        
        public static BattleScene? Instance { get; private set; }


        private StringBuilder sb;
        private int selOptions = 0;     // 몬스터 선택용
        private int actionSelect = 0;   // 공격 스킬 선택용
        private int skillSelect = 1;     // 스킬 선택용 (0: 공격, 1: 스킬) 

        private MonsterLibrary monsterLibrary;
        private List<Monster>? enemy;

        private Player player;
        private BattleState currentState = BattleState.SelectAction;

        public enum BattleState
        {
            SelectAction,   // 공격 스킬 선택
            SelectSkill,    // 스킬 선택
            SelectMonster   // 적 선택
        }

        
        public BattleScene()
        {

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

            if (currentState == BattleState.SelectAction || currentState == BattleState.SelectSkill)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    Monster m = enemy[i];

                    sb.Append("　 ");
                    sb.AppendLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                sb.AppendLine();
                sb.AppendLine("[내정보]");

                sb.AppendLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                sb.AppendLine($"HP {player.Hp}/{player.MaxHp}");
                sb.AppendLine();

                if (currentState == BattleState.SelectAction) // 공격 스킬 선택
                { string[] actions = { "공격", "스킬" };
                    for (int i = 0; i < actions.Length; i++)
                    {
                        if (actionSelect == i) sb.Append("▶ ");
                        else sb.Append("　 ");
                        sb.AppendLine(actions[i]);
                    }
                }
                else if (currentState == BattleState.SelectSkill) // 스킬 선택
                {
                    sb.AppendLine();
                    List<Skill> skills = SkillLibrary.Instance.GetAllSkills();
                    for (int i = 1; i < skills.Count; i++)
                    {
                        if (skillSelect == i) sb.Append("▶ ");
                        else sb.Append("　 ");
                        sb.AppendLine($"{skills[i].Name} | 데미지: {skills[i].Atk} | PP: {skills[i].PP} | 설명: {skills[i].Description}");
                    }
                }
            }
            else if (currentState == BattleState.SelectMonster) // 몬스터 선택
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

                sb.AppendLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                sb.AppendLine($"HP {player.Hp}/{player.MaxHp}");
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z, 취소: x");
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
                                currentState = BattleState.SelectSkill; 
                            }
                            break;
                    }
                    break;

                case BattleState.SelectSkill:
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (skillSelect != 1) skillSelect--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (skillSelect != 2) skillSelect++;
                            break;
                        case ConsoleKey.Z:
                            currentState = BattleState.SelectMonster; // 몬스터 선택으로
                            break;
                        case ConsoleKey.X: // 취소
                            currentState = BattleState.SelectAction;
                            break;
                        default:
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
                        case ConsoleKey.Z: // 몬스터 공격으로 
                            MonsterManager.Instance.SelActiveMonstersNum = selOptions;
                            SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                            break;
                            case ConsoleKey.X: // 취소
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
