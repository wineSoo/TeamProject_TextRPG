using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject.Item;

namespace TeamProject
{
    internal class Player : Character
    {
        private static Player? instance;

        public enum PlayerJob { Warrior, Archer, Theif, Mage }
        public PlayerJob Job { get; set; }
        public float Mp { get; set; } // 기본 마나
        public float MaxMp { get; set; } // 장비 추가한 최대 마나

        public float Skill { get; set; } // 치명타율
        public float Speed { get; set; } // 회피율
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int DungeonFloor { get; set; }
        public float BattleStartHp { get; set; }
        public int PlusAtk { get; set; }
        public int PlusDef { get; set; }
        public int PlusHp { get; set; }
        public int PlusMp { get; set; }
        public int PlusSkill { get; set; }
        public int PlusSpeed { get; set; }

        public List<Item> Inventory;
        public Dictionary<Item.ItemType, Item> Equipments;

        private Player() : base()
        {
            Level = 1;
            Name = "이름 없는";
            Job = PlayerJob.Warrior;
            AtkPower = 30f; //과제 기본값 30
            DefPower = 5f;
            MaxHp = 100f;
            MaxMp = 50f;
            Skill = 15f;
            Speed = 10f;

            Hp = MaxHp; // 현재 체력은 최대 체력으로 초기화
            Mp = MaxMp; // 현재 마나는 최대 마나로 초기화

            PlusAtk = 0;
            PlusDef = 0;
            PlusHp = 0;
            PlusMp = 0;
            PlusSkill = 0;
            PlusSpeed = 0;

            Gold = 1500;
            Exp = 0;
            DungeonFloor = 1;

            skills.Add(new TeamProject.Skill(this));
            skills.Add(new AlphaStrike(this));
            skills.Add(new DoubleStrike(this));

            Inventory = new List<Item>();
            Equipments = new Dictionary<Item.ItemType, Item>();

            // 테스트 아이템 추가
            Inventory.Add(new Item
            {
                Name = "테스트 검1",
                Type = ItemType.Weapon,
                Atk = 10,
                Def = 0,
                Description = "시험용 무기1"
            });

            Inventory.Add(new Item
            {
                Name = "테스트 검2",
                Type = ItemType.Weapon,
                Atk = 10,
                Def = 0,
                Description = "시험용 무기2"
            });

            Inventory.Add(new Item
            {
                Name = "테스트 방어구1",
                Type = ItemType.Armor,
                Atk = 0,
                Def = 8,
                Description = "시험용 방어구1"
            });

            Inventory.Add(new Item
            {
                Name = "테스트 방어구2",
                Type = ItemType.Armor,
                Atk = 0,
                Def = 8,
                Description = "시험용 방어구2"
            });

            Inventory.Add(new Item
            {
                Name = "회복 물약",
                Type = ItemType.Consumable,
                Heal = 20,
                Quantity = 1,
                Description = "체력을 회복시킨다"
            });


        }
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }

        }
        public bool LevelCalculator(int expGained)
        {
            Exp += expGained;
            int expToLevelUP;
            bool isLevelUp = false;
            do
            {
                expToLevelUP = (5 * Level * Level + 35 * Level - 20) / 2;
                if (expToLevelUP <= Exp)
                {
                    Level++;
                    AtkPower += 0.5f;
                    DefPower++;
                    isLevelUp = true;
                    for (int i = 0; i < skills.Count; i++)
                    {
                        skills[i].SetDamge();
                    }
                }
            }
            while (expToLevelUP <= Exp);
            return isLevelUp;
        }
        public void StatInitializer(PlayerJob selectedjob)
        {
            switch (selectedjob)
            {
                case PlayerJob.Warrior:
                    Job = PlayerJob.Warrior;
                    Hp = 150f;
                    break;
                case PlayerJob.Archer:
                    Job = PlayerJob.Archer;
                    Skill = 25f;
                    break;
                case PlayerJob.Theif:
                    Job = PlayerJob.Theif;
                    Speed = 20f;
                    break;
                case PlayerJob.Mage:
                    Job = PlayerJob.Mage;
                    Mp = 100f;
                    break;
            }
            SetAbilityByEquipment();
            Hp = MaxHp;
            Mp = MaxMp;
        }
        public void UseSkill()
        {
            Mp -= skills[SelSkillNum].MP;
        }
        public bool CanUseSkill(int skillNum) // 해당 스킬이 사용 가능한가
        {
            if (skillNum >= skills.Count) return false; // 스킬 인덱스 초과

            if (Mp - skills[skillNum].MP < 0) return false; // 사용할 마나가 안되면 false

            return true;
        }
        // 취소 할 수도 있으니 마나 감소는 플레이어 공격씬 들어갈 때
        public void BattleFinish()
        {
            Mp = Mp + 10 >= MaxMp ? MaxMp : Mp + 10;
        }

        public void SetAbilityByEquipment()
        {
            PlusAtk = 0;
            PlusDef = 0;
            PlusHp = 0;
            PlusMp = 0;
            PlusSkill = 0;
            PlusSpeed = 0;

            foreach (var equip in Equipments.Values)
            {
                if (equip == null) continue;

                PlusAtk += equip.Atk;
                PlusDef += equip.Def;
                PlusHp += equip.HP;
                PlusMp += equip.MP;
                PlusSkill += equip.Skill;
                PlusSpeed += equip.Speed;
            }

            AtkPower += PlusAtk; // 기본 공격력 30
            DefPower += PlusDef; // 기본 방어력 5
            MaxHp += PlusHp; // 기본 체력 100
            MaxMp += PlusMp; // 기본 마나 50
            Skill += PlusSkill;
            Speed += PlusSpeed;
        }

        public bool IsEquipped(Item item)
        {
            foreach (var pair in Equipments)
            {
                if (pair.Value == item)
                    return true;
            }
            return false;
        }
        public void SetEquipment(Item item)
        {
            ItemType slot = GetEquipmentSlot(item);

            // 이미 장착되어 있던 아이템이면 제거
            if (Equipments.ContainsKey(slot) && Equipments[slot] == item)
            {
                Equipments.Remove(slot);
            }
            else
            {
                // 기존 장비가 있으면 교체
                Equipments[slot] = item;
            }
        }
        private ItemType GetEquipmentSlot(Item item)
        {

            return item.Type;

        }
    }
}