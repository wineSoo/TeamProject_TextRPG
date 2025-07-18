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
            scenes.Add(SceneState.GameIntroScene, new GameIntroScene());
            scenes.Add(SceneState.StartScene, new StartScene());
            scenes.Add(SceneState.StatScene, new StatScene());
            scenes.Add(SceneState.PlayerAttackScene, new PlayerAttackScene());
            scenes.Add(SceneState.EnemyAttackScene, new EnemyAttackScene());
            scenes.Add(SceneState.BattleScene, new BattleScene());
            scenes.Add(SceneState.WinEndScene, new WinEndScene());
            scenes.Add(SceneState.LoseEndScene, new LoseEndScene());
            scenes.Add(SceneState.JobSelectScene, new JobSelectScene());
            scenes.Add(SceneState.QuestScene, new QuestScene());
            scenes.Add(SceneState.SelectQuestScene, new SelectQuestScene());
            scenes.Add(SceneState.FinalScene, new FinalScene());
            scenes.Add(SceneState.InventoryScene, new InventoryScene());

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

            InputNameScene, GameIntroScene, JobSelectScene, StartScene, StatScene, BattleScene, PlayerAttackScene, EnemyAttackScene, TestScene, WinEndScene, LoseEndScene, SkillScene, QuestScene, SelectQuestScene, InventoryScene,FinalScene

        }
        private SceneState sceneState;
        // 씬 저장용
        private Dictionary<SceneState, Scene> scenes;
        public string tmpS = ""; // 밀어내기용

        public SceneState SetSceneState
        {
            /*get
            {  return sceneState; }*/
            set
            {
                // 씬 변경 후 실행해야 할 것들 실행
                scenes[sceneState].FinishScene();
                // 씬 스테이트 세팅하면 씬 세팅 자동 초기화 해보기
                sceneState = value;
                scenes[sceneState].SetupScene();
                ControlManager.ClearInputBuffer(); // 씬 넘어 갈 떄 기존에 입력된 키값들 없애기
            }
        }
        public void StartScene()
        {
            // 윈도우 사이즈
            Console.SetWindowSize(120, 36);
            Console.CursorVisible = false; // 커서 보이기 x
            //Console.CursorVisible = true;
            Console.OutputEncoding = Encoding.UTF8;
            // 한번에 출력하여, 깜빡임 줄이기
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 120; j++)
                {
                    sb.Append(" ");
                }
                sb.AppendLine();
            }
            tmpS = sb.ToString();
            while (true)
            {
                // 깜빡임 줄이기 위해 빈공간으로 덮어 쓰기

                // 게임 상태 그리기
                scenes[sceneState].Render();

                ClearScene();

            }
        }
        public void ClearScene()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(tmpS);
            Console.SetCursorPosition(0, 0);
        }
    }
}
