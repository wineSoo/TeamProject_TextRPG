using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class GameIntroScene : Scene
    {
        public GameIntroScene()
        {
            title = "게임 인트로 씬은 우선 한 글자 씩 출력되다가, 모든 내용이 출력된 후 넘어가도록 세팅하겠습니다.";
            sb = new StringBuilder();
        }
        int titleIdx = -2;
        string title;
        StringBuilder sb;
        // 0.3초마다 한 글자 씩 출력 되도록

        public override void Render()
        {
            sb.Clear();
            if(titleIdx < title.Length)
            {
                titleIdx++;
            }
            for (int i = 0; i < titleIdx; i++)
            {
                sb.Append(title[i]);
            }
            if(titleIdx == title.Length) // 다 출력 했다면
            {
                sb.Append("\n아무 키를 누르면 다음 씬으로 넘어갑니다.(테스트용, 구현 다했으면 자동으로 넘어감)");
                Console.WriteLine(sb.ToString());
                SceneControl(); // 키입력 대기 = 화면 멈추기
                SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;

            }
            else // 출력할 게 남았다면
            {
                Console.WriteLine(sb.ToString());
                Thread.Sleep(50); // ms 마다 한 글자씩
            }
        }

        public override void SetupScene()
        {
            base.SetupScene();
            sb.Clear(); // 혹시 다시 인트로 씬으로 오더라도 글자 처음부터 출력되게 하기
            titleIdx = -2;
        }

        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        }
    }
    
}
