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
       
        public MonsterManager(MonsterLibrary library) //매니저가 라이브러리 내용을 사용할수있게 저장
        {
            monsterLibrary = library;
            activeMonsters = new List<Monster>();
        }
        
        public void SetBattleMonsters(int count)  //전투에 등장할 몬스터 N마리를 “도감”에서 복사해서 세팅
        {
            activeMonsters = monsterLibrary.GetRandomMonsters(count);
        }
       
        public List<Monster> GetActiveMonsters()  //현재 전투 중인 몬스터들 리스트를 외부에 제공
        {            
            return activeMonsters;
        }
        
        public List<Monster> GetAllMonsters()  //MonsterLibrary에 저장된 전체 몬스터 복사본을 외부로 제공
        {
            return monsterLibrary.GetAllMonsters();
        }
    }
}
