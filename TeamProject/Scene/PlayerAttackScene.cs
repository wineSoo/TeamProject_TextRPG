using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class PlayerAttackScene : Scene
    {
        public PlayerAttackScene()
        {
            sb = new StringBuilder();
            sbMonHp = new StringBuilder();
            options.Add("0. 다음");
            optionsLen = options.Count;
            SetupScene();// 현재 몬스터 체력 세팅
        }
        // 플레이어가 공격 맞춤 -> 적 체력 줄어듦 -> 데미지 표시;
        // 추후 필수 기능으로 간다면
        // 1. 플레이어 공격 -> 플레이어 공격이 맞았다면 2, 안맞았다면 5
        // 2. 몬스터 현재 체력 잠깐 보여주기 -> 3
        // 3. 몬스터 체력 닳기 -> 4
        // 4. 몬스터 체력 변화량 출력
        // 5. 몬스터 회피, 바로 적 턴으로
        
        enum AttackState 
        {
            PlayerAttack, ShowEnermyHp, EnemyTakeDamage, ShowDamageText, Evaded
        }
        AttackState atkState { get; set; }

        StringBuilder sb; // 기본 출력용
        float lerpTime = 0;

        // 테스트용 몬스터
        Monster? selectedMon;
        int beforeHp = 0;
        int lerpBeforeHpToCurHp = -1;
        int renderDam = 0;
        // 치명타 출력용
        bool isCritical = false;
        string criticalS = " - 치명타 공격!!";
        // 몬스터 체력 출력용 
        StringBuilder sbMonHp;
        // 최대 Hp바 개수
        int hpBarCnt = 50;
        public override void Render()
        {
            switch (atkState)
            {
                case AttackState.PlayerAttack:
                    RenderPlayerAttack();
                    break;
                case AttackState.ShowEnermyHp:
                    RenderEnermyHp();
                    break;
                case AttackState.EnemyTakeDamage:
                    RenderEnermyTakeDam();
                    break;
                case AttackState.ShowDamageText:
                    RenderDamageText();
                    break;
                case AttackState.Evaded:
                    Evaded();
                    break;
                default:
                    break;
            }
        }

        protected override void SceneControl()
        {
            ControlManager.ClearInputBuffer();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.Z: // 다음 선택
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.EnemyAttackScene;
                    // atkState = AttackState.PlayerAttack; 씬 세팅에서
                    //Console.WriteLine("적 공격 턴으로 넘어갑니다");
                    //Thread.Sleep(1000);
                    break;
                case ConsoleKey.X: // 선택지 없음
                    
                    break;
                default:
                    break;
            }
        }
        void RenderPlayerAttack()
        {
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append(Player.Instance.Name);
            sb.AppendLine("의 공격!");

            Console.Write(sb.ToString());

            // 기본 공격이라면 회피 확률 10%, 스킬은 회피 불가
            // 현재는 기본 공격만
            if(CheckAttackSuccess()) // 공격이 적중했는가
            {
                atkState = AttackState.ShowEnermyHp;
            }
            else // 회피
            {
                atkState = AttackState.Evaded;
            }

            Thread.Sleep(500);
        }
        void RenderEnermyHp()
        {
            if (selectedMon == null) return;
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append(Player.Instance.Name);
            sb.AppendLine("의 공격!");

            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.Append(selectedMon.Name.ToString());
            
            sb.Append($"을(를) 맞췄습니다. [데미지: {renderDam}]");
            if (isCritical) sb.AppendLine(criticalS);
            else sb.AppendLine();
            sb.AppendLine();

            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            //sb.AppendLine(tM.Name.ToString());
            sb.AppendLine(selectedMon.Name.ToString());
            sb.Append("[");
            Console.Write(sb.ToString());

            //SetMonHp(tM); // 몬스터 체력바 세팅
            Console.ForegroundColor = ConsoleColor.Red; // 출력 색 지정
            //sb.AppendLine(" ██████████████████████████████████████████████████"); // 50개
            //sb.AppendLine(" ██████████████████████████████"); // 30개
            Console.Write(sbMonHp.ToString()); // 체력바 출력
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append("]");
            Console.Write(sb.ToString());
            Thread.Sleep(500);
            atkState = AttackState.EnemyTakeDamage;
        }
        void RenderEnermyTakeDam()
        {
            if (selectedMon == null) return;

            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append(Player.Instance.Name);
            sb.AppendLine("의 공격!");

            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.Append(selectedMon.Name.ToString());
            sb.Append($"을(를) 맞췄습니다. [데미지: {renderDam}]");
            if (isCritical) sb.AppendLine(criticalS);
            else sb.AppendLine();
            sb.AppendLine();

            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.AppendLine(selectedMon.Name.ToString());
            sb.Append("[");
            Console.Write(sb.ToString());

            SetMonHp(selectedMon); // 몬스터 체력바 세팅
            Console.ForegroundColor = ConsoleColor.Red; // 출력 색 지정
            Console.Write(sbMonHp.ToString()); // 체력바 출력
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append("]");
            sb.AppendLine();

            Console.Write(sb.ToString());
            if (lerpTime >= 1.0f)
            {
                lerpTime = 0.0f;
                atkState = AttackState.ShowDamageText;
                Thread.Sleep(300);
            }
            else
            {
                Thread.Sleep(50);
            }
        }
        void RenderDamageText()
        {
            if (selectedMon == null) return;

            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append(Player.Instance.Name);
            sb.AppendLine("의 공격!");

            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.Append(selectedMon.Name.ToString());
            sb.Append($"을(를) 맞췄습니다. [데미지: {renderDam}]");
            if (isCritical) sb.AppendLine(criticalS);
            else sb.AppendLine();
            sb.AppendLine();


            sb.Append("Lv. ");
            sb.Append(selectedMon.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.AppendLine(selectedMon.Name.ToString());
            sb.Append("[");
            Console.Write(sb.ToString());

            
            Console.ForegroundColor = ConsoleColor.Red; // 출력 색 지정
            //sb.AppendLine(" ██████████████████████████████████████████████████"); // 50개
            //sb.AppendLine(" ██████████████████████████████"); // 30개
            Console.Write(sbMonHp.ToString()); // 체력바 출력
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append("]");
            sb.AppendLine();

            sb.Append("HP ");
            sb.Append(beforeHp);
            sb.Append(" -> ");
            sb.AppendLine(selectedMon.Hp <= 0 ? "Dead" : selectedMon.Hp.ToString());

            sb.AppendLine();
            sb.Append("▶ ");
            sb.AppendLine(options[0]);

            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z, 돌아가기: x");
            Console.Write(sb.ToString());
            if (CheckClear()) // 클리어 여부 확인
            {
                atkState = AttackState.PlayerAttack;
                Console.WriteLine("모든 적을 처지하셨습니다!");

                //소환된 몬스터 초기화가 원래 이 자리에 있었는데, WindEndScene으로 이동했습니다

                Thread.Sleep(2000);
                // 클리어 했다면 자동으로 승리 씬으로 이동
                // 테스트로는 스타트로 이동
                SceneManager.Instance.SetSceneState = SceneManager.SceneState.WinEndScene;
            }
            else SceneControl();
        }
        void Evaded()
        {
            if (selectedMon == null) return;

            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            sb.AppendLine();
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.AppendLine($"{Player.Instance.Name}의 공격!");
            sb.AppendLine($"Lv. {selectedMon.Level} {selectedMon.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            sb.AppendLine();
            sb.AppendLine($"▶ {options[0]}");
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.Write(sb.ToString());
            SceneControl();
        }
        
        public override void SetupScene()
        {
            base.SetupScene();
            selectedMon = MonsterManager.Instance.GetSelectedMonster();
            if (selectedMon == null) return;

            beforeHp = selectedMon.Hp;
            float tmp = selectedMon.Hp / (float)selectedMon.MaxHp;
            int tmpCnt = (int)(hpBarCnt * tmp);
            sbMonHp.Clear();
            for (int i = 0; i < hpBarCnt; i++)
            {
                if (i < tmpCnt) sbMonHp.Append("█");
                else sbMonHp.Append(" ");
            }
            lerpBeforeHpToCurHp = beforeHp;
            atkState = AttackState.PlayerAttack;
        }
        void SetMonHp(Monster mon)
        {
            lerpTime += 0.1f;

            lerpBeforeHpToCurHp = (int)ControlManager.Lerp(beforeHp, mon.Hp, lerpTime);
            float tmp = (float)lerpBeforeHpToCurHp / mon.MaxHp;
            int tmpCnt = (int)(hpBarCnt * tmp);
            sbMonHp.Clear();
            for (int i = 0; i < hpBarCnt; i++)
            {
                if (i < tmpCnt) sbMonHp.Append("█");
                else sbMonHp.Append(" ");
            }
        }
        bool CheckClear() // 몬스터 다 죽었는지 체크하기
        {
            List<Monster>? monList = MonsterManager.Instance.GetActiveMonsters();
            if (monList == null) return true;
            foreach (Monster mon in monList)
            {
                if (!mon.isDie) return false; // 안죽었다면 다시
            }
            return true;
        }
        bool CheckAttackSuccess()
        {
            if (selectedMon == null) return false;
            bool IsHit;
            beforeHp = selectedMon.Hp;
            renderDam = selectedMon.DamageTaken((int)Player.Instance.AtkPower, out IsHit, out isCritical);
            //renderDam = selectedMon.DamageTaken(new Skill(), out IsHit, out isCritical);

            return IsHit;
        }
    }
}
