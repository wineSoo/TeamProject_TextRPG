using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject.SceneManager;

namespace TeamProject
{
    // MonsterLibrary에서 몬스터 복사본을 받아 실제로 관리
    public class MonsterManager
    {
        private static MonsterManager? instance;
        private MonsterManager()
        {
            MonsterLibrary = new MonsterLibrary();
            //activeMonsters;
        }
        public static MonsterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonsterManager();
                }
                return instance;
            }
        }

        Random rand = new Random();
        public MonsterLibrary MonsterLibrary { get; private set; } // 몬스터 라이브러리
        public List<Monster>? ActiveMonsters { get; private set; }  // 현재 관리하는 몬스터들
        public int SelActiveMonstersNum { private get; set; }
        public int MonsterCnt { get; private set; }
        /*public MonsterManager(MonsterLibrary library) // 생성은 매니저에서
        {
            monsterLibrary = library;
            activeMonsters = new List<Monster>();
        }*/
  
        public void SetBattleMonsters()

       
        {
            if (Player.Instance.DungeonFloor == 5)  //5스테이지에 보스 몬스터 등장
            {
                ActiveMonsters = new List<Monster>();
                ActiveMonsters.Add(MonsterLibrary.GetBossMonster());
                MonsterCnt = 1;
            }
            else
            {
                MonsterCnt = rand.Next(1, 5);
                ActiveMonsters = MonsterLibrary.GetRandomMonsters(MonsterCnt);
            }
        }        

        public List<Monster>? GetActiveMonsters()
        {            
            return ActiveMonsters;
        }
        
        public List<Monster> GetAllMonsters()
        {
            return MonsterLibrary.GetAllMonsters();
        }
        public Monster? GetSelectedMonster()
        {
            if(ActiveMonsters == null) return new Monster(); // 깡통 반환하면 버그

            return ActiveMonsters[SelActiveMonstersNum];
        }
        public void ClearActiveMonsters()
        {
            if (ActiveMonsters == null) return;
            ActiveMonsters.Clear();
            ActiveMonsters = null;
        }
    }
}
