using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    // MonsterLibrary에서 몬스터 복사본을 받아 실제로 관리
    public class MonsterManager
    {
        private MonsterLibrary monsterLibrary;    // 라이브러리 참조
        private List<Monster> activeMonsters;     // 현재 관리하는 몬스터들
       
        public MonsterManager(MonsterLibrary library)
        {
            monsterLibrary = library;
            activeMonsters = new List<Monster>();
        }
        
        public void SetBattleMonsters(int count)
        {
            activeMonsters = monsterLibrary.GetRandomMonsters(count);
        }
       
        public List<Monster> GetActiveMonsters()
        {            
            return activeMonsters;
        }
        
        public List<Monster> GetAllMonsters()
        {
            return monsterLibrary.GetAllMonsters();
        }
    }
}
