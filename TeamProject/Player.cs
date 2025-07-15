using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject
{
    internal class Player
    {

        private static Player? instance;

       
        //속성을 초기화 합니다. 나중에 직업을 구현하면 직업에 따른 초기 능력치를 조절할 수 있을 것 같습니다.
        private Player()
        {
            Lv = 1;
            Name = "이름 없는"; //기본값
            Job = PlayerJob.warrior;
            AtkPower = 10;
            DefPower = 5;
            Hp = 100;
            Gold = 1500;

        }


        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        
        }

        public enum PlayerJob 
        {
            warrior
        }
        
        
        //플레이어 속성. 필요하면 추가해서 쓰세용
        public int Lv { get; set; }
        public string Name { get; set; }
        public PlayerJob Job { get; set; }
        public float AtkPower { get; set; }
        public float DefPower { get; set; }
        public float Hp { get; set; }
        public int Gold { get; set; }


    }
}
