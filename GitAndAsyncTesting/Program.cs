
using System.Diagnostics;
using System.Linq.Expressions;

namespace GitAndAsyncTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch myTimer = new Stopwatch();

            myTimer.Start();
            await RunSyncOperation(); // delayed to 1500 ms 
            myTimer.Stop();
            Console.WriteLine($"Elapsed time: {myTimer.ElapsedMilliseconds}");

            myTimer.Reset();

            myTimer.Start();
            await RunAsyncOperation(); // async has 8 * 500 ms, 4000 ms total, but also excecuted in 1500 ms
            myTimer.Stop();
            Console.WriteLine($"Elapsed time: {myTimer.ElapsedMilliseconds}");

        }

      
        private static async Task RunSyncOperation()
        {
            Console.WriteLine("Sync operation starting");
            await Task.Delay(1500);
            Console.WriteLine("Sync operation ending");
            
        }

        private static async Task RunAsyncOperation()
        {
            Console.WriteLine("Async operation starting");

            //Unordered wild excecution - 500 ms total
            var BananaSplitTask =  MakeBananaSplit(1);
            var BananaSplitTask2 = MakeBananaSplit(2);
            var BananaSplitTask3 = MakeBananaSplit(3);
            var BananaSplitTask4 = MakeBananaSplit(4);


            //Ordered utlizing concurrency - 500 ms total
            var ChocolateSundaeTask1 =  MakeChocolateSundae(1);
            var ChocolateSundaeTask2 =  MakeChocolateSundae(2);

            string sundae1 = await ChocolateSundaeTask1;
            string sundae2 = await ChocolateSundaeTask2;

            Console.WriteLine(sundae1);
            Console.WriteLine(sundae2);

            //Ordered not utlizing concurrency - 1000 ms total

            var sundae3 = await MakeChocolateSundae(3);
            var sundae4 = await MakeChocolateSundae(4);

            Console.WriteLine(sundae3);
            Console.WriteLine(sundae4);

            Console.WriteLine("Async operation ending");
          
        }

        private static async Task<string> MakeChocolateSundae(int number)
        {
            await Task.Delay(500);
            return $"     made chocolate sundae {number}"; 
        }

        private static async Task MakeBananaSplit(int number)
        {
            await Task.Delay(500);
            Console.WriteLine($"     Made bananasplit {number}");
           
        }
    }
}
