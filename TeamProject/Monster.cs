using System.Security.Cryptography.X509Certificates;

namespace TeamProject
{
    public class Monster
    {
        public string Name;
        public int Level;
        public int MaxHp;
        public int Hp;
        public int Atk;
        public int Def;
        public string Description;


        public Monster(string name, int level, int maxhp, int atk, int def, string description)
        {
            Name = name;
            Level = level;
            MaxHp = maxhp;
            Hp = maxhp;
            Atk = atk;
            Def = def;
            Description = description;
        }

        public void DamageTaken(int damage)
        {
            Hp -= damage;
            if (Hp < 0)
                Hp = 0;
        }

       //몬스터 죽은후 상태(dead) + 나중에 회색처리 예정
        public string Status()
        {
            return Hp <= 0 ? $"{Name} (Dead)" : $"{Name} (HP: {Hp})";
        }


        List<Monster> monsters = new List<Monster>();

        void CreateMonsters()
        {
            monsters.Add(new Monster("코스믹 슬라임", 2, 15, 6, 1, "우주에 있는 가장 평범한 몬스터."));
            monsters.Add(new Monster("네뷸라 기생충", 5, 25, 10, 3, "성운(네뷸라) 구름 속을 유영하는 거대 기생충."));
            monsters.Add(new Monster("궁허충", 3, 10, 7, 0, "빠르고 공격적인 벌레."));
            monsters.Add(new Monster("외계인 병사", 1, 8, 4, 1, "우주 말단 병사."));
        }

        //오류가 어디서 나는걸까..
    }
}
