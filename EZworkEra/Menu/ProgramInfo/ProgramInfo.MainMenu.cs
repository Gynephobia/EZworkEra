using System;

namespace EZworkEra
{
    public partial class ProgramInfo
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          EZworkEra - Develop utility for EmuEra base game");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                                            EZworkEra 정보");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("[0]. 버전명");
            Console.WriteLine("[1]. 오류보고 관련");
            Console.WriteLine("[2]. 유의사항");
            Console.WriteLine("[3]. 이전으로");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("====================================================================================================");
            Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");
            string select = Console.ReadLine();

            switch (select)
            {
                case "0":
                    Console.WriteLine("버전명: 3.3");
                    Console.WriteLine("아무키나 누르시면 이전 메뉴로 돌아갑니다.");

                    Console.ReadKey();

                    MainMenu();

                    break;

                case "1":
                    Console.WriteLine("https://github.com/SecretU4/EZworkEra//issues 으로 연락주세요.");
                    Console.WriteLine("아무키나 누르시면 이전 메뉴로 돌아갑니다.");

                    Console.ReadKey();

                    MainMenu();

                    break;

                case "2":
                    Console.WriteLine("");
                    Console.WriteLine("            아직 완성된 프로그램이 아닙니다. 사용 시 문제가 발생하면 오류를 보고해주세요.");
                    Console.WriteLine("            여러분의 도움으로 더 나은 프로그램을 만들어 노가다를 줄입니다.");
                    Console.WriteLine("            현재 윈도우 환경만 지원합니다. 어차피 원본 엔진도 윈도우용이잖아요.");
                    Console.WriteLine("");
                    Console.WriteLine("아무키나 누르시면 이전 메뉴로 돌아갑니다.");

                    Console.ReadKey();

                    MainMenu();

                    break;


                case "3":
                    Menu.MainMenu();
                    break;

                default:
                    Menu.InvalidInput(MainMenu);
                    break;
            }
        }
    }
}
