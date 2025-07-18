using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class QuestManager
    {
        private static QuestManager? instance;
        private QuestManager()
        {
            questList = new List<Quest>()
            {
                new Quest() { },
                new Quest() {
                                Id = 1,
                                Name = "마을을 위협하는 쉐도우 임프 처치",
                                Description = "이봐! 마을 근처에 쉐도우 임프들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",
                                Type = Quest.QuestType.KillShadowImp,
                                ConditionNumber = 5,
                                ConditionDescription = "쉐도우 임프 5마리 처치",
                                RewardItem = "작은 물약",
                                RewardItemQuantity = 3,
                                RewardGold = 5
                            },
                new Quest() {
                                Id = 2,
                                Name = "장비를 장착해보자",
                                Description = "설마 자네 맨몸으로 몬스터랑 싸우는 건가?\n 너무 위험하네! 아무리 실력이 있어도 목숨을 소중히 하라고\n자네가 걱정되서 이러다 잠도 안오겠어",
                                Type = Quest.QuestType.Equip,
                                ConditionNumber = 1,
                                ConditionDescription = "아무 장비 착용하고 오기",
                                RewardItem = "작은 물약",
                                RewardItemQuantity = 1,
                                RewardGold = 15,
                            },
                new Quest() {
                                Id = 3,
                                Name = "더욱 더 강해지기!",
                                Description = "자네 덕분에 마을이 안전해지고 있어!\n그래도 던전은 갈수록 위험해지니, 만만히 보면 안된다네.\n열심히 경험을 쌓아서 더욱 강해져야 한다고!",
                                Type = Quest.QuestType.IncreaseLevel,
                                ConditionNumber = 1,
                                ConditionDescription = "레벨 1이상 추가로 올리기",
                                RewardItem = "작은 물약",
                                RewardItemQuantity = 2,
                                RewardGold = 100
                            }

            };

            killCounts = new Dictionary<Character.MonsterIndex, int> { { Character.MonsterIndex.ShadowImp, 0 }, { Character.MonsterIndex.DarkGuardian, 0 }, { Character.MonsterIndex.PaleWhisp, 0 }, { Character.MonsterIndex.AbyssLord, 0 } };
        }
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }

        public List<Quest> questList;
        public int SelectQuestVariable {get; set;}


        public Dictionary<Character.MonsterIndex, int> killCounts;

        public void KillCuntsUp(Character.MonsterIndex monsterIndex)
        {
            if (killCounts.ContainsKey(monsterIndex))
                killCounts[monsterIndex]++;
            else
                killCounts[monsterIndex] = 1;
        }

    }
}
