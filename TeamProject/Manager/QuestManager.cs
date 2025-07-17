using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class QuestManager
    {
        private static QuestManager? instance;
        private QuestManager()
        {

        }
        public static QuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestManager();
                }
                return instance;
            }
        }

        //public Dictionary<Monster.MonsterType, int> killCounts = new();

        




    }
}
