using System;

namespace EZworkEra
{
    public partial class Menu
    {
        public static void Quit()
        {
            Console.WriteLine("이용해주셔서 감사합니다.");
            Console.WriteLine("아무키나 누르시면 종료됩니다.");

            Console.ReadKey();
        }

        public static void InvalidInput(Action menu)
        {
            Console.WriteLine("유효하지 않은 입력입니다.");
            Console.WriteLine("아무키나 누르시면 이전 메뉴로 돌아갑니다.");

            Console.ReadKey();

            menu();
        }
    }
}
