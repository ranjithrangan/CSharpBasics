using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CSharpBasicsConsole
{
    class Program
    {
        delegate List<int> del(List<int> numlist);

        static void Main(string[] args)
        {
            //Print Two Dimensional Array
            TwoDimensionalArray objTwoDimensionalArray = new TwoDimensionalArray();
            objTwoDimensionalArray.Print2DArray();


            //Mutiple inheritence
            Console.WriteLine("****Multiple inheritance using interface****");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");
            Calculator objCalculator = new Calculator();
            objCalculator.Add();
            objCalculator.Multiply();


            //Method Overriding
            Console.WriteLine("****Method Overriding using virtual Keyword****");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");

            Shape objShape = new Shape();
            Shape objSquare = new Square();
            objShape.Draw();
            objSquare.Draw();

            //Extension Method for email
            Console.WriteLine("****Validate email using extension methods****");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("\n");
            string emailtoValidate = "Usre@MYApp.com";
            Console.WriteLine("Is Valid Email : " + emailtoValidate.IsEmail());
            Console.WriteLine("\n");

            //Delegates and lamda expressions

            List<int> numberlist = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Math objMath = new Math();
            del numlist = objMath.GetNumberList;
            List<int> divisibleByThreeList = numlist(numberlist);

            Console.WriteLine("****Print divisible By Three  Nuber List using Delegates and Lamda expressions****");
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("Number List : 1, 2, 3, 4, 5, 6, 7, 8, 9, 10");
            Console.WriteLine("\n");
            Console.WriteLine("Result:");
            Console.WriteLine("\n");

            foreach (var num in divisibleByThreeList)
            {
                Console.WriteLine(num.ToString() + "\n");
            }

            Console.WriteLine("GITHUB Commit");

            Console.ReadKey();
        }
    }


    //Delegates and lamda expressions
    class Math
    {
        public List<int> GetNumberList(List<int> numList)
        {
            return numList.FindAll(x => x % 3 == 0);

        }
    }


    //Method Overriding 

    public class Shape
    {
        public virtual void Draw()
        {
            Console.WriteLine("This is the Virtual Method of from base class Shape");
            Console.WriteLine("\n\n");
        }
    }

    public class Square : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("This is the overriden method from child class Square");
            Console.WriteLine("\n\n");
        }
    }

    //Multiple inheritance

    public interface IAddition
    {
        void Add();
    }
    public interface IMultiplication
    {
        void Multiply();
    }
    public class Calculator : IAddition, IMultiplication
    {
        public void Add()
        {
            Console.WriteLine("Invoked Add method using multiple inheritance");
            Console.WriteLine("\n\n");
        }
        public void Multiply()
        {
            Console.WriteLine("Invoked Multiply method using multiple inheritance");
            Console.WriteLine("\n\n");
        }
    }

    //Email  Extension method
    public static class StringExtensions
    {
        public static bool IsEmail(this string email)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;

        }

    }


    //Two Dimensional Array
    class TwoDimensionalArray
    {

        public void Print2DArray()
        {
            int i, j;
            int[,] arr1 = new int[3, 3];

            Console.WriteLine("\n\n ****Read a 2D array of size 3x3 and print the matrix :*****\n");
            Console.WriteLine("-------------------------------------------------------------");
                        
            Console.Write("Input elements in the matrix :\n");
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.WriteLine("element - [{0},{1}] : ", i, j);
                    arr1[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("\nThe matrix is : \n");
            for (i = 0; i < 3; i++)
            {
                Console.WriteLine("\n");
                for (j = 0; j < 3; j++)
                    Console.WriteLine("{0}\t", arr1[i, j]);
            }
            Console.WriteLine("\n\n");

        }

    }
}


