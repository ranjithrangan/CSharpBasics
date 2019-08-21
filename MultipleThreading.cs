using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharPBasicsConsole
{
    public class ATM
    {
        static void Main(string[] args)
        {
            Account objAccount = new Account();
            objAccount.CustomerName = "Ranjith&Rangan";
            objAccount.AccountNumber = "0001115689016";
            objAccount.Address = "Chennai";
            objAccount.Balance = 1000;

            //display current balance before any transaction
            objAccount.DisplayCurrentBalance();

            //Ranjith depositing 500 rupess
            Thread deposit = new Thread(new ThreadStart(objAccount.Deposit500Rupees));
            deposit.Start();

            //Rangan withdrawing 200 rupess
            Thread withdraw = new Thread(new ThreadStart(objAccount.Withdraw200Rupees));
            withdraw.Start();

            Console.ReadLine();
        }
    }

    public class Account
    {
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }

        static object locker = new object();

        public void DisplayCurrentBalance()
        {

            Console.WriteLine("****Account Summary****");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Name         : " + CustomerName);
            Console.WriteLine("AccountNumber: " + AccountNumber);
            Console.WriteLine("Address      : " + Address);
            Console.WriteLine("Balance      : " + Balance.ToString());
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("\n");
        }
        public void Deposit500Rupees()
        {
            Thread.Sleep(800);
            Monitor.Enter(locker);
            try
            {
                Balance = Balance + 500;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Monitor.Exit(locker);
                Console.WriteLine("Ranjith Deposited 500 Rupess");
                Console.WriteLine("\n");
                DisplayCurrentBalance();
            }

        }
        public void Withdraw200Rupees()
        {
            Thread.Sleep(800);
            Monitor.Enter(locker);
            try
            {
                Balance = Balance - 200;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Rangan Withdrawn 200 Rupess");
                Console.WriteLine("\n");
                DisplayCurrentBalance();
            }
        }

    }


}
