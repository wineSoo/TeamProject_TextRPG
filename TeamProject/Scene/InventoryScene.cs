using System;
using System.Collections.Generic;
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

            for (int i = 0; i < items.Count; i++)
            {
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
                    Console.WriteLine($"{equipTag} {item.Name} |공격력: {item.Atk} | 방어력: {item.Def} | {item.Description}");
                }
                else if (item.Type == ItemType.Consumable)
                {
                    Console.WriteLine($"   {item.Name} |회복력: {item.Heal} | 수량: {item.Quantity} | {item.Description}");
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

                case ConsoleKey.Z: // 아이템 선택
                    if (items != null && selOptions < items.Count)
                    {
                        Item selectedItem = items[selOptions];

                        if (selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Armor)
                        {
                            EquipItem(selOptions);
                        }
                        else if (selectedItem.Type == ItemType.Consumable)
                        {
                            // 소비 아이템 효과 기능 추가 해야함
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