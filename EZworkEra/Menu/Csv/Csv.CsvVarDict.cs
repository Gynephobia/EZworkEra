using System;

namespace EZworkEra
{
    partial class Csv
    {
        static void CsvVarDict()
        {
            Console.WriteLine("CSV 변수 대응 딕셔너리를 제작합니다.");

            CsvFunction.Import(CsvFunction.ImportMode.VarDict);
        }
    }
}
