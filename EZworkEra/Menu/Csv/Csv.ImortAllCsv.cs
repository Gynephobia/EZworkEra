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
                    Import(CsvFunction.ImportMode.All);
                    break;

                case "1":
                    Import(CsvFunction.ImportMode.NoChara);
                    break;

                case "2":
                    Import(CsvFunction.ImportMode.Revert);
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

            void Import(CsvFunction.ImportMode mode)
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

                        select = Console.ReadLine();

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

                        select = Console.ReadLine();


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

                        select = Console.ReadLine();

                        string fileName;

                        switch (select)
                        {
                            case "0":
                                InsertFileName();
                                CsvFunction.SaveDictionary(Program.MainDictionary, fileName);
                                break;

                            case "1":
                                MainMenu();
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
}
