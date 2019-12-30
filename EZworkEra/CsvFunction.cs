using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace EZworkEra
{
    partial class CsvFunction
    {
        private List<string> GetPaths(string path, SearchOption searchOption)
        {
            List<string> files = Directory.GetFiles(path, "*.csv", searchOption).ToList();

            return files;
        }

        private bool IsChara(string file)
        {
            return file.ToLower().Contains("chara");
        }


        public Dictionary<string, Dictionary<string, string>> ImportAllCsv(string path, SearchOption searchOption, Encoding encoding, ImportMode mode)
        {
            List<string> files = GetPaths(path, searchOption);

            switch (mode)
            {
                case ImportMode.All:
                    break;

                case ImportMode.NoChara:
                    files.RemoveAll(IsChara);
                    break;

                case ImportMode.Revert: // 대체 어디에 쓰는 기능?
                    files.RemoveAll(IsChara);
                    break;
            }

            Console.WriteLine($"{files.Count} 개의 파일이 발견되었습니다.");

            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            int errorCount = 0;

            foreach (string file in files)
            {
                try
                {
                    result.Add(file, GetDictionary2(file, encoding, mode));
                }

                catch (Exception e)
                {
                    errorCount++;

                    Console.WriteLine($"오류!");
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine($"총 {files.Count} 개의 파일 중 {files.Count - errorCount} 개가 처리되었습니다.");

            return result;
        }

        public Dictionary<string, string> GetDictionary2(string path, Encoding encoding, ImportMode mode) // 이름을 뭘로 해야 될까
        {
            Dictionary<string, string> dict2 = new Dictionary<string, string>(); // 이름을 뭘로 해야 될까

            foreach (string line in File.ReadAllLines(path, encoding))
            {
                if (line.Trim().Length > 0 && line.Trim().Substring(0, 1) != ";")
                {
                    if (mode == ImportMode.Revert)
                    {
                        dict2.Add(line.Split(',')[1], line.Split(',')[0]);
                    }

                    else
                    {
                        dict2.Add(line.Split(',')[0], line.Split(',')[1]);
                    }
                }
            }

            return dict2;
        }

        public Dictionary<string, Dictionary<string, string>> GetDictionary1(string path, Dictionary<string, string> dict2) // 이름을 뭘로 해야 될까
        {
            Dictionary<string, Dictionary<string, string>> dict1 = new Dictionary<string, Dictionary<string, string>>(); // 이름을 뭘로 해야 될까

            dict1.Add(path, dict2);

            return dict1;
        }

        public static void SaveDictionary(Dictionary<string, Dictionary<string, string>> dictionary, string fileName)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Dictionary<string, Dictionary<string, string>>));


            try
            {
                if (!Directory.Exists("sav")) // sav 없으면 생성
                {
                    Directory.CreateDirectory("sav");
                }

                using (FileStream fileStream = new FileStream($"{Directory.GetCurrentDirectory()}\\sav\\{fileName}.json", FileMode.Create))
                {
                    using (XmlDictionaryWriter xmlDictionaryWriter = JsonReaderWriterFactory.CreateJsonWriter(fileStream, Encoding.UTF8, true, true))
                    {
                        serializer.WriteObject(xmlDictionaryWriter, dictionary);
                    }
                }

                Console.WriteLine($"{Directory.GetCurrentDirectory()}\\sav\\{fileName}.json으로 저장되었습니다.");
                Console.WriteLine("아무키나 누르시면 처음 메뉴로 돌아갑니다.");

                Console.ReadKey();

                Menu.MainMenu();
            }

            catch (Exception e)
            {
                Console.WriteLine("오류가 발생하여 저장을 실패하였습니다.");
                Console.WriteLine(e);
                Console.WriteLine("아무키나 누르시면 처음 메뉴로 돌아갑니다.");

                Console.ReadKey();

                Menu.MainMenu();
            }
        }
    }
}
