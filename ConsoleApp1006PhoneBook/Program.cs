using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {

            //ToSTring()를 오버라이드 해서 폰 매니저해서 사용해보기
            PhoneBookManager manager = new PhoneBookManager("오버워치", "감자튀김");
            PhoneBookManager manager2 = new PhoneBookManager("유튜브", "피자");
            manager.ReadSerial();
            Console.WriteLine(manager.ToString());
            Console.WriteLine(manager2.ToString());

            while (true)
            {

                manager.ShowMenu();
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: manager.InputData(); break;
                    case 2: manager.ListData(); break;
                    case 3: manager.SearchData(); break;
                    case 4: manager.DeleteData(); break;
                    case 5:
                        Console.WriteLine("프로그램을 종료합니다."); manager.SaveSerial();
                        return;

                }
            }
        }
    }
}
