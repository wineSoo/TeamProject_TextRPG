using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.CharacterManager;

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
            sb.AppendLine($"공격력 : {Player.Instance.AtkPower}");// 플레이어 공격력
            sb.AppendLine($"방어력 : {Player.Instance.DefPower}");// 플레이어 방어력
            sb.AppendLine($"기 술 : {Player.Instance.Skill}"); //플레이어 기술 = 플레이어 치명타율
            sb.AppendLine($"속 도 : {Player.Instance.Speed}");// 플레이어 속도 = 플레이어 회피율
            sb.AppendLine($"체 력 : {Player.Instance.Hp}");// 플레이어 체력
            sb.AppendLine($"마 나 : {Player.Instance.Mp}");//플레이어 마나
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
