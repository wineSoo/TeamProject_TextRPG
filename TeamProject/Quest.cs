using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject.Item;

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

        //public Item? RewardItem { get; set; }
        public string? RewardItem { get; set; } // 원래는 아이템을 받아와야 하지만, 현재 보상 아이템이 회복 아이템이 전부라서 일단 이렇게 구현
        public int RewardItemQuantity { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsCleared { get; set; }
        public bool IsRewarded { get; set; }



        // [미완성] 보상 아이템 추가 필요

        public enum QuestType
        {
            KillShadowImp, Equip, IncreaseLevel
        }
 
        public void QuestAccept()
        {
            IsAccepted = true;
            switch (Type)
            {
                case QuestType.KillShadowImp:
                    initialConditionNumber = QuestManager.Instance.killCounts[Character.MonsterIndex.ShadowImp];

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
                case QuestType.KillShadowImp:

                    CurrentConditionNumber = QuestManager.Instance.killCounts[Character.MonsterIndex.ShadowImp] - initialConditionNumber;
                    if (CurrentConditionNumber >= ConditionNumber)
                        IsCleared = true;
                    break;
                case QuestType.Equip:
                    CurrentConditionNumber = Player.Instance.Equipments.Count >= 1 ? Player.Instance.Equipments.Count : 0;
                    IsCleared = CurrentConditionNumber >= 1 ? true : false;
                    break;
                case QuestType.IncreaseLevel:
                    CurrentConditionNumber = Player.Instance.Level - initialConditionNumber;
                    if (CurrentConditionNumber >= ConditionNumber)
                        IsCleared = true;
                    break;
            }
        }

        public void QuestReward()
        {
            IsRewarded = true;
            Player.Instance.Gold += RewardGold;

            // 아이템 보상 지급
            if (RewardItem != null)
            {
                var inventory = Player.Instance.Inventory;
                var consumableList = inventory.FirstOrDefault(
                    i => i.Type == Item.ItemType.ConsumableHP // 주의: 현재 가장 먼저 검색된 소비 아이템을 찾음 => 소비 아이템이 한 종류라 가능, 종류 늘어나면 반드시 수정
                );

                if (consumableList != null)
                {
                    consumableList.Quantity += RewardItemQuantity;
                }
                else
                {
                    inventory.Add(new Item
                    {
                        Name = "회복 물약",
                        Type = ItemType.ConsumableHP,
                        RestoreHp = 20,
                        Quantity = RewardItemQuantity,
                        Description = "체력을 회복시킨다"
                    });
                }
            }

        }

    }
}
