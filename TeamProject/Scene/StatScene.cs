using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class StatScene : Scene
    {
        public StatScene()
        {
            sb = new StringBuilder();
            options.Add("0. 나가기");
            optionsLen = options.Count;
        }
        StringBuilder sb;
        int selOptions = 0;
        public override void Render()
        {
            Player.Instance.SetAbilityByEquipment();
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("상태 보기");
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화
            sb.Clear();

            sb.AppendLine("캐릭터의 정보가 표시됩니다.");
            sb.AppendLine();
            sb.AppendLine($"Lv. {Player.Instance.Level}"); // 플레이어 레벨
            sb.Append(Player.Instance.Name); // 플레이어 이름
            switch (Player.Instance.Job) // 플레이어 직업
            {
                case Player.PlayerJob.Warrior:
                    sb.AppendLine($" ( 전사 )");
                    break;
                case Player.PlayerJob.Archer:
                    sb.AppendLine($" ( 궁수 )");
                    break;
                case Player.PlayerJob.Theif:
                    sb.AppendLine($" ( 도적 )");
                    break;
                case Player.PlayerJob.Mage:
                    sb.AppendLine($" ( 마법사 )");
                    break;
                default:
                    break;
            }

            string FormatStat(string statName, float baseValue, int plusValue)
            {
                string plusText = plusValue > 0 ? $" (+{plusValue})" : "";
                return $"{statName}: {baseValue}{plusText}";
            }

            sb.AppendLine(FormatStat("공격력", Player.Instance.AtkPower, Player.Instance.PlusAtk)); // 플레이어 공격력
            sb.AppendLine(FormatStat("방어력", Player.Instance.DefPower, Player.Instance.PlusDef)); // 플레이어 방어력
            sb.AppendLine(FormatStat("기 술 ", Player.Instance.Skill, Player.Instance.PlusSkill)); // 플레이어 기술 (치명타율)
            sb.AppendLine(FormatStat("속 도 ", Player.Instance.Speed, Player.Instance.PlusSpeed)); // 플레이어 속도 (회피율)
            sb.AppendLine(FormatStat("체 력 ", Player.Instance.MaxHp, Player.Instance.PlusHp)); // 플레이어 체력
            sb.AppendLine(FormatStat("마 나 ", Player.Instance.MaxMp, Player.Instance.PlusMp)); // 플레이어 마나
            sb.AppendLine($"Gold : {Player.Instance.Gold} G"); // 플레이어 골드
            sb.AppendLine();
            for (int i = 0; i < optionsLen; i++)
            {
                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z, 돌아가기: x");
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
                case ConsoleKey.Z: // 나가기 선택
                case ConsoleKey.X: // 나가기
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                default:
                    break;
            }
        }
    }
    
}
