using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    //library 몬스터 복사본
    public class MonsterManager
    {
        private MonsterLibrary monsterLibrary;    // 라이브러리 참조
        private List<Monster> activeMonsters;     // 현재 관리하는 몬스터들

        
        public MonsterManager(MonsterLibrary library) 
        {
            monsterLibrary = library;
            activeMonsters = new List<Monster>();
        }

        // 새로운 전투, 맵 등에 쓸 랜덤 몬스터 복사본 세팅
        public void SetBattleMonsters(int count)
        {
            activeMonsters = monsterLibrary.GetRandomMonsters(count);
        }

        // 현재 관리중인 몬스터 리스트 반환 (다른 씬에 사용)
        public List<Monster> GetActiveMonsters()
        {
            return activeMonsters;
        }

        // 전체 몬스터(마스터 데이터)도 접근 가능
        public List<Monster> GetAllMonsters()
        {
            return monsterLibrary.GetAllMonsters();
        }
    }

}
