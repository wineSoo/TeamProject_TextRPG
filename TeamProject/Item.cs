using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // 고유 식별자
        public string Name { get; set; } // 아이템 이름
        public int Atk { get; set; }// 아이템 공격력
        public int Def { get; set; } // 아이템 방어력
        public int HP { get; set; } // 아이템 체력
        public int MP { get; set; } // 아이템 마나
        public int Skill { get; set; } // 아이템 치명타율
        public int Speed { get; set; } // 아이템 속도
        public int RestoreHp { get; set; } // 아이템 체력 회복량
        public int RestoreMp { get; set; } // 아이템 마나 회복량
        public int Quantity { get; set; } // 아이템 수량
        public int Price { get; set; } // 아이템 가격
        public string Description { get; set; } // 아이템 설명
        public ItemType Type { get; set; } // 아이템 타입
        public enum ItemType
        {
            Weapon,
            Armor,
            ConsumableHP, // 소모품
            ConsumableMP,
        }
        public Item()
        {
            Name = "";
            Description = "";
            Atk = 0;
            Def = 0;
            HP = 0;
            MP = 0;
            Skill = 0;
            Speed = 0;
            RestoreHp = 0;
            RestoreMp = 0;
            Quantity = 1;
            Price = 0;
            Type = ItemType.Weapon; // 기본값은 장비
        }
        public Item(string name, int atk, int def,int hp,int mp,int skill, int speed, int restorehp, int restoremp,  int quantity, int price, string description, ItemType type)
        {
            Name = name;
            Atk = atk;
            Def = def;
            HP = hp;
            MP = mp;
            Skill = skill;
            Speed = speed;
            RestoreHp = restorehp;
            RestoreMp = restoremp;
            Quantity = quantity;
            Price = price;
            Description = description;
            Type = type;
        }

        public Item(Item other)
        {
            Name = other.Name;
            Atk = other.Atk;
            Def = other.Def;
            HP = other.HP;
            MP = other.MP;
            Skill = other.Skill;
            Speed = other.Speed;
            RestoreHp = other.RestoreHp;
            RestoreMp = other.RestoreMp;
            Quantity = other.Quantity;
            Price = other.Price;
            Description = other.Description;
            Type = other.Type;
        }
    }
}
