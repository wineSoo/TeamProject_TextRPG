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
            items[2].Name = "작은 물약";
            items[2].Atk = 0;
            items[2].Def = 0;
            items[2].Heal = 10;
            items[2].Quantity = 1;
            items[2].Description = "적은양의 체력을 회복하는 물약";
            items[2].Type = Item.ItemType.Consumable;

        }

        public List<Item> GetAllItems()
        {
            return new List<Item>(items);
        }

    }
}
