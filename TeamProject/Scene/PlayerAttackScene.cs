using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class PlayerAttackScene : Scene
    {
        public PlayerAttackScene()
        {
            sb = new StringBuilder();
            options.Add("0. 다음");
            optionsLen = options.Count;
            tM = new Monster("대포미니언", 99, 124, 10, 5, "");
        }
        StringBuilder sb;
        // 테스트용 몬스터
        Monster tM;
        int beforeHp = 124;
        public override void Render()
        {
            sb.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; // 출력 색 지정
            sb.AppendLine("Battle!!");
            Console.Write(sb.ToString());
            Console.ResetColor();// 출력 색 초기화

            sb.Clear();
            sb.Append(Player.Instance.Name);
            sb.AppendLine("의 공격!"); 

            sb.Append("Lv. ");
            sb.Append(tM.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.Append(tM.Name.ToString());
            sb.Append("을(를) 맞췄습니다. [데미지: "); 
            sb.Append("11"); // 가한 데미지
            sb.AppendLine("]");

            sb.Append("Lv. ");
            sb.Append(tM.Level.ToString()); // 몬스터 레벨
            sb.Append(" ");
            sb.AppendLine(tM.Name.ToString());

            sb.Append("HP ");
            sb.Append(beforeHp);
            sb.Append(" -> ");
            sb.AppendLine(tM.Hp <= 0 ? "Dead" : tM.Hp.ToString());

            sb.AppendLine();
            sb.Append("▶ ");
            sb.AppendLine(options[0]);
            
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
                case ConsoleKey.Z: // 나가기 선택
                case ConsoleKey.X: // 나가기
                    //SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    Console.WriteLine("적 공격으로 전환");
                    Thread.Sleep(1000);
                    break;
                default:
                    break;
            }
        }
    }
}
