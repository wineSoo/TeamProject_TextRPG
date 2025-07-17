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
            monsters[0].Name = "쉐도우 임프";
            monsters[0].Level = 2;
            monsters[0].MaxHp = 15f;
            monsters[0].Hp = 15f;
            monsters[0].AtkPower = 6f;
            monsters[0].DefPower = 1f;
            monsters[0].skills.Add(new Skill(monsters[0]));
            monsters[0].Description = "어둠 속을 몰래 돌아다니는 작은 악마. 위협적이진 않지만 방심은 금물";

            monsters.Add(new Monster());
            monsters[1].Name = "다크 가디언";
            monsters[1].Level = 5;
            monsters[1].MaxHp = 25f;
            monsters[1].Hp = 25f;
            monsters[1].AtkPower = 10f;
            monsters[1].DefPower = 3f;
            monsters[1].skills.Add(new Skill(monsters[1]));
            monsters[1].Description = "높은 생명력과 방어력을 보유하고 있는 심연의 문을 지키는 수호자.";

            monsters.Add(new Monster());
            monsters[2].Name = "페일 위스프";
            monsters[2].Level = 3;
            monsters[2].MaxHp = 10f;
            monsters[2].Hp = 10f;
            monsters[2].AtkPower = 7f;
            monsters[2].DefPower = 0f;
            monsters[2].skills.Add(new Skill(monsters[2]));
            monsters[2].Description = "죽은 자의 영혼이 모여든 악령. 공격력은 강하지만 몸은 약하다.";

            //보스몬스터 생성(단일)
            bossMonster = new BossMonster();
            bossMonster.Name = "어비스 로드";
            bossMonster.Level = 10;           
            bossMonster.MaxHp = 50f;
            bossMonster.Hp = 50f;
            bossMonster.AtkPower = 15f;
            bossMonster.DefPower = 5f;
            bossMonster.Description = "심연의 지배자. 어둠의 힘을 다루며 모든 악마의 공포 대상이다.";
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
                //selected.Add(new Monster(m.Name, m.Level, m.MaxHp, m.AtkPower, m.DefPower, m.Description));
                selected.Add(new Monster(m));
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
            for (int i = 0;i < monster.skills.Count;i++)
            {
                monster.skills[i].SetDamge();
            }
        }
      

    }
}