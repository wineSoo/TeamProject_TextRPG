using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{

    internal class ItemLibrary
    {
        private static ItemLibrary? instance;

        public List<Item> items { get; private set; }

        public static ItemLibrary Instance
        {
            get
            {
                if (instance == null)
                    instance = new ItemLibrary();
                return instance;
            }

        }


        public ItemLibrary()
        {
            items = new List<Item>();
            CreateItems();
        }


        private void CreateItems()
        {
            items.Add(new Item());
            items[0].Name = "낡은 검";
            items[0].Atk = 5;
            items[0].Def = 0;
            items[0].HP = 0;
            items[0].RestoreHp = 0;
            items[0].Description = "부서지기 일보직전 상태의 낡은 검";
            items[0].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[1].Name = "낡은 방패";
            items[1].Atk = 0;
            items[1].Def = 2;
            items[1].HP = 0;
            items[1].RestoreHp = 0;
            items[1].Description = "공격을 막으면 바로 부서질듯한 낡은 방패";
            items[1].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[2].Name = "작은 회복 물약";
            items[2].Atk = 0;
            items[2].Def = 0;
            items[2].RestoreHp = 20;
            items[2].Quantity = 1;
            items[2].Description = "적은양의 체력을 회복하는 물약";
            items[2].Type = Item.ItemType.ConsumableHP;

            items.Add(new Item());
            items[3].Name = "중간 회복 물약";
            items[3].Atk = 0;
            items[3].Def = 0;
            items[3].RestoreHp = 40;
            items[3].Quantity = 1;
            items[3].Description = "적당한 양의 체력을 회복하는 물약";
            items[3].Type = Item.ItemType.ConsumableHP;

            items.Add(new Item());
            items[4].Name = "작은 마나 물약";
            items[4].Atk = 0;
            items[4].Def = 0;
            items[4].RestoreMp = 10;
            items[4].Quantity = 1;
            items[4].Description = "적은양의 마나를 회복하는 물약";
            items[4].Type = Item.ItemType.ConsumableMP;

            items.Add(new Item());
            items[5].Name = "초보자의 검";
            items[5].Atk = 10;
            items[5].Def = 0;
            items[5].HP = 0;
            items[5].RestoreHp = 0;
            items[5].Description = "누구나 쓸 쑤 있게 만든 가벼운 검";
            items[5].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[6].Name = "초보자의 방패";
            items[6].Atk = 0;
            items[6].Def = 5;
            items[6].HP = 0;
            items[6].RestoreHp = 0;
            items[6].Description = "누구나 쓸 수 있게 만든 가벼운 방패";
            items[6].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[7].Name = "숙련자의 검";
            items[7].Atk = 15;
            items[7].Def = 0;
            items[7].HP = 0;
            items[7].RestoreHp = 0;
            items[7].Description = "숙련자들이 쓸 수 있게 만든 날카로운 검";
            items[7].Type = Item.ItemType.Weapon;
            items.Add(new Item());

            items.Add(new Item());
            items[8].Name = "숙련자의 방패";
            items[8].Atk = 0;
            items[8].Def = 8;
            items[8].HP = 0;
            items[8].RestoreHp = 0;
            items[8].Description = "숙련자들이 쓸 수 있게 만든 단단한 방패";
            items[8].Type = Item.ItemType.Armor;

            items[9].Name = "날카로운 방패";
            items[9].Atk = 10;
            items[9].Def = 5;
            items[9].HP = 0;
            items[9].RestoreHp = 0;
            items[9].Description = "공격적으로 사용가능하게 끝을 날카롭게 만든 방패";
            items[9].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[10].Name = "신성의 검";
            items[10].Atk = 20;
            items[10].Def = 5;
            items[10].HP = 0;
            items[10].RestoreHp = 0;
            items[10].Description = "악마를 잡기에 특화된 강력한 검";
            items[10].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[11].Name = "악마사냥꾼의 갑옷";
            items[11].Atk = 5;
            items[11].Def = 15;
            items[11].HP = 0;
            items[11].RestoreHp = 0;
            items[11].Description = "악마 사냥꾼에 특화된 단단한 갑옷";
            items[11].Type = Item.ItemType.Armor;
        }

        public Item GetRandomRewardItem()
        {
            Random rand = new Random();
            int roll = rand.Next(100);

            List<int> targetIndices;

            if (roll < 25)
            {
                targetIndices = new List<int> { 0, 1 }; // 25%
            }
            else if (roll < 85)
            {
                targetIndices = new List<int> { 2, 3, 4, 5, 6 }; // 60%
            }
            else if (roll < 95)
            {
                targetIndices = new List<int> { 7, 8, 9 }; // 10%
            }
            else
            {
                targetIndices = new List<int> { 10, 11 }; // 5%
            }

            // 해당 인덱스 중 하나 랜덤 선택 
            int selectedIndex = targetIndices[rand.Next(targetIndices.Count)];

            return new Item(items[selectedIndex]);
        }
    }
}
