using System;

namespace EZworkEra
{
    partial class Csv
    {
        static void CsvSrsFriendly()
        {
            Console.Clear();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          EZworkEra - Develop utility for EmuEra base game");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                                        추출 방법을 선택하세요.");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("[0]. CSV 내 이름만");
            Console.WriteLine("[1]. CSV 내 변수만 (CHARA 제외)");
            Console.WriteLine("[2]. 이전으로");
            Console.WriteLine("[3]. 처음으로");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("====================================================================================================");
            Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

            string select = Console.ReadLine();

            switch (select)
            {
                case "0":
                    CsvFunction.Import(CsvFunction.ImportMode.OnlyMyName);
                    break;

                case "1":
                    CsvFunction.Import(CsvFunction.ImportMode.OnlyMyVariable);
                    break;

                case "2":
                    MainMenu();
                    break;

                case "3":
                    Menu.MainMenu();
                    break;

                default:
                    Menu.InvalidInput(ImportAllCsv);
                    break;
            }
        }
    }
}
