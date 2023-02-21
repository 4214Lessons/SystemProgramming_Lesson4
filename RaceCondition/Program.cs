Thread[] threads = new Thread[5];

var sync = new object();





for (int i = 0; i < 5; i++)
{
    threads[i] = new Thread(() =>
    {
        for (int i = 0; i < 1000000; i++)
        {
            #region

            lock (sync)
            {
                Counter.Count1++;

                if (Counter.Count1 % 2 == 0)
                    Counter.Count2++;
            }

            #endregion



            // #region
            // var lockTaken = false;

            // try
            // {
            //     Monitor.Enter(sync, ref lockTaken);
            //     if (lockTaken)
            //     {
            //         Counter.Count1++;

            //         if (Counter.Count1 % 2 == 0)
            //             Counter.Count2++;
            //     }
            // }
            // finally
            // {
            //     if (lockTaken)
            //     {
            //         Monitor.Exit(sync);
            //     }
            // }


            // if (lockTaken)
            // {
            //     // doSomething
            // }
            // #endregion







            {
                // Interlocked.Increment(ref Counter.Count1);
                // 
                // if (Counter.Count1 % 2 == 0)
                //     Interlocked.Increment(ref Counter.Count2);
            }



            #region problem
            // // critical section start
            // Counter.Count1++; // shared memory
            // // critical section end
            #endregion problem
        }
    });
}




for (int i = 0; i < 5; i++) threads[i].Start();
for (int i = 0; i < 5; i++) threads[i].Join();



Console.WriteLine($"Count 1: {Counter.Count1}");
Console.WriteLine($"Count 2: {Counter.Count2}");



class Counter
{
    public static int Count1 = 0;
    public static int Count2 = 0;
}