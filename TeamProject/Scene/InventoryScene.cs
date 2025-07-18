using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static TeamProject.Item;

namespace TeamProject
{
    internal class InventoryScene : Scene
    {
        private ItemLibrary itemLibrary;
        List<Item>? items = Player.Instance.Inventory;

        public InventoryScene()
        {
            itemLibrary = ItemLibrary.Instance;
            sb = new StringBuilder();
            optionsLen = items.Count;
        }

        StringBuilder sb;
        int selOptions = 0;

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("인벤토리");
            sb.AppendLine();
            sb.AppendLine("아이템을 선택해 주세요.");

            if (Player.Instance.Inventory.Count == 0)
            {
                sb.AppendLine("아이템이 없습니다.");
                Console.Write(sb.ToString());
            }
            else
            {
                Console.Write(sb.ToString());
                PrintInventory(); // 아이템 출력
            }

            sb.Clear();
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z, 돌아가기: x");
            Console.Write(sb.ToString());

            SceneControl();
        }

        private void PrintInventory()
        {
            Player player = Player.Instance;
            var items = player.Inventory;

            if (selOptions < 0) selOptions = 0;
            if (selOptions >= items.Count) selOptions = items.Count - 1;

            for (int i = 0; i < items.Count; i++)
            {
                int npad = padding - GetDisplayWidth(items[i].Name);
                int apad = apadding - GetDisplayWidth(items[i].Atk.ToString());
                int hpad = apadding - GetDisplayWidth(items[i].RestoreHp.ToString());
                int qpad = apadding - GetDisplayWidth(items[i].Quantity.ToString());
                int epad = exPadding - GetDisplayWidth(items[i].Description);

                Item item = items[i];
                bool isEquipped = player.IsEquipped(item);
                string equipTag = "  ";

                if ((item.Type == ItemType.Weapon || item.Type == ItemType.Armor) && isEquipped)
                {
                    equipTag = "[E]";
                }

                if (selOptions == i) Console.Write("▶ ");
                else Console.Write("  ");

                if (item.Type == ItemType.Weapon || item.Type == ItemType.Armor)
                {
                    Console.WriteLine($"{equipTag} {item.Name}{new string(' ', npad)}{(item.Type == ItemType.Weapon ? $"| 공격력: {item.Atk}" : $"| 방어력: {item.Def}")}{new string(' ', apad)}| {item.Description}");
                }
                else if (item.Type == ItemType.ConsumableHP || item.Type == ItemType.ConsumableMP)
                {
                    Console.WriteLine($"   {item.Name}{new string(' ', npad)}| 회복력: {(item.Type == ItemType.ConsumableHP ? item.RestoreHp : item.RestoreMp)}{new string(' ', hpad)}| 수량: {item.Quantity}{new string(' ', qpad)}| {item.Description}");
                }
            }
        }

        public void EquipItem(int idx)
        {
            Player player = Player.Instance;

            if (idx < 0 || idx >= player.Inventory.Count)
                return;

            Item item = player.Inventory[idx];

            // 장비 아이템만 처리
            if (item.Type != ItemType.Armor && item.Type != ItemType.Weapon)
                return;

            // 장착 중이면 해제, 아니면 장착
            player.SetEquipment(item);
        }

        private void UseConsumable(Item selectedItem)
        {
            Player player = Player.Instance;
            int hpamount = selectedItem.RestoreHp;
            int mpAmount = selectedItem.RestoreMp;
            
            if (selectedItem.Type == ItemType.ConsumableHP)
            {
                if (player.Hp == player.MaxHp)
                {
                    Console.WriteLine("체력이 가득 찼습니다.");
                    Thread.Sleep(1000);
                    return;
                }

                if (player.Hp < player.MaxHp - hpamount)
                {
                    player.Hp += hpamount;
                    Console.WriteLine($"체력이 {hpamount}회복 되었습니다");
                }
                else
                {
                    float realHeal = player.MaxHp - player.Hp;
                    player.Hp = player.MaxHp;
                    Console.WriteLine($"체력이 {realHeal} 회복 되었습니다");
                }
            }
            else // ConsumableMP
            {
                if (player.Mp == player.MaxMp)
                {
                    Console.WriteLine("마나가 가득 찼습니다.");
                    Thread.Sleep(1000);
                    return;
                }

                if (player.Mp < player.MaxMp - mpAmount)
                {
                    player.Mp += mpAmount;
                    Console.WriteLine($"마나가 {mpAmount}회복 되었습니다");
                }
                else
                {
                    float realRestore = player.MaxMp - player.Mp;
                    player.Mp = player.MaxMp;
                    Console.WriteLine($"마나가 {realRestore} 회복 되었습니다");
                }
            }

            Thread.Sleep(1000);

            selectedItem.Quantity--;
            if (selectedItem.Quantity <= 0)
            {
                Player.Instance.Inventory.Remove(selectedItem);
                Console.WriteLine($"{selectedItem.Name}을(를) 모두 사용했습니다.");
                Thread.Sleep(1000);
            }
        }

        public int GetDisplayWidth(string s)
        {
            int width = 0;
            foreach (char c in s)
            {
                if (IsKorean(c))
                    width += 2;
                else
                    width += 1;
            }
            return width;
        }
        public bool IsKorean(char c)
        {
            // 한글 완성형 범위: U+AC00 ~ U+D7A3
            return c >= 0xAC00 && c <= 0xD7A3;
        }

        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int itemCount = Player.Instance.Inventory.Count;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selOptions--;
                    if (selOptions < 0) selOptions = 0;
                    break;

                case ConsoleKey.DownArrow:
                    selOptions++;
                    if (selOptions >= itemCount) selOptions = itemCount - 1;
                    break;

                case ConsoleKey.Z: // 아이템 선택
                    if (itemCount > 0 && selOptions < itemCount)
                    {
                        Item selectedItem = Player.Instance.Inventory[selOptions];

                        if (selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Armor)
                        {
                            EquipItem(selOptions);
                        }
                        else if (selectedItem.Type == ItemType.ConsumableHP || selectedItem.Type == ItemType.ConsumableMP)
                        {
                            UseConsumable(selectedItem);
                        }
                    }
                    break;

                case ConsoleKey.X: // 나가기
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;

                default:
                    break;
            }
        }
    }
}