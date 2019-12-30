using System.Collections.Generic;

namespace EZworkEra
{
    class Program
    {
        public static Dictionary<string, Dictionary<string, string>> MainDictionary;

        // 나중에 쓸지도 모름
        /*
        private struct PathAndValues // 변경 예정
        {
            string path;
            List<Value> values;      // 변경 예정
        }

        private struct Value         // 변경 예정
        {
            string number;           // 변경 예정
            string value;            // 변경 예정
        }
        */

        static void Main(string[] args)
        {
            MainDictionary = new Dictionary<string, Dictionary<string, string>>();

            Menu.MainMenu();
        }
    }
}
