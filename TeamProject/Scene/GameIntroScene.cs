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
            title = "게임 인트로 씬은 우선 한 글자 씩 출력되다가, 모든 내용이 출력된 후 넘어가도록 세팅하겠습니다. 추후 추가 작업하겠습니다.";
            sb = new StringBuilder();
        }
        int titleIdx = 0;
        string title;
        StringBuilder sb;
        // 0.3초마다 한 글자 씩 출력 되도록
        int time = 0;
        public override void Update()
        {
            time += SceneManager.Instance.delta;
            if (time >= 100)
            {
                time = 0;
                if (titleIdx >= title.Length)
                {
                    Thread.Sleep(1000);
                    SetupScene();// 무한 반복
                    return;
                }
                sb.Append(title[titleIdx]);
                titleIdx++;
            }
        }

        public override void Render()
        {
            Console.WriteLine(sb.ToString());
        }

        public override void SetupScene()
        {
            base.SetupScene();
            sb.Clear(); // 혹시 다시 인트로 씬으로 오더라도 글자 처음부터 출력되게 하기
            titleIdx = 0;
        }

        
    }
}
