using System;
using System.IO;
using System.Text;

namespace EZworkEra
{
    public partial class Csv
    {
        static void ImportAllCsv()
        {
            Console.Clear();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          EZworkEra - Develop utility for EmuEra base game");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                                        추출할 CSV의 종류를 선택하세요.");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("[0]. 모두");
            Console.WriteLine("[1]. CHARA 제외");
            Console.WriteLine("[2]. 문자/숫자 변환");
            Console.WriteLine("[3]. 이전으로");
            Console.WriteLine("[4]. 처음으로");
            Console.WriteLine("");
            Console.WriteLine("====================================================================================================");
            Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

            string select = Console.ReadLine();

            switch (select)
            {
                case "0":
                    CsvFunction.Import(CsvFunction.ImportMode.All);
                    break;

                case "1":
                    CsvFunction.Import(CsvFunction.ImportMode.NoChara);
                    break;

                case "2":
                    CsvFunction.Import(CsvFunction.ImportMode.Revert);
                    break;

                case "3":
                    MainMenu();
                    break;

                case "4":
                    Menu.MainMenu();
                    break;

                default:
                    Menu.InvalidInput(ImportAllCsv);
                    break;
            }
        }
    }
}
