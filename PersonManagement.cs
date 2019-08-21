using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharpBasicsConsole
{
    class PersonManagement
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[5];
            persons[0] = new Student("Ranjith", 50.5);
            persons[1] = new Student("Tamil", 90.6);
            persons[2] = new Student("Vasanth", 95.3);
            persons[3] = new Professor("Prakash", 10);
            persons[4] = new Professor("Hari", 2);


            Console.WriteLine("****Outstanding Professors and Students Information****");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");

            foreach (var person in persons)
            {
                if (person.isOutstanding())
                {
                    if (person is Student)
                    {
                        Student objStudent = (Student)person;
                        objStudent.Dispaly();
                    }
                    else
                    {
                        Professor objProfessor = (Professor)person;
                        objProfessor.Print();
                    }
                }

            }
            Console.ReadLine();
        }
    }

    public class Person
    {
        public string Name = string.Empty;

        public Person(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public string SetName(string name)
        {
            Name = name;
            return Name;
        }

        public virtual bool isOutstanding()
        {
            return false;
        }


    }

    public class Professor : Person
    {
        private int _booksPublished { get; set; }

        public Professor(string name, int booksPublished) : base(name)
        {
            _booksPublished = booksPublished;
        }

        public void Print()
        {

            Console.WriteLine(" Professor Name  : " + Name);
            Console.WriteLine("Books Published : " + _booksPublished);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");
        }
        public override bool isOutstanding()
        {
            if (_booksPublished > 4)
            {
                return true;
            }
            return false;
        }


    }

    public class Student : Person
    {
        private double _percentage { get; set; }

        public Student(string name, double percentage) : base(name)
        {
            _percentage = percentage;
        }

        public void Dispaly()
        {
            Console.WriteLine("Student Name  : " + Name);
            Console.WriteLine("\n");
            Console.WriteLine("Percentage : " + _percentage);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");
        }

        public override bool isOutstanding()
        {
            if (_percentage > 85)
            {
                return true;
            }
            return false;
        }


    }


}
