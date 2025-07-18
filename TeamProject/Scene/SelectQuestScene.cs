using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class SelectQuestScene : Scene
    {
       public SelectQuestScene()
       {
            selectedQuest = QuestManager.Instance.questList[QuestManager.Instance.SelectQuestVariable];
            sb = new StringBuilder();
            options.Add($"1. 수락");
            options.Add($"2. 거절");
            optionsLen = options.Count;
       }

        StringBuilder sb;
        int selOptions = 0;
        Quest selectedQuest;

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Quest!!");
            sb.AppendLine();
            sb.AppendLine(selectedQuest.Name);
            sb.AppendLine();
            sb.AppendLine(selectedQuest.Description);
            sb.AppendLine();
            sb.AppendLine($"- {selectedQuest.ConditionDescription} ({selectedQuest.CurrentConditionNumber}/{selectedQuest.ConditionNumber})");
            sb.AppendLine();
            sb.AppendLine("- 보상-");
            sb.AppendLine();
            sb.AppendLine($"  {selectedQuest.RewardItem} x {selectedQuest.RewardItemQuantity}");
            sb.AppendLine($"  {selectedQuest.RewardGold} G");
            sb.AppendLine();
            for (int i = 0; i < optionsLen; i++)
            {
                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("　 ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
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
                case ConsoleKey.Z:
                    if (!selectedQuest.IsAccepted)
                    {
                        switch (selOptions)
                        {
                            case 0: // 수락
                                selectedQuest.QuestAccept();
                                Console.WriteLine("\n수락되었습니다");
                                Thread.Sleep(1000);
                                SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                                break;
                            case 1: // 거절 && 돌아가기
                                SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                    else if (selectedQuest.IsCleared && !selectedQuest.IsRewarded)
                    {
                        switch (selOptions)
                        {
                            case 0: // 보상 수령
                                selectedQuest.QuestReward();
                                Console.WriteLine("\n보상을 받았습니다!");
                                Thread.Sleep(1000);
                                SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                                break;
                            case 1: // 돌아가기
                                SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                    else
                    {
                        SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                    }
                    break;
                case ConsoleKey.X:
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.QuestScene;
                    break;
                default:
                    break;
            }

        }

        public override void SetupScene()
        {
            base.SetupScene();
            selectedQuest = QuestManager.Instance.questList[QuestManager.Instance.SelectQuestVariable];

            if (selectedQuest.IsAccepted)  //퀘스트 진행도를 체크
                selectedQuest.QuestClearCheck(); 

            options.Clear();
            if (!selectedQuest.IsAccepted)
            {
                options.Add("1. 수락");
                options.Add("2. 거절");
            }
            else if (selectedQuest.IsCleared && !selectedQuest.IsRewarded)
            {
                options.Add("1. 보상 받기");
                options.Add("2. 돌아가기");
            }
            else
            {
                options.Add("1.돌아가기");
            }
            optionsLen = options.Count;

        }


    }
}
