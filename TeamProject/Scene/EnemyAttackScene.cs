using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TeamProject.BattleScene;

namespace TeamProject
{
    internal class EnemyAttackScene : Scene
    {
        
        public EnemyAttackScene()
        {
            sb = new StringBuilder();
            
            options.Add("0. 다음");
            optionsLen = options.Count;

            /* monster1 = new Monster("대포미니언", 1, 124, 10, 5, "");
             monster2 = new Monster("대포미니언", 2, 124, 10, 5, "");
             monster3 = new Monster("대포미니언", 3, 124, 10, 5, "");
             monsters.Add(monster1);
             monsters.Add(monster2);
             monsters.Add(monster3);*/

            sbPlayerHp = new StringBuilder();
            SetPlayerHp();
            sb0 = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 120; j++)
                {
                    sb0.Append(" ");
                }
                sb0.AppendLine();
            }
            tmpS = sb0.ToString();
        }
        // 1. 적공격 -> 공격 성공시 2, 실패시 4
        // 2. 플레이어 체력 줄어들기 -> 3
        // 3. 데미지
        enum AttackState
        {
            EnermyAttack, ShowPlayerHp, PlayerTakeDamage, DamageText, Evaded, Finish
        }
        AttackState AtkState { get; set; }

        //float initialHp = Player.Instance.Hp;
        StringBuilder sb;
        int selOptions = 0;
        StringBuilder sb0; // 덮어쓰기 용
        string tmpS;
        // 출력용 임시 저장 변수
        //float initialHp;
        int beforeHp = 0;
        int renderDam = 0;
        // 치명타 출력용
        bool isCritical = false;
        string criticalS = " - 치명타 공격!!";
        // 패배 씬으로 넘어가기용 변수
        bool isGameOver = false;
        // 플레이어 체력 출력용 
        StringBuilder sbPlayerHp;
        // 최대 Hp바 개수
        int hpBarCnt = 50;
        int lerpBeforeHpToCurHp = -1;
        // 체력 보간용
        float lerpTime = 0;

        //받아와야 하는거
        //현재 살아있는 몬스터 목록
        /*Monster monster1;
        Monster monster2;
        Monster monster3;*/
        //List<Monster> monsters = new List<Monster>();
        List<Monster>? monsters;


        public override void Render()
        {
            if (monsters == null) return;

            foreach (Monster monster in monsters)
            {
                if (monster.isDie) continue; // 죽었다면 다음 미니언

                while (AtkState != AttackState.Finish)
                {
                    switch (AtkState)
                    {
                        case AttackState.EnermyAttack:
                            EnermyAttack(monster);
                            break;
                        case AttackState.ShowPlayerHp:
                            ShowPlayerHp(monster);
                            break;
                        case AttackState.PlayerTakeDamage:
                            PlayerTakeDamage(monster);
                            break;
                        case AttackState.DamageText:
                            DamageText(monster);
                            break;
                        case AttackState.Evaded:
                            Evaded(monster);
                            break;
                        default:
                            break;
                    }
                }
                AtkState = AttackState.EnermyAttack;
                // 게임이 종료되면 몬스터 정보도 사라지기 때문에 반복문 종료하기
                if (isGameOver) break;



                /*sb.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
                sb.AppendLine("Battle!!");
                sb.AppendLine();
                Console.Write(sb.ToString());
                Console.ResetColor();// 출력 색 초기화

                sb.Clear();
                sb.AppendLine($"LV.{monster.Level} {monster.Name} 의 공격!");

                initialHp = (int)Player.Instance.Hp;
                renderDam = Player.Instance.DamageTaken(monster.Atk);
                sb.Append($"{Player.Instance.Name} 을(를) 맞췄습니다.  ");

                sb.AppendLine($"[데미지: {renderDam}]");
                sb.AppendLine();

                sb.AppendLine($"Lv.{Player.Instance.Lv} {Player.Instance.Name}");
                sb.Append("[");
                Console.Write(sb.ToString());

                // 체력바 출력
                Console.ForegroundColor = ConsoleColor.Red; // 출력 색 지정
                SetPlayerHp();
                Console.Write(sbPlayerHp.ToString()); // 체력바 출력
                Console.ResetColor();// 출력 색 초기화

                sb.Clear();
                sb.AppendLine("]");
                sb.AppendLine($"Hp {(int)initialHp} -> {(int)Player.Instance.Hp}");
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
                SceneControl();*/

            }

            if (!isGameOver)
            {
                BattleScene.Instance.CurrentState = BattleState.SelectAction;
                SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
            }
            else isGameOver = false; // 다음 배틀 상황이 올때 바로 게임 오버가 되지 않도록
        }
        void EnermyAttack(Monster mon)
        {
            DrawEnermyAttack(ref mon);
            Console.WriteLine(sb.ToString());
            if (CheckAttackSuccess(ref mon)) // 공격이 적중했는가
            {
                AtkState = AttackState.ShowPlayerHp;
            }
            else // 회피
            {
                AtkState = AttackState.Evaded;
            }
            Thread.Sleep(500);
            ClearConsole();
        }
        void ShowPlayerHp(Monster mon)
        {
            DrawEnermyAttack(ref mon);
            DrawHit(ref mon);
            Console.WriteLine(sb.ToString());
            Thread.Sleep(500);
            AtkState = AttackState.PlayerTakeDamage;
            ClearConsole();
        }
        void PlayerTakeDamage(Monster mon)
        {
            SetPlayerHp();
            DrawEnermyAttack(ref mon);
            DrawHit(ref mon);
            Console.WriteLine(sb.ToString());
            if (lerpTime >= 1.0f)
            {
                lerpTime = 0.0f;
                AtkState = AttackState.DamageText;
                Thread.Sleep(300);
            }
            else
            {
                Thread.Sleep(50);
            }
            ClearConsole();
        }
        void DamageText(Monster mon)
        {
            DrawEnermyAttack(ref mon);
            DrawHit(ref mon);
            DrawDamage();
            SceneControl();
            ClearConsole();
            AtkState = AttackState.Finish;
        }
        void Evaded(Monster mon)
        {
            DrawEnermyAttack(ref mon);
            sb.AppendLine($"Lv. {Player.Instance.Lv} {Player.Instance.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            sb.AppendLine();
            sb.AppendLine($"▶ {options[0]}");
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.WriteLine(sb.ToString());
            SceneControl();
            ClearConsole();
            AtkState = AttackState.Finish;
        }
        void ClearConsole()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(tmpS);
            Console.SetCursorPosition(0, 0);
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
                    // 매 턴마다 플레이어 체력 체크, 체력 0이면 게임 오버 씬으로
                    if (Player.Instance.Hp <= 0) //게임 오버 씬
                    {
                        Console.WriteLine("게임 오버 씬으로 체인지");
                        isGameOver = true;
                        Thread.Sleep(2000);

                        // 소환된 몬스터 초기화
                        MonsterManager.Instance.ClearActiveMonsters();
                        // 테스트로 스타트씬으로 변경
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.LoseEndScene;

                        //SceneManager.Instance.SetSceneState = SceneManager.SceneState.
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
            monsters = MonsterManager.Instance.GetActiveMonsters();
            AtkState = AttackState.EnermyAttack;

            beforeHp = (int)Player.Instance.Hp;
            float tmp = Player.Instance.Hp / (float)Player.Instance.MaxHp;
            int tmpCnt = (int)(hpBarCnt * tmp);
            sbPlayerHp.Clear();
            for (int i = 0; i < hpBarCnt; i++)
            {
                if (i < tmpCnt) sbPlayerHp.Append("█");
                else sbPlayerHp.Append(" ");
            }
            lerpBeforeHpToCurHp = beforeHp;
        }

        void SetPlayerHp()
        {
            lerpTime += 0.1f;

            lerpBeforeHpToCurHp = (int)ControlManager.Lerp(beforeHp, Player.Instance.Hp, lerpTime);
            float tmp = (float)lerpBeforeHpToCurHp / Player.Instance.MaxHp;
            int tmpCnt = (int)(hpBarCnt * tmp);
            sbPlayerHp.Clear();
            for (int i = 0; i < hpBarCnt; i++)
            {
                if (i < tmpCnt) sbPlayerHp.Append("█");
                else sbPlayerHp.Append(" ");
            }
        }
        bool CheckAttackSuccess(ref Monster mon)
        {
            bool IsHit;
            beforeHp = (int)Player.Instance.Hp;
            renderDam = Player.Instance.DamageTaken((int)mon.Atk, out IsHit, out isCritical);

            return IsHit;
        }
        void DrawEnermyAttack(ref Monster mon)
        {
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            Console.WriteLine(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.AppendLine($"LV.{mon.Level} {mon.Name} 의 공격!");
        }
        void DrawHit(ref Monster mon)
        {
            sb.Append($"{Player.Instance.Name} 을(를) 맞췄습니다. ");

            sb.Append($"[데미지: {renderDam}]");
            if (isCritical) sb.AppendLine(criticalS);
            else sb.AppendLine();
            sb.AppendLine();

            sb.AppendLine($"Lv.{Player.Instance.Lv} {Player.Instance.Name}");
            sb.Append("[");
            Console.Write(sb.ToString());

            // 체력바 출력
            Console.ForegroundColor = ConsoleColor.Red; // 출력 색 지정
            Console.Write(sbPlayerHp.ToString()); // 체력바 출력
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.AppendLine("]");
        }
        void DrawDamage()
        {
            sb.AppendLine($"Hp {(int)beforeHp} -> {(int)Player.Instance.Hp}");
            sb.AppendLine();

            for (int i = 0; i < optionsLen; i++)
            {
                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.WriteLine(sb.ToString());
        }
    }
}
