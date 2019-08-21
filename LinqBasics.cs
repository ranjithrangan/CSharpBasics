using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharPBasicsConsole
{
    class LinqBasics
    {
        static void Main(string[] args)
        {
            LinqExample.DisplyCubeOfNumbers();
            LinqExample.GetPossibleTennisMatchCombinationOfTwoList();

            Order.DisplayOrderDetailsByDateAndQuantityInDecendingOrder();
            Order.DisplayOrderDetailsGroupByMonthInDecendingOrder();
            Item.DisplayOrderwithPriceDetailsGroupByMonthInDecendingOrderUsingAnonymousTypes();
            Item.DisplayOrderwithPriceDetailsUsingLinqMethods();

            Order.DisplayOrderSpecificDetails();
            Order.DisplayOrderSpecificDetailsUsingLinqQuery();
            LinqExample.DisplyEvenNumbers();
            Order.DisplaySumOfQuantityandMaxItemOrdered();
                        
            Console.ReadLine();
        }
    }


    public class Order
    {
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

        public static void DisplayOrderDetailsByDateAndQuantityInDecendingOrder()
        {
            var objOrderList = GetOrderList();

            var orderedList = from o in objOrderList orderby o.OrderDate descending, o.Quantity descending select new { Id = o.OrderID, o.ItemName, o.OrderDate.Date, o.Quantity };

            Console.Write("\n Order Details By Date And Quantity In Decending Order :");
            Console.Write("\n------------------------------------------------------------------------\n");
            foreach (Object order in orderedList)
            {
                Console.WriteLine(order);
            }



        }
        public static void DisplaySumOfQuantityandMaxItemOrdered()
        {
            var objOrderList = GetOrderList();

            var teamTotalScores = from order in objOrderList
                                  group order by order.ItemName into itemGroup
                                  select new
                                  {
                                      Item = itemGroup.Key,
                                      TotalQuatity = itemGroup.Sum(x => x.Quantity)
                                  };

            Console.Write("\n Item Name and  Total Number of Quantities :");
            Console.Write("\n------------------------------------------------------------------------\n");


            foreach (Object order in teamTotalScores)
            {
                Console.WriteLine(order);
            }

            var ItemThatOrderedMaximum = (from order in objOrderList

                                          select order.ItemName).Max();

            Console.Write("\n------------------------------------------------------------------------\n");

            Console.WriteLine("Maximum Ordered Item :" + ItemThatOrderedMaximum);

        }


        public static void DisplayOrderSpecificDetails()
        {
            var objOrderList = GetOrderList();


            Console.Write("\n Number of items having quantities  greater than zero : " + objOrderList.Count(o => o.Quantity > 0));
            Console.Write("\n Item that ordered in largest quantity in a single order : " + objOrderList.OrderByDescending(o => o.Quantity).FirstOrDefault().ItemName);
            Console.Write("\n Number of  orders placed before jan of this year  : " + objOrderList.Count(o => o.OrderDate.Year < DateTime.Now.Year));
            Console.Write("\n------------------------------------------------------------------------\n");



        }

        public static void DisplayOrderSpecificDetailsUsingLinqQuery()
        {
            var objOrderList = GetOrderList();


            Console.Write("\n Number of items having quantities  greater than zero (output using Query Syntax) : " + (from o in objOrderList where o.Quantity > 0 select o).Count());

            Console.Write("\n Number of  orders placed before jan of this year (output using Query Syntax) )  : " + (from o in objOrderList where o.OrderDate.Year < DateTime.Now.Year select o).Count());
            Console.Write("\n------------------------------------------------------------------------\n");



        }



        public static List<Order> GetOrderList()
        {
            List<Order> objOrderList = new List<Order>();
            objOrderList.Add(new Order { OrderID = 1, ItemName = "Pen", OrderDate = new DateTime(2019, 01, 10), Quantity = 100 });
            objOrderList.Add(new Order { OrderID = 2, ItemName = "Pencil", OrderDate = new DateTime(2019, 01, 20), Quantity = 200 });
            objOrderList.Add(new Order { OrderID = 3, ItemName = "Book", OrderDate = new DateTime(2019, 02, 15), Quantity = 50 });
            objOrderList.Add(new Order { OrderID = 4, ItemName = "Chart", OrderDate = new DateTime(2019, 02, 01), Quantity = 20 });
            objOrderList.Add(new Order { OrderID = 5, ItemName = "Eraser", OrderDate = new DateTime(2019, 05, 25), Quantity = 40 });

            return objOrderList;
        }

        public static void DisplayOrderDetailsGroupByMonthInDecendingOrder()
        {
            var objOrderList = GetOrderList();

            var grouptedList = from o in objOrderList orderby o.OrderDate descending group o by new { o.OrderDate.Month } into g select g;

            Console.Write("\n  Order Details Group By Month In Decending Order :");
            Console.Write("\n------------------------------------------------------------------------\n");

            foreach (var group in grouptedList)
            {
                Console.WriteLine("Month: {0}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month));

                foreach (Order o in group)
                {
                    Console.WriteLine(o.OrderID + " - " + o.ItemName + " - " + o.OrderDate.Date + " - " + o.Quantity);
                }
            }



        }






    }

    public class Item
    {
        public string ItemName { get; set; }
        public int Price { get; set; }

        public static List<Item> GetItemPriceList()
        {
            List<Item> objItemPriceList = new List<Item>();
            objItemPriceList.Add(new Item { ItemName = "Pen", Price = 20 });
            objItemPriceList.Add(new Item { ItemName = "Pencil", Price = 10 });
            objItemPriceList.Add(new Item { ItemName = "Book", Price = 30 });
            objItemPriceList.Add(new Item { ItemName = "Chart", Price = 15 });
            objItemPriceList.Add(new Item { ItemName = "Eraser", Price = 5 });

            return objItemPriceList;
        }

        public static void DisplayOrderwithPriceDetailsGroupByMonthInDecendingOrderUsingAnonymousTypes()
        {

            var objOrderList = Order.GetOrderList();
            var objItemPriceList = Item.GetItemPriceList();

            var grouptedList = from o in objOrderList orderby o.OrderDate descending group o by new { o.OrderDate.Month } into g  select g ;

            Console.Write("\n  Order with Price Details Group By Month In Decending Order Using Anonymous Types :");
            Console.Write("\n------------------------------------------------------------------------\n");


            foreach (var group in grouptedList)
            {

                Console.WriteLine("Month: {0}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key.Month));

                foreach (var o in group)
                {
                    Console.WriteLine(o.OrderID + " - " + o.ItemName + " - " + o.OrderDate + " - " + objItemPriceList.Find(i => i.ItemName == o.ItemName).Price * o.Quantity);
                }
            }



        }

        public static void DisplayOrderwithPriceDetailsUsingLinqMethods()
        {

            var objOrderList = Order.GetOrderList();
            var objItemPriceList = Item.GetItemPriceList();

            var query = objOrderList.GroupBy(order => order.OrderDate.Month)
                  .Select(group =>
                        new
                        {
                            Month = group.Key,
                            Items = group.OrderByDescending(x => x.OrderDate)
                        })
                  .OrderBy(group => group.Items.First().OrderDate);

            Console.Write("\nLINQ : Display Order with Price Details Using Linq Methods");
            Console.Write("\n------------------------------------------------------------------------\n");


            foreach (var group in query)
            {
                Console.WriteLine("Month: {0}", group.Month);
                foreach (var o in group.Items)
                {
                    Console.WriteLine(o.OrderID + " - " + o.ItemName + " - " + o.OrderDate + " - " + objItemPriceList.Find(i => i.ItemName == o.ItemName).Price * o.Quantity);
                }
            }





        }
    }

    public class LinqExample
    {
        public static void DisplyCubeOfNumbers()
        {
            var arr1 = new[] { 3, 9, 2, 8, 6, 5 };

            Console.Write("\nLINQ : Find the number and its Cube of an array which is greater than 100 but less than 1000  : { 3, 9, 2, 8, 6, 5 } ");
            Console.Write("\n------------------------------------------------------------------------\n");

            var cNo = from int Number in arr1
                      let CubeNo = Number * Number * Number
                      where CubeNo > 100 && CubeNo < 1000
                      select new { Number, CubeNo };

            foreach (var num in cNo)
                Console.WriteLine(num);
        }

        public static void GetPossibleTennisMatchCombinationOfTwoList()
        {
            Console.Write("\nLINQ : Possible Tennis Match Combination Of Two List: { Ranjith,Vivek,Arun} and {Monisha,Jeya,Priya}");
            Console.Write("\n------------------------------------------------------------------------\n");
            List<string> groupA = new List<string> { "Ranjith", "Vivek", "Arun" };
            List<string> groupB = new List<string> { "Monisha", "Jeya", "Priya" };
            var allmatchs = (from string c1 in groupA
                             from string c2 in groupB
                             select c1 + c2);

            foreach (var match in allmatchs)
            {
                Console.WriteLine(match);
            }
        }

        public static void DisplyEvenNumbers()
        {
            var Numbers = new[] { 3, 9, 2, 8, 6, 5 };

            Console.Write("\nLINQ : Count and Display even numbers in the array { 3, 9, 2, 8, 6, 5 } ");
            Console.Write("\n------------------------------------------------------------------------\n");

            Console.Write("\n Total number of even Numbers  :" + Numbers.Count(n => n % 2 == 0));
            Console.Write("\n------------------------------------------------------------------------\n");

            Console.Write("\n Even Number List  :");
            Console.Write("\n------------------------------------------------------------------------\n");

            var evenNum = Numbers.Where(n => n % 2 == 0);

            foreach (var num in evenNum)
                Console.WriteLine(num);
        }
    }

}



