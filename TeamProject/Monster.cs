using System;

namespace TeamProject
{
    /* 구현해야될 기능들
        1. 몬스터 기본 구조
        2. 체력 변화 정보 및 전투 관련 기능들
            - 현재 남은 체력 표시
            - 체력이 0되었을때 hp -> dead , 회색처리
     */

    public class Monster
    {
        public string Name;
        public int Level;
        public int MaxHp;
        public int Hp;
        public int Atk;
        public int Def;
        public string Description;
        public bool isDie; //죽었는지 여부 확인 (true = 죽음 / false = 생존)
        Random rand = new Random();

        public Monster() { Name = ""; Description = ""; }  //몬스터매니저 생성자

        public Monster(string name, int level, int maxhp, int atk, int def, string description)
        {
            Name = name;
            Level = level;
            MaxHp = maxhp;
            Hp = maxhp;
            Atk = atk;
            Def = def;
            Description = description;
            isDie = false;
            //previousHp = maxhp;
        }
        

        
        //몬스터 hp가 0이하로 내려갈때
        public int DamageTaken(int playerAtk, out bool isHit, out bool isCritical)
        {
            int tmpDam = 0;
            int check = rand.Next(10);
            isCritical = false;

            // 10% 확률로 공격 실패(0~3, 5~9)
            //if (check <= 5) isHit = false; // 테스트용
            if (check == 6) isHit = false; // 공격 실패 시
            else // 공격 성공 시
            {
                isHit = true;

                int tmpAtk = rand.Next((int)(playerAtk - playerAtk * 0.1f),
                    (int)(playerAtk * 0.1f >= 0.5f ? (int)(playerAtk + playerAtk * 0.1f + 1) : (int)(playerAtk + playerAtk * 0.1f)));

                // 치명타 계산
                check = rand.Next(0, 100);
                if (check <= 54)
                {
                    isCritical = true;
                    tmpAtk = (int)(tmpAtk * 1.6f); // 160% 데미지
                }

                tmpDam = tmpAtk - Def;

                if (tmpDam < 0) tmpDam = 0; // 데미지는 0 밑으로 떨어짐x

                Hp -= tmpDam;
                if (Hp <= 0)
                {
                    Hp = 0;
                    isDie = true;
                }
            }
            return tmpDam;
        }
       /* public bool IsDead
        {
            get
            {
                if (Hp <= 0) //0보다 작거나 같은 경우
                {
                    return true; // 체력이 0 이하이면 죽음
                }
                else
                {
                    return false; // 체력이 남아 있으면 살아있는 상태
                }
            }
        }

        // 몬스터 기본 상태 정보 반환
        public string GetStatus()
        {
            if (IsDead)
                return $"Lv.{Level} {Name} Dead"; //Lv.2 코스믹 슬라임 Dead
            else
                return $"Lv.{Level} {Name} HP {Hp}"; //Lv.2 코스믹 슬라임 HP 12
        }

        //몬스터 죽은후 회색처리
        public void PrintStatus()
        {
            // 만약 몬스터가 죽었으면 회색, 아니면 빨간색으로 콘솔 글씨색을 바꾼다.
            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            // 몬스터의 현재 상태(체력 or Dead)를 출력한다.
            Console.WriteLine(GetStatus());

            // 콘솔 글씨색을 원래대로 돌려놓는다.
            Console.ResetColor();
        }*/
    }


}

