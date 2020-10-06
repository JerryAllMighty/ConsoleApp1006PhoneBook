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
            PhoneBookManager manager = PhoneBookManager.CreateInstance();
            manager.ReadSerial();
            
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
