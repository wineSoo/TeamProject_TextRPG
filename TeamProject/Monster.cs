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
       
    }
}
