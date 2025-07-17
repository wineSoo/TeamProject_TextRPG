using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class BattleScene : Scene
    {


        private int monsterselect = 0;     // 몬스터 선택용
        private int actionSelect = 0;   // 공격 스킬 선택용
        private int skillSelect = 1;     // 스킬 선택용 (0: 공격, 1: 스킬) 

        private MonsterLibrary monsterLibrary;

        List<Monster>? enemy = MonsterManager.Instance.GetActiveMonsters();

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
            player = Player.Instance;
            monsterLibrary = new MonsterLibrary();
            SetupScene();
        }

        public override void Render()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            if (enemy == null) return;

            if (currentState == BattleState.SelectAction || currentState == BattleState.SelectSkill)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    Monster m = enemy[i];

                    Console.Write("　 ");
                    Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
                Console.WriteLine();

                if (currentState == BattleState.SelectAction) // 공격 스킬 선택
                {
                    string[] actions = { "공격", "스킬" };
                    for (int i = 0; i < actions.Length; i++)
                    {
                        if (actionSelect == i) Console.Write("▶ ");
                        else Console.Write("　 ");
                        Console.WriteLine(actions[i]);
                    }
                }
                else if (currentState == BattleState.SelectSkill) // 스킬 선택
                {
                    Console.WriteLine();
                    List<Skill> skills = SkillLibrary.Instance.GetAllSkills();
                    for (int i = 1; i < skills.Count; i++)
                    {
                        if (skillSelect == i) Console.Write("▶ ");
                        else Console.Write("　 ");
                        Console.WriteLine($"{skills[i].Name} | 데미지: {skills[i].Atk} | PP: {skills[i].PP} | 설명: {skills[i].Description}");
                    }
                }
            }
            else if (currentState == BattleState.SelectMonster) // 몬스터 선택
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    Monster m = enemy[i];

                    if (monsterselect == i) Console.Write("▶ ");
                    else Console.Write("　 ");

                    if (m.isDie)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"　Lv.{m.Level} {m.Name} (HP: Dead)");// 죽은 몬스터는 회색 처리
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("이동: 방향키, 선택: z, 취소: x");
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
                            {
                                int prev = monsterselect;
                                do
                                {
                                    if (monsterselect != 0) monsterselect--;
                                    else break;
                                } while (enemy != null && enemy[monsterselect].isDie && monsterselect != prev); // 살아있는 몬스터 찾을 때까지
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                int prev = monsterselect;
                                do
                                {
                                    if (enemy != null && monsterselect != enemy.Count - 1) monsterselect++;
                                    else break;
                                } while (enemy[monsterselect].isDie && monsterselect != prev); // 살아있는 몬스터 찾을 때까지
                                break;
                            }
                        case ConsoleKey.Z: // 몬스터 공격으로 
                            MonsterManager.Instance.SelActiveMonstersNum = monsterselect;
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
                monsterselect = 0; // 몬스터 선택지도 초기화
                Player.Instance.BattleStartHp = Player.Instance.Hp;
            }
        }
    }
}
