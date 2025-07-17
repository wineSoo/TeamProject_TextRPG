using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ConditionDescription { get; set; } = "";
        public QuestType Type { get; set; } 
        public int ConditionNumber {get;set;}
        public int initialConditionNumber { get;set;}
        public int CurrentConditionNumber {get;set;}
        public int RewardGold { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsCleared { get; set; }
        public bool IsRewarded { get; set; }

        // [미완성] 보상 아이템 추가 필요

        public enum QuestType
        {
            KillMinion, Equip, IncreaseLevel
        }
 
        public void QuestAccept()
        {
            IsAccepted = true;
            switch (Type)
            {
                case QuestType.KillMinion:



                    break;
                case QuestType.Equip:
                    break;
                case QuestType.IncreaseLevel:
                    initialConditionNumber = Player.Instance.Level;
                    break;
            }
        }

        
        public void QuestClearCheck()
        {
            switch (Type)
            {
                case QuestType.KillMinion:



                    break;
                case QuestType.Equip:
                    break;
                case QuestType.IncreaseLevel:
                    CurrentConditionNumber = Player.Instance.Level - initialConditionNumber;
                    if (CurrentConditionNumber > ConditionNumber)
                        IsCleared = true;
                    break;
            }
        }

        public void QuestReward()
        {
            IsRewarded = true;
            Player.Instance.Gold += RewardGold;
        }

    }
}
