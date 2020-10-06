using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    public class PhoneBookManager
    {
        public override string ToString()
        {
            string str = $@"취미는 {this.Hobby}이고, 좋아하는 음식은 {this.Food}입니다.";
            return str;
        }

        public string Hobby { get; set; }
        public string Food { get; set; }

        #region 생성자
        public PhoneBookManager()
        {

        }

        public PhoneBookManager(string hobby, string food)
        {
            this.Hobby = hobby;
            this.Food = food;
        }
        #endregion
          
        HashSet<PhoneInfo> infoStorage = new HashSet<PhoneInfo>();
        string fileName = "jerry.dat";


        public void ReadSerial()
        {
            if (File.Exists(fileName))
            {
                return;
            }
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                BinaryFormatter serializer = new BinaryFormatter();
                infoStorage = (HashSet<PhoneInfo>)serializer.Deserialize(fs);

                foreach (PhoneInfo info in infoStorage)
                {
                    if (info == null)
                    {
                        break;
                    }
                    Console.WriteLine(info.Name);
                }
                fs.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
        }

        public void SaveSerial()
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, infoStorage);

                fs.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }


        }

        public void BirthCheck(string name, string phone, string birth)
        {
            if (birth.Length < 1)
                infoStorage.Add(new PhoneInfo(name, phone));

            else
                infoStorage.Add(new PhoneInfo(name, phone, birth));
        }

        public void BirthCheck(string name, string phone, string birth, string major, string year)
        {
            if (birth.Length < 1)
                infoStorage.Add((PhoneInfo)new PhoneUnivInfo(name, phone, major, year));

            else
                infoStorage.Add((PhoneInfo)new PhoneUnivInfo(name, phone, birth, major, year));
        }

        public void BirthCheck2 (string name, string phone, string birth, string department, string company)
        {
            if (birth.Length < 1)
                infoStorage.Add((PhoneInfo)new PhoneCompanyInfo(name, phone, department, company));

            else
                infoStorage.Add((PhoneInfo)new PhoneCompanyInfo(name, phone, birth, department, company));
        }

        public void ShowMenu()
        {
            Console.WriteLine("------------------------ 주소록 --------------------------");
            Console.WriteLine("1. 입력  |  2. 목록  |  3. 검색  |  4. 삭제  |  5. 종료");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("선택: ");
        }

        public void InputData()
        {
            Console.WriteLine("1.일반     2.대학    3.회사");
            Console.Write("선택 >> ");
            int choice = int.Parse(Console.ReadLine().Trim());

            //필수항목들 무조건 실행

            Console.Write("이름: ");
            string name = Console.ReadLine().Trim();
            //if (name == "") or if (name.Length < 1) or if (name.Equals(""))
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("이름은 필수입력입니다");
                return;
            }
            else
            {
                PhoneInfo gagchae = SearchName(name);
                if (gagchae != null)
                {
                    Console.WriteLine(" 이미 있는 이름입니다.");
                    return;
                }
            }

            Console.Write("전화번호: ");
            string phone = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(phone))
            {
                Console.WriteLine("전화번호는 필수입력입니다");
                return;
            }



            Console.Write("생일: ");
            string birth = Console.ReadLine().Trim();



            if (choice == 1)
            {
                BirthCheck(name, phone, birth);
                foreach (PhoneInfo str in infoStorage)
                {
                    Console.WriteLine($"입력되었습니다. 이름은 {str.Name}, 전화번호는 {str.PhoneNumber}, 생일은{str.Birth}입니다");
                }
            }



            if (choice == 2)
            {
                Console.Write("전공을 입력하세요: ");
                string major = Console.ReadLine().Trim();

                if (major.Equals(""))
                {
                    Console.WriteLine("전공은 필수입력입니다");
                    return;
                }

                Console.Write("학번을 입력하세요 : ");
                string year = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(year))      //말도 안되는 학번 걸러내기
                {
                    Console.WriteLine("학번은 필수입력입니다.");
                    return;
                }

                BirthCheck (name, phone, birth, major, year);
            }

            else if (choice == 3)
            {
                Console.Write("부서를 입력하세요: ");
                string department = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(department))
                {
                    Console.WriteLine("부서는 필수입력입니다");
                    return;
                }

                Console.Write("회사를 입력하세요: ");
                string company = Console.ReadLine().Trim();

                if (company.Equals(""))
                {
                    Console.WriteLine("회사는 필수입력입니다");
                    return;
                }
                BirthCheck2 (name, phone, birth, department, company);
            }
        }


        public void ListData()
        {
            if (infoStorage.Count == 0)
            {
                Console.WriteLine("입력된 데이터가 없습니다.");
                return;
            }

            foreach (PhoneInfo curInfo in infoStorage)
            {
                curInfo.ShowPhoneInfo();
            }
        }

        public void SearchData()
        {
            Console.WriteLine("주소록 검색을 시작합니다......");
            PhoneInfo ereum = SearchName();
            if (ereum == null)
            {
                Console.WriteLine("검색된 데이터가 없습니다");
            }
            else
            {
                ereum.ShowPhoneInfo();
            }

            #region 모두 찾기
            //int findCnt = 0;
            //for(int i=0; i<curCnt; i++)
            //{
            //    // ==, Equals(), CompareTo()
            //    if (infoStorage[i].Name.Replace(" ","").CompareTo(name) == 0)
            //    {
            //        infoStorage[i].ShowPhoneInfo();
            //        findCnt++;
            //    }
            //}
            //if (findCnt < 1)
            //{
            //    Console.WriteLine("검색된 데이터가 없습니다");
            //}
            //else
            //{
            //    Console.WriteLine($"총 {findCnt} 명이 검색되었습니다.");
            //}
            #endregion
        }

        private PhoneInfo SearchName()
        {
            Console.Write("이름: ");
            string name = Console.ReadLine().Trim().Replace(" ", "");

            foreach (PhoneInfo curInfo in infoStorage)
            { 
            if(curInfo.Name == name)
                {
                    return curInfo;
                }
            }

            return null;
        }

        private PhoneInfo SearchName(string name)
        {
            foreach (PhoneInfo curInfo in infoStorage)
            {
                if (curInfo.Name == name)
                {
                    return curInfo;
                }
            }
            return null;

        }

        public void DeleteData()
        {
            Console.WriteLine("주소록 삭제를 시작합니다......");

            PhoneInfo gagchae = SearchName();
            if (gagchae == null)
            {
                Console.WriteLine("삭제할 데이터가 없습니다");
            }
            else
            {
                infoStorage.Remove(gagchae);
                //for (int i = dataIdx; i < infoStorage.Count; i++)
                //{
                //    infoStorage[i] = infoStorage[i + 1];
                //}
                // infoStorage.Count--;
                Console.WriteLine("주소록 삭제가 완료되었습니다");
            }
        }
    }
}
