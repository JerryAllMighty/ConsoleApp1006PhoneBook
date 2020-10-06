using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    [Serializable]
    public class PhoneInfo
    {
        string name;        //필수
        string phonenumber;     //필수
        string birth;


        public string Name { get { return name; } }
        public string PhoneNumber { get { return phonenumber; } }
        public string Birth { get { return birth; } }


        public PhoneInfo()
        {

        }

        public PhoneInfo(string name, string phonenumber)
        {
            this.phonenumber = phonenumber;
            this.name = name;
            //this.birth = ""; 
        }

        public PhoneInfo(string name, string phonenumber, string birth)
        {
            this.phonenumber = phonenumber;
            this.name = name;
            this.birth = birth;
        }



        public virtual void ShowPhoneInfo()
        {
            Console.WriteLine();
            Console.WriteLine("이름은 : " + this.name);
            Console.WriteLine("전화번호는 : " + this.phonenumber);
            if (birth != null)
                Console.WriteLine("생일은 : " + this.birth);

        }


    }
    [Serializable]
    public class PhoneUnivInfo : PhoneInfo
    {
        string major;
        string year;
        public string Major { get { return major; } }
        public string Year { get { return year; } }

        public PhoneUnivInfo(string name, string phonenumber, string major, string year) : base(name, phonenumber)
        {
            this.major = major;
            this.year = year;

        }

        public PhoneUnivInfo(string name, string phonenumber, string birth, string major, string year) : base(name, phonenumber, birth)
        {
            this.major = major;
            this.year = year;

        }



        //오버라이드
        public override void ShowPhoneInfo()
        {
            base.ShowPhoneInfo();
            Console.WriteLine($"전공은 {major}이고, 학번은 {year}입니다.");

        }

    }

    [Serializable]
    public class PhoneCompanyInfo : PhoneInfo
    {
        string department;
        string company;

        public PhoneCompanyInfo(string name, string phonenumber, string department, string company) : base(name, phonenumber)
        {
            this.department = department;
            this.company = company;
        }

        public PhoneCompanyInfo(string name, string phonenumber, string birth, string department, string company) : base(name, phonenumber, birth)
        {
            this.department = department;
            this.company = company;
        }

        public override void ShowPhoneInfo()
        {
            base.ShowPhoneInfo();
            Console.WriteLine($"부서는 {this.department}이고, 회사는 {this.company}입니다.");
        }
    }


}
