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
        private List<string> actions = new List<string> { "1. 공격", "2. 스킬" };
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
                    if (m.isDie)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: Dead)");// 죽은 몬스터는 회색 처리
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
                Console.WriteLine($"MP {player.Mp}/{player.MaxMp}");
                Console.WriteLine();

                if (currentState == BattleState.SelectAction) // 공격 스킬 선택
                {
                    //string[] actions = { "1. 공격", "2. 스킬" };
                    for (int i = 0; i < actions.Count; i++)
                    {
                        if (actionSelect == i) Console.Write("▶ ");
                        else Console.Write("　 ");
                        Console.WriteLine(actions[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("공격 방식을 선택해주세요. 이동: 방향키, 선택: z");
                }
                else if (currentState == BattleState.SelectSkill) // 스킬 선택
                {
                    List<Skill> skills = Player.Instance.skills;
                    for (int i = 1; i < skills.Count; i++)
                    {
                        if (skillSelect == i) Console.Write("▶ ");
                        else Console.Write("　 ");
                        //Console.WriteLine($"{i}. {skills[i].Name} | 데미지: {skills[i].Atk} | PP: {skills[i].PP} | 설명: {skills[i].Description}");
                        Console.WriteLine($"{i}. {skills[i].Name} - MP {skills[i].MP}\n　    {skills[i].Description}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("사용할 스킬을 선택해주세요. 이동: 방향키, 선택: z, 취소: x");
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
                        Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: Dead)");// 죽은 몬스터는 회색 처리
                        Console.ResetColor();
                    }
                    else Console.WriteLine($"Lv.{m.Level} {m.Name} (HP: {m.Hp})");

                }
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
                Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
                Console.WriteLine($"MP {player.Mp}/{player.MaxMp}");
                Console.WriteLine();
                Console.WriteLine("공격할 몬스터를 선택해주세요. 이동: 방향키, 선택: z, 취소: x");
            }
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
                                Player.Instance.SelSkillNum = actionSelect;

                                if (enemy == null) return;
                                for (int i = 0; i < enemy.Count; i++) // 죽은 몬스터에 화살표 안 가도록
                                {
                                    if (enemy[i].isDie) continue;
                                    monsterselect = i;
                                    break;
                                }
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
                            if(player.CanUseSkill(skillSelect)) // 사용할 마나가 되는가
                            {
                                Player.Instance.SelSkillNum = skillSelect;

                                if (enemy == null) return;
                                for (int i = 0; i < enemy.Count; i++) // 죽은 몬스터에 화살표 안 가도록
                                {
                                    if (enemy[i].isDie) continue;
                                    monsterselect = i;
                                    break;
                                }

                                // 스킬이 싱글 타겟인가 멀티 타겟인가 랜덤 멀티 타겟인가
                                switch (Player.Instance.GetUseSkill().Target)
                                {
                                    case Skill.SkillTarget.Multi:
                                    case Skill.SkillTarget.Single: // 기본 로직대로 몬스터 한 마리 선택으로
                                        currentState = BattleState.SelectMonster; // 몬스터 선택으로
                                        break;
                                    case Skill.SkillTarget.RandomMulti:
                                        MonsterManager.Instance.SelActiveMonstersNum = monsterselect;
                                        Player.Instance.UseSkill(); // 마나 사용

                                        // 바로 플레이어 공격 씬으로
                                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.PlayerAttackScene;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                // 안되면 사용 불가 출가
                                Console.WriteLine("MP가 부족합니다.");
                                Thread.Sleep(1000);
                            }
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
                                if (monsterselect == 0 || enemy == null) return;
                                for (int i = monsterselect - 1; i >= 0; i--) // 현재 위치부터 위로 탐색
                                {
                                    if (enemy[i].isDie) continue; // 해당 인덱스의 몬스터가 죽었다면 스킵
                                                                  // 살았다면 인덱스 넣어주고 브레이크
                                    monsterselect = i;
                                    break;
                                }

                                // 이전 로직
                                /*do
                                {
                                    if (monsterselect != 0) monsterselect--;
                                    else break;
                                } while (enemy != null && enemy[monsterselect].isDie && monsterselect != prev); // 살아있는 몬스터 찾을 때까지*/
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (enemy != null && monsterselect != enemy.Count - 1)
                                {
                                    for (int i = monsterselect + 1; i < enemy.Count; i++) // 현재 위치부터 위로 탐색
                                    {
                                        if (enemy[i].isDie) continue; // 해당 인덱스의 몬스터가 죽었다면 스킵
                                                                      // 살았다면 인덱스 넣어주고 브레이크
                                        monsterselect = i;
                                        break;
                                    }
                                }

                                /* do
                                 {
                                     if (enemy != null && monsterselect != enemy.Count - 1) monsterselect++;
                                     else break;
                                 } while (enemy[monsterselect].isDie && monsterselect != prev); // 살아있는 몬스터 찾을 때까지*/
                                break;
                            }
                        case ConsoleKey.Z: // 몬스터 공격으로 
                            MonsterManager.Instance.SelActiveMonstersNum = monsterselect;
                            Player.Instance.UseSkill(); // 마나 사용

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
                actionSelect = 0;
            }
            currentState = BattleState.SelectAction;
        }
    }
}
