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

        private bool IsCharaOrName(string file)
        {
            return file.ToLower().Contains("chara") || file.ToLower().Contains("name");
        }

        private bool IsVariableOrReplace(string file)
        {
            return file.ToLower().Contains("variable") || file.ToLower().Contains("replace");
        }

        public Dictionary<string, Dictionary<string, string>> ImportAllCsv(string path, SearchOption searchOption, Encoding encoding, ImportMode mode)
        {
            List<string> files = GetPaths(path, searchOption);

            switch (mode)
            {
                case ImportMode.All:
                    break;

                case ImportMode.NoChara:
                    files.RemoveAll(IsCharaOrName);
                    break;

                case ImportMode.Revert: // 대체 어디에 쓰는 기능?
                    files.RemoveAll(IsCharaOrName);
                    break;

                case ImportMode.OnlyMyName: // 좀 더 쉽게 하는 방법 없을까?
                    string[] filesCopy = new string[files.Count];
                    files.CopyTo(filesCopy);
                    files.RemoveAll(IsCharaOrName);
                    files = filesCopy.Except(files).ToList();
                    break;

                case ImportMode.OnlyMyVariable:
                    files.RemoveAll(IsCharaOrName);
                    files.RemoveAll(IsVariableOrReplace);
                    break;
            }

            Console.WriteLine($"{files.Count} 개의 파일이 발견되었습니다.");

            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();

            int errorCount = 0;

            foreach (string file in files)
            {
                try
                {
                    result.Add(file, GetDictionary(file, encoding, mode));
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

        public Dictionary<string, string> GetDictionary(string path, Encoding encoding, ImportMode mode) // 이름을 뭘로 해야 될까
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(); // 이름을 뭘로 해야 될까

            foreach (string line in File.ReadAllLines(path, encoding))
            {
                if (line.Trim().Length > 0 && line.Trim().Substring(0, 1) != ";")
                {
                    if (mode == ImportMode.Revert)
                    {
                        dictionary.Add(line.Split(',')[1], line.Split(',')[0]);
                    }

                    else if (mode == ImportMode.OnlyMyName)
                    {
                        if(path.ToLower().Contains("chara"))
                        {
                            if (line.Split(',')[0] == "CALLNAME" || line.Split(',')[0] == "呼び名")
                            {
                                dictionary.Add(line.Split(',')[0], line.Split(',')[1]);
                            }
                        }
                        
                        else
                        {
                            dictionary.Add(line.Split(',')[0], line.Split(',')[1]);
                        }
                    }

                    else
                    {
                        dictionary.Add(line.Split(',')[0], line.Split(',')[1]);
                    }
                }
            }

            return dictionary;
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

        public static void Import(ImportMode mode)
        {
            Console.WriteLine("추출을 시작합니다.");
            Console.WriteLine("CSV 파일이 위치하는 폴더명을 입력해주세요. :");

            string path = Console.ReadLine();

            // 대상 폴더 존재 확인
            if (Directory.Exists(path))
            {
                SearchOption searchOption = new SearchOption();

                SelectSearchOption();

                Encoding encoding;

                SelectEncoding();

                Program.MainDictionary = new CsvFunction().ImportAllCsv(path, searchOption, encoding, mode);

                Console.WriteLine("추출이 완료되었습니다.");

                SelectSave();

                void SelectSearchOption()
                {
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("                                        하위폴더까지 포함해 진행하시겠습니까?");
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("[0]. 예");     // 구현
                    Console.WriteLine("[1]. 아니오"); // 구현
                    Console.WriteLine("====================================================================================================");
                    Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

                    string select = Console.ReadLine();

                    switch (select)
                    {
                        case "0":
                            searchOption = SearchOption.AllDirectories;
                            break;

                        case "1":
                            searchOption = SearchOption.TopDirectoryOnly;
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            SelectSearchOption();
                            break;
                    }
                }

                void SelectEncoding()
                {
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("                                         대상 파일의 인코딩을 선택하세요.");
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("[0]. UTF-8");              // 구현
                    Console.WriteLine("[1]. UTF-8 with BOM");     // 구현, UTF-8과 동일, 나눠야 하는 의미가 없음
                    Console.WriteLine("[2]. SHIFT-JIS");          // 미구현, 932로 퉁치면 되는데 필요할까?, C#에선 932가 shift-jis로 나와서 뭔지 찾기도 힘들다
                    Console.WriteLine("[3]. 일본어 확장(cp932)"); // 구현
                    Console.WriteLine("[4]. EUC-KR");             // 구현, 949로 퉁치면 되는데 필요할까?
                    Console.WriteLine("[5]. 한국어 확장(cp949)"); // 구현
                    Console.WriteLine("====================================================================================================");
                    Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

                    string select = Console.ReadLine();

                    switch (select)
                    {
                        case "0":
                            encoding = Encoding.UTF8;
                            break;

                        case "1":
                            encoding = Encoding.UTF8;
                            break;

                        case "3":
                            encoding = Encoding.GetEncoding(932);
                            break;

                        case "4":
                            encoding = Encoding.GetEncoding(51949); // 이거 맞겠지?
                            break;

                        case "5":
                            encoding = Encoding.GetEncoding(949);
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            SelectEncoding();
                            break;
                    }
                }

                void SelectSave()
                {
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("                                     출력된 데이터를 외부 파일에 저장하시겠습니까?");
                    Console.WriteLine("====================================================================================================");
                    Console.WriteLine("[0]. 예");     // 구현, 파이썬 버전은 pickle 쓰는 거 같아서 json으로 대체, 파이썬 버전 파일과 호환 안됨
                    Console.WriteLine("[1]. 아니오"); // 구현
                    Console.WriteLine("====================================================================================================");
                    Console.Write("번호를 입력하세요. 클릭은 지원하지 않습니다. :");

                    string select = Console.ReadLine();

                    string fileName;

                    switch (select)
                    {
                        case "0":
                            InsertFileName();
                            SaveDictionary(Program.MainDictionary, fileName);
                            break;

                        case "1":
                            Menu.MainMenu();
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            SelectSave();
                            break;
                    }

                    void InsertFileName()
                    {
                        Console.WriteLine("저장할 외부 파일의 이름을 입력해주세요.");

                        fileName = Console.ReadLine().TrimStart(); // 앞에 있는 공백 제거

                        if (fileName.Length == 0 || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) // 공백만으로 이루어진 파일명과 사용할 수 없는 문자가 들어간 파일명 거르기
                        {
                            Console.WriteLine("사용할 수 없는 파일명입니다.");

                            InsertFileName();
                        }

                        else
                        {
                            if (File.Exists($"{Directory.GetCurrentDirectory()}\\sav\\{fileName}.json")) // 파일명 중복 체크, 중복될 경우 (2)부터 숫자가 올라감
                            {
                                int i = 2;

                                string alteredFileName = $"{fileName} (2)";

                                while (File.Exists($"{Directory.GetCurrentDirectory()}\\sav\\{fileName} ({i}).json"))
                                {
                                    i++;

                                    alteredFileName = $"{fileName} ({i})";
                                }

                                fileName = alteredFileName;
                            }
                        }
                    }
                }
            }

            // 대상 폴더가 없을 경우
            else
            {
                Console.WriteLine("폴더를 찾을 수 없습니다.");

                Import(mode);
            }
        }
    }
}
