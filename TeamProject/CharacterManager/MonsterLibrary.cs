using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject.CharacterManager
{
     
    public class MonsterLibrary
    {
        //모든 몬스터 종류를 저장
        private List<Monster> monsters;

        //생성자에서 한 번만 데이터 등록
        public MonsterLibrary()
        {
            monsters = new List<Monster>();
            rnd = new Random();
            CreateMonsters();
        }
        Random rnd;

        //몬스터 정보 등록
        private void CreateMonsters()
        {
            monsters.Add(new Monster());
            monsters[0].Name = "미니언";
            monsters[0].Level = 2;
            monsters[0].MaxHp = 15;
            monsters[0].Hp = 15;
            monsters[0].Atk = 6;
            monsters[0].Def = 1;
            monsters[0].Description = "가장 평범한 몬스터.";

            monsters.Add(new Monster());
            monsters[1].Name = "대포미니언";
            monsters[1].Level = 5;
            monsters[1].MaxHp = 25;
            monsters[1].Hp = 25;
            monsters[1].Atk = 10;
            monsters[1].Def = 3;
            monsters[1].Description = "무시무시한 대포를 장착한 미니언.";

            monsters.Add(new Monster());
            monsters[2].Name = "궁허충";
            monsters[2].Level = 3;
            monsters[2].MaxHp = 10;
            monsters[2].Hp = 10;
            monsters[2].Atk = 7;
            monsters[2].Def = 0;
            monsters[2].Description = "빠르고 공격적인 벌레.";
        }

        //전체 몬스터 복사본 리스트 반환
        public List<Monster> GetAllMonsters()
        {
            List<Monster> result = new List<Monster>();
            foreach (var m in monsters)
            {
                result.Add(new Monster(m.Name, m.Level, m.MaxHp, m.Atk, m.Def, m.Description));
            }
            return result;
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
                selected.Add(new Monster(m.Name, m.Level, m.MaxHp, m.Atk, m.Def, m.Description));
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
                selected.Add(new Monster(m.Name, m.Level, m.MaxHp, m.Atk, m.Def, m.Description));
                LevelIncreaser(selected[i], Player.Instance.DungeonFloor);


            }
            return selected;
        }

        void LevelIncreaser(Monster monster, int dungeonFloor)
        {
            monster.Level += dungeonFloor - 1;
            monster.Hp += (dungeonFloor / 2) * 5;
            monster.MaxHp += (dungeonFloor / 2) * 5;
            monster.Atk += dungeonFloor / 2;
            monster.Def += dungeonFloor - 1;

        }


    }
}