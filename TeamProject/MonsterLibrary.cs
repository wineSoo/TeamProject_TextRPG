using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{

    public class MonsterLibrary
    {
        private List<Monster> monsters;       //몬스터들을 저장해두는 리스트(상자)

        public MonsterLibrary()
        {
            monsters = new List<Monster>();  
            CreateMonsters();
        }

        // 몬스터 데이터
        private void CreateMonsters()
        {
            monsters.Add(new Monster("미니언", 2, 15, 6, 1, "가장 평범한 몬스터."));
            monsters.Add(new Monster("대표미니언", 5, 25, 10, 3, "모든 미니언들의 우두머리."));
            monsters.Add(new Monster("궁허충", 3, 10, 7, 0, "빠르고 공격적인 벌레."));
        }

        // 몬스터 데이터를 다른 클래스로 전달하는 역할
        public List<Monster> GiveMonsterData()
        {
            return monsters;
        }
    }
}
