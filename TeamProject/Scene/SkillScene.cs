using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject.BattleScene;
using static TeamProject.SceneManager;

namespace TeamProject
{
    internal class SkillScene : Scene
    {
        private StringBuilder sb;
        private List<Skill> skills = new List<Skill>();
        private int selOptions = 1; // 스킬 선택용
        private int skillLen = 0;

        public SkillScene()
        {
            sb = new StringBuilder();
            skills = SkillLibrary.Instance.GetAllSkills();
            skillLen = skills.Count;
        }
        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("[스킬]");
            sb.AppendLine("원하시는 스킬을 선택하세요.");
            sb.AppendLine();
            for (int i = 1; i < skillLen; i++)
            {
                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine($"{skills[i].Name} | ");
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z, 취소 x");
            Console.Write(sb.ToString());
            SceneControl();
        }
        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selOptions != 1) selOptions--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selOptions != skillLen ) selOptions++;
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.Z:
                    BattleScene.Instance.CurrentState = BattleState.SelectMonster;
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
                    break;
                case ConsoleKey.X:
                    BattleScene.Instance.CurrentState = BattleState.SelectAction;
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.BattleScene;
                    break;
                default:
                    break;
            }
        }
    }
}
