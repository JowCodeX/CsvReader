namespace CsvReader
{
    internal class Program
    {
        private static List<CarOrders> carOrders = new List<CarOrders>();

        public class CarOrders
        {
            public string CompanyId { get; set; }
            public string Region { get; set; }
            public string Car { get; set; }

        }

        static void Main(string[] args)
        {
            var csvPath = @"C:\Users\Jooow\Downloads\car_orders.csv";

            string[] csvlines = System.IO.File.ReadAllLines(csvPath);

            for (int i = 1; i < csvlines.Length; i++)
            {
                string[] rowData = csvlines[i].Split(';');

                var order = new CarOrders
                {
                    CompanyId = rowData[0],
                    Region = rowData[1],
                    Car = rowData[2],
                };

                carOrders.Add(order);
            }

             void OrderCountForCars()
            {
                var orderCountByCar = new SortedDictionary<string, int>();

                foreach (var order in carOrders)
                {
                    if (orderCountByCar.ContainsKey(order.Car))
                    {
                        orderCountByCar[order.Car]++;
                    }
                    else
                    {
                        orderCountByCar[order.Car] = 1;
                    }
                }

                foreach (var kvp in orderCountByCar)
                {
                    Console.WriteLine($"Car: {kvp.Key}, Order Count: {kvp.Value}");
                }
            }

            void MostPopularCarBySeller()
            {
                var mostOccurringCarById = new SortedDictionary<string, string>();

                foreach (var order in carOrders)
                {

                    if (mostOccurringCarById.ContainsKey(order.CompanyId))
                    {

                        if (CountOccurrences(order.Car, order.CompanyId) > CountOccurrences(mostOccurringCarById[order.CompanyId], order.Car))
                        {
                            mostOccurringCarById[order.CompanyId] = order.Car;
                        }
                    }
                    else
                    {
                        mostOccurringCarById[order.CompanyId] = order.Car;
                    }
                }

                foreach (var kvp in mostOccurringCarById)
                {
                    Console.WriteLine($"Car: {kvp.Key}, Order Count: {kvp.Value}");
                }

                int CountOccurrences(string car, string id)
                {
                    return carOrders.Count(order => order.CompanyId == id && order.Car == car);
                }

            }

           void OrderCountForId()
            {
                var orderCountById = new SortedDictionary<string, int>();

                foreach (var order in carOrders)
                {
                    if (orderCountById.ContainsKey(order.CompanyId))
                    {
                        orderCountById[order.CompanyId]++;
                    }
                    else
                    {
                        orderCountById[order.CompanyId] = 1;
                    }
                }

                foreach (var kvp in orderCountById)
                {
                    Console.WriteLine($"ID: {kvp.Key}, Order Count: {kvp.Value}");
                }

            }

            Console.WriteLine("The order count per company: ");
            OrderCountForId();

            Console.WriteLine();
            Console.WriteLine("The order count per car brand: ");
            OrderCountForCars();

            Console.WriteLine();
            Console.WriteLine("Order count for the most popular cars per sellers: ");
            MostPopularCarBySeller();

        }

    }
}
