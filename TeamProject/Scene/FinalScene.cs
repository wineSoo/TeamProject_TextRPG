using System;
using System.Text;

namespace TeamProject
{
    internal class FinalScene : Scene
    {
        private Player player;
        public FinalScene()
        {
            this.player = Player.Instance;
            sb = new StringBuilder();
            options.Add("처음으로");
            optionsLen = options.Count;
        }

        StringBuilder sb;
        int selOptions = 0;

        public override void Render()
        {
            sb.Clear();
            sb.AppendLine("Battle!! - Result");
            sb.AppendLine();
            sb.AppendLine("Victory!");
            sb.AppendLine("던전 최종 보스 '어비스 로드'를 쓰러뜨렸습니다!");
            sb.AppendLine();
            sb.AppendLine("[캐릭터 정보]");
            sb.AppendLine($"Lv.{player.Level} {player.Name}");
            sb.AppendLine($"HP {player.BattleStartHp} -> {player.Hp}");
            sb.AppendLine($"exp {player.Exp}");
            sb.AppendLine();

            sb.AppendLine("========================================");
            sb.AppendLine("           🎮 Soul C# ");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("   팀장: 이원진");
            sb.AppendLine("   조원: 김세웅, 정희찬, 조수호\n");
            sb.AppendLine("   Special Thanks:");
            sb.AppendLine("     플레이해주셔서 감사합니다!\n");
            sb.AppendLine("========================================\n");


            for (int i = 0; i < optionsLen; i++)
            {
                if (selOptions == i) sb.Append("▶ ");
                else sb.Append("  ");
                sb.AppendLine(options[i]);
            }
            sb.AppendLine();
            sb.AppendLine("이동: 방향키, 선택: z");
            Console.Write(sb.ToString());
            SceneControl();
        }

        protected override void SceneControl()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Z:
                    SceneManager.Instance.SetSceneState = SceneManager.SceneState.StartScene;
                    break;
            }
        }
    }
}