using System;

namespace EZworkEra
{
    public partial class Menu
    {
        public static void MainMenu()
        {
            //구현 끝은 파이썬 EZworkEra의 기능 구현이 끝난 것을 의미함
            Console.Clear();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          EZworkEra - Develop utility for EmuEra base game");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("작업 후 버튼을 눌러 프로그램을 종료하셔야 작업파일이 손실되지 않습니다.");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("[0]. CSV 파일 처리");
            Console.WriteLine("[1]. ERB 파일 처리");
            Console.WriteLine("[2]. ERH 파일 처리 (미구현)"); // 구현
            Console.WriteLine("[3]. 결과물 제어");
            Console.WriteLine("[4]. 프로그램 정보");          // 구현
            Console.WriteLine("[5]. 프로그램 종료");          // 구현
            Console.WriteLine("====================================================================================================");
            Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

            string select = Console.ReadLine();

            switch (select)
            {
                case "0":
                    Csv.MainMenu();
                    break;

                case "1":
                    Erb.MainMenu();
                    break;

                case "2":
                    Erh.MainMenu();
                    break;

                case "3":
                    Result.MainMenu();
                    break;

                case "4":
                    ProgramInfo.MainMenu();
                    break;

                case "5":
                    Quit();
                    break;

                default:
                    InvalidInput(MainMenu);
                    break;
            }
        }
    }
}
