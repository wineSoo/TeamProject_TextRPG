using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TeamProject
{
    internal static class SaveManager
    {
        //private static readonly string savePath = "player_save.json";
        public static void Save(Player player)
        {
            player.PrepareForSave();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };
            string json = JsonSerializer.Serialize(player, options);
            File.WriteAllText(player.Name + ".json", json);
            Console.WriteLine("플레이어 저장 중.");
            Thread.Sleep(500);
            Console.WriteLine("플레이어 저장 완료.");
            Thread.Sleep(800);
        }

        public static Player? Load(string savePath)
        {
            if (!File.Exists(savePath + ".json"))
            {
                return null;
            }

            string json = File.ReadAllText(savePath + ".json");
            var options = new JsonSerializerOptions
            {
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            Player? player = JsonSerializer.Deserialize<Player>(json, options);
            /*Console.WriteLine("플레이어 불러오기 완료.");
            Thread.Sleep(2000);
            Console.Clear();*/
            return player;
        }
    }
}
