using System;
using System.Collections.Generic;

namespace TeamProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // 몬스터 리스트 생성
            List<Monster> monsters = new List<Monster>
            {
                new Monster("미니언", 2, 15, 5, 1, "작은 악당"),
                new Monster("대표미니언", 5, 25, 8, 2, "우두머리 미니언"),
                new Monster("공허충", 3, 10, 3, 1, "빠르고 약한 몬스터")
            };

            Console.WriteLine("=== 전투 전 몬스터 상태 ===");
            foreach (var m in monsters)
            {
                m.PrintStatusColor();
            }

            // 데미지 입힘 (몬스터 2마리 사망 처리)
            monsters[0].DamageTaken(15); // 미니언 죽음
            monsters[2].DamageTaken(20); // 공허충 죽음

            Console.WriteLine();
            Console.WriteLine("=== 데미지 후 몬스터 상태 ===");
            foreach (var m in monsters)
            {
                m.PrintStatusColor();
            }

            Console.WriteLine();
            Console.WriteLine("테스트 완료. 아무 키나 누르면 종료됩니다.");
            Console.ReadKey();
        }
    }
}
