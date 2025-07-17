using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{

    public class MonsterLibrary
    {
        private List<Monster> monsters;
        private BossMonster? bossMonster;
        private readonly Random rnd;

        public MonsterLibrary()
        {
            monsters = new List<Monster>();
            rnd = new Random();
            CreateMonsters();
        }

        //몬스터 정보 등록
        private void CreateMonsters()
        {
            monsters.Add(new Monster());
            monsters[0].Name = "미니언";
            monsters[0].Level = 2;
            monsters[0].MaxHp = 15f;
            monsters[0].Hp = 15f;
            monsters[0].AtkPower = 6f;
            monsters[0].DefPower = 1f;
            monsters[0].Description = "가장 평범한 몬스터.";

            monsters.Add(new Monster());
            monsters[1].Name = "대포미니언";
            monsters[1].Level = 5;
            monsters[1].MaxHp = 25f;
            monsters[1].Hp = 25f;
            monsters[1].AtkPower = 10f;
            monsters[1].DefPower = 3f;
            monsters[1].Description = "무시무시한 대포를 장착한 미니언.";

            monsters.Add(new Monster());
            monsters[2].Name = "궁허충";
            monsters[2].Level = 3;
            monsters[2].MaxHp = 10f;
            monsters[2].Hp = 10f;
            monsters[2].AtkPower = 7f;
            monsters[2].DefPower = 0f;
            monsters[2].Description = "빠르고 공격적인 벌레.";

            //보스몬스터 생성(단일)
            bossMonster = new BossMonster();
            bossMonster.Name = "바론";
            bossMonster.Level = 10;           
            bossMonster.MaxHp = 100f;
            bossMonster.Hp = 100f;
            bossMonster.AtkPower = 20f;
            bossMonster.DefPower = 5f;
            bossMonster.Description = "벌레들의 왕.";
        }

        // 전체 몬스터 복사본 반환
        public List<Monster> GetAllMonsters()
        {
            var result = new List<Monster>();
            foreach (var m in monsters)
            {
                result.Add(new Monster(m.Name, m.Level, m.MaxHp, m.AtkPower, m.DefPower, m.Description));
            }
            return result;
        }
        // 단일 보스 몬스터 복사본 반환
        public BossMonster GetBossMonster()
        {
            var m = bossMonster;
            return new BossMonster(m.Name!, m.Level, m.MaxHp, m.AtkPower, m.DefPower, m.Description);
        }

        //랜덤 N마리 복사본 반환 (중복X)
        public List<Monster> GetRandomMonstersR(int count)
        {
            List<int> used = new List<int>();
            List<Monster> selected = new List<Monster>();

            for (int i = 0; i < count; i++)
            {
                int idx;
                do
                {
                    idx = rnd.Next(monsters.Count);
                } while (used.Contains(idx));

                used.Add(idx);
                var m = monsters[idx];
                selected.Add(new Monster(m.Name, m.Level, m.MaxHp, m.AtkPower, m.DefPower, m.Description));
            }
            return selected;
        }
        // 랜덤 N마리 복사본 반환 (중복O)
        public List<Monster> GetRandomMonsters(int count)
        {
            List<Monster> selected = new List<Monster>();

            for (int i = 0; i < count; i++)
            {
                int idx = rnd.Next(monsters.Count);
                Monster m = monsters[idx];
                selected.Add(new Monster(m.Name, m.Level, m.MaxHp, m.AtkPower, m.DefPower, m.Description));
                LevelIncreaser(selected[i], Player.Instance.DungeonFloor);


            }
            return selected;
        }

        void LevelIncreaser(Monster monster, int dungeonFloor)
        {
            monster.Level += dungeonFloor - 1;
            monster.Hp += (dungeonFloor / 2) * 5;
            monster.MaxHp += (dungeonFloor / 2) * 5;
            monster.AtkPower += dungeonFloor / 2;
            monster.DefPower += dungeonFloor - 1;

        }
      

    }
}