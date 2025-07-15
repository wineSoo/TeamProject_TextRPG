using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class InputNameScene : Scene
    {
        public InputNameScene()
        {
            sb = new StringBuilder();
            CreatScene();
            state = InputState.Input;

        }
        public enum InputState
        {
            Input, NextScene
        }
        InputState state { get; set; }

        StringBuilder sb;
        string? stringName;

        //public override void Render()
        public override void Render()
        {
            sb.Clear();
            sb.Append("이름을 입력해주세요. (Enter로 완료): ");
            if (stringName != null)
            {
                sb.AppendLine(stringName);
            }
            switch (state)
            {
                case InputState.Input:
                    Console.Write(sb);
                    SceneControl(); // 입력 대기
                    break;
                case InputState.NextScene:
                    sb.AppendLine("1초 후 인트로 씬으로 넘어갑니다.");
                    Console.Write(sb);
                    Thread.Sleep(1000);
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.GameIntroScene;
                    break;
                default:
                    break;
            }
        }
        protected override void SceneControl()
        {
            stringName = Console.ReadLine();
            Console.CursorVisible = false;
            if(stringName != null) Player.Instance.Name = stringName;
            state = InputState.NextScene;
        }

        void CreatScene()
        {
            sb.Append("이름을 입력해 주세요: ");
        }

    }   
}
