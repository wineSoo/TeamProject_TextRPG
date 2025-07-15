using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    public static class MonsterLibrary
    {
        
        public static readonly List<Monster> Templates = new List<Monster>
        {
            new Monster("코스믹 슬라임", 2, 15, 6, 1, "우주에 있는 가장 평범한 몬스터."),
            new Monster("네뷸라 기생충", 5, 25, 10, 3, "성운(네뷸라) 구름 속을 유영하는 거대 기생충."),
            new Monster("궁허충", 3, 10, 7, 0, "빠르고 공격적인 벌레."),
            new Monster("외계인 병사", 1, 8, 4, 1, "우주 말단 병사.")
        };
        
        
        public static Monster CreateMonsterByName(string name)
        {
            // 1. Templates 리스트에서 이름이 같은 몬스터 원본을 찾음
            var original = Templates.Find(m => m.Name == name);
            if (original == null) return null;  //찾는 몬스터가 없으면 null 반환(=실패)

            //몬스터를 찾았으면 복사본을 만들어 반환
            return new Monster(
                original.Name, original.Level, original.MaxHp, original.Atk, original.Def, original.Description
            );
        }
        public static List<Monster> GetRandomMonsters(int count)
        {
            var rand = new System.Random();
            var monsters = new List<Monster>();
            for (int i = 0; i < count; i++)
            {
                var original = Templates[rand.Next(Templates.Count)];
                monsters.Add(new Monster(
                    original.Name, original.Level, original.MaxHp, original.Atk, original.Def, original.Description
                ));
            }
            return monsters;
        }
    }


}
