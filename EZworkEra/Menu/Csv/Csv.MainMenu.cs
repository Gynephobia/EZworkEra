using System;

namespace EZworkEra
{
    public partial class Csv
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          EZworkEra - Develop utility for EmuEra base game");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                                         CSV 파일 처리 유틸리티입니다.");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("[0]. CSV 변수 목록 추출");             // 구현, 저장은 json으로 대체
            Console.WriteLine("[1]. CSV 변수 목록 추출(SRS 최적화)"); // 구현, 맞게 작동하는 거 맞겠지?
            Console.WriteLine("[2]. CSV 변수 명칭 사전");
            Console.WriteLine("[3]. 이전으로");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("====================================================================================================");
            Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

            string select = Console.ReadLine();

            switch (select)
            {
                case "0":
                    ImportAllCsv();
                    break;

                case "1":
                    CsvSrsFriendly();
                    break;

                case "2":
                    CsvVarDict();
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