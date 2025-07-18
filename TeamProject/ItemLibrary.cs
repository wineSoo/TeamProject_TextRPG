using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{

    internal class ItemLibrary
    {
        private static ItemLibrary? instance;

        private List<Item> items;

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
            items[0].Heal = 0;
            items[0].Description = "부서지기 일보직전 상태의 낡은 검";
            items[0].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[1].Name = "낡은 방패";
            items[1].Atk = 0;
            items[1].Def = 2;
            items[1].HP = 0;
            items[1].Heal = 0;
            items[1].Description = "공격을 막으면 바로 부서질듯한 낡은 방패";
            items[1].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[2].Name = "작은 회복 물약";
            items[2].Atk = 0;
            items[2].Def = 0;
            items[2].Heal = 20;
            items[2].Quantity = 1;
            items[2].Description = "적은양의 체력을 회복하는 물약";
            items[2].Type = Item.ItemType.Consumable;

            items.Add(new Item());
            items[3].Name = "중간 회복 물약";
            items[3].Atk = 0;
            items[3].Def = 0;
            items[3].Heal = 40;
            items[3].Quantity = 1;
            items[3].Description = "적당한 양의 체력을 회복하는 물약";
            items[3].Type = Item.ItemType.Consumable;

            items.Add(new Item());
            items[4].Name = "작은 마나 물약";
            items[4].Atk = 0;
            items[4].Def = 0;
            items[4].Heal = 10;
            items[4].Quantity = 1;
            items[4].Description = "적은양의 마나를 회복하는 물약";
            items[4].Type = Item.ItemType.Consumable;

            items.Add(new Item());
            items[5].Name = "초보자의 검";
            items[5].Atk = 10;
            items[5].Def = 0;
            items[5].HP = 0;
            items[5].Heal = 0;
            items[5].Description = "누구나 쓸 쑤 있게 만든 가벼운 검";
            items[5].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[6].Name = "숙련자의 검";
            items[6].Atk = 15;
            items[6].Def = 0;
            items[6].HP = 0;
            items[6].Heal = 0;
            items[6].Description = "숙련자들이 쓸 수 있게 만든 날카로운 검";
            items[6].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[7].Name = "초보자의 방패";
            items[7].Atk = 0;
            items[7].Def = 5;
            items[7].HP = 0;
            items[7].Heal = 0;
            items[7].Description = "누구나 쓸 수 있게 만든 가벼운 방패";
            items[7].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[8].Name = "숙련자의 방패";
            items[8].Atk = 0;
            items[8].Def = 8;
            items[8].HP = 0;
            items[8].Heal = 0;
            items[8].Description = "숙련자들이 쓸 수 있게 만든 단단한 방패";
            items[8].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[9].Name = "날카로운 방패";
            items[9].Atk = 10;
            items[9].Def = 5;
            items[9].HP = 0;
            items[9].Heal = 0;
            items[9].Description = "공격적으로 사용가능하게 끝을 날카롭게 만든 방패";
            items[9].Type = Item.ItemType.Armor;

            items.Add(new Item());
            items[10].Name = "신성의 검";
            items[10].Atk = 20;
            items[10].Def = 5;
            items[10].HP = 0;
            items[10].Heal = 0;
            items[10].Description = "악마를 잡기에 특화된 강력한 검";
            items[10].Type = Item.ItemType.Weapon;

            items.Add(new Item());
            items[11].Name = "악마사냥꾼의 갑옷";
            items[11].Atk = 5;
            items[11].Def = 15;
            items[11].HP = 0;
            items[11].Heal = 0;
            items[11].Description = "악마 사냥꾼에 특화된 단단한 갑옷";
            items[11].Type = Item.ItemType.Armor;
        }

        public List<Item> GetAllItems()
        {
            return new List<Item>(items);
        }

    }
}
