using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class SceneManager
    {
        private static SceneManager? instance;
        private SceneManager()
        {
            
            // 씬 추가 예시
            scenes = new Dictionary<SceneState, Scene>();
            scenes.Add(SceneState.InputNameScene, new InputNameScene());
            /*scenes.Add(SceneState.StatScene, new StatScene());*/
            /*scenes.Add(SceneState.InventoryScene, new InventoryScene());*/
            /*scenes.Add(SceneState.ShopScene, new ShopScene());*/
            /*scenes.Add(SceneState.SellScene, new SellScene());*/
            /*scenes.Add(SceneState.Rest, new RestScene());*/
            /*scenes.Add(SceneState.Dungeon, new DungeonScene());*/

            // 시작은 이름 입력 씬으로
            sceneState = SceneState.InputNameScene;
        }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }

                return instance;
            }
        }
        public enum SceneState
        {
            InputNameScene, GameIntroScene, StartScene, StatScene, BattleScene, PlayerAttackScene, EnermyAttackScene, TestScene
        }

        private SceneState sceneState;
        // 씬 저장용
        private Dictionary<SceneState, Scene> scenes;

        int delta = 10;

        public SceneState SetSceneState
        {
            /*get
            {  return sceneState; }*/
            set
            {
                // 씬 스테이트 세팅하면 씬 세팅 자동 초기화 해보기
                sceneState = value;
                scenes[sceneState].SetupScene();
                ControlManager.ClearInputBuffer(); // 씬 넘어 갈 떄 기존에 입력된 키값들 없애기
            }
        }
        public Dictionary<SceneState, Scene> ScenesDict
        {
            get { return scenes; }
        }
        public Scene GetCurScene
        {
            get
            {
                return scenes[sceneState];
            }
        }
        public void StartScene()
        {
            // 윈도우 사이즈
            Console.SetWindowSize(120, 36);
            Console.CursorVisible = false; // 커서 보이기 x
            // 한번에 출력하여, 깜빡임 줄이기
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 110; j++)
                {
                    sb.Append(" ");
                }
                sb.Append("\n");
            }
            while (true)
            {
                // 키 입력 받아서 게임 상태 업데이트 시키기
                scenes[sceneState].Update();

                // 게임 상태 그리기
                Console.SetCursorPosition(0, 0);
                scenes[sceneState].Render();
                Thread.Sleep(delta);
                
                // 깜빡임 줄이기 위해 빈공간으로 덮어 쓰기
                Console.SetCursorPosition(0, 0);
                Console.Write(sb.ToString());
            }
        }
    }
}
