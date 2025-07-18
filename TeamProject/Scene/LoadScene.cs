using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class LoadScene : Scene
    {
        public enum InputState
        {
            Input, NextScene
        }
        InputState state { get; set; }
        string? stringName;
        StringBuilder sb = new StringBuilder();

        public override void Render()
        {
            sb.Clear();
            sb.Append("불러올 플레이어 이름을 입력해주세요: ");
            switch (state)
            {
                case InputState.Input:
                    Console.Write(sb.ToString());
                    SceneControl();
                    break;
                case InputState.NextScene:
                    sb.AppendLine("플레이어 불러오기 완료.");
                    Console.Write(sb);
                    Thread.Sleep(2000);
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
                default:
                    break;
            }
            
        }

        protected override void SceneControl()
        {
            stringName = Console.ReadLine();
            if (stringName == null) return;

            Player? player = SaveManager.Load(stringName);
            if (player == null) // 불러오지 못했다면
            {
                Console.Write("해당 플레이어의 데이터를 찾을 수 없습니다.");
                Thread.Sleep(2000);
            }
            else // 불러왔다면
            {
                Player.Instance.LoadPlayer(player);
                state = InputState.NextScene;
            }
        }
        public override void SetupScene()
        {
            Console.CursorVisible = true; // 커서 보이기 o
            state= InputState.Input;
        }
        public override void FinishScene()
        {
            Console.CursorVisible = false; // 커서 보이기 x
        }
    }
}
