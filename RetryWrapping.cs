using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxCoding
{
    public class RetryWrapping
    {
        private int Counter { set; get; }
        public RetryWrapping()
        {
            Counter = 0;
        }

        public TResult Retry<T, TResult>( T para ,Func<T, TResult> func)
        {
            try
            {
                var result = func.Invoke(para);
                return result;
            }
            catch(Exception e)
            {
                Counter++;
                Console.WriteLine(e.Message);

                if (Counter > 2)
                {
                    Console.WriteLine(Counter);
                    Counter = 0;   // clear the counter when retry is over
                    throw new Exception("Retry reached Max value of 3 for func with argument");
                }
                else
                {
                    return Retry(para, func);
                }
            }
        }

        public TResult Retry<TResult>(Func<TResult> func)
        {
            try
            {
                Console.WriteLine(Counter);
                var result = func.Invoke();
                Counter = 0; // clear the counter when retry is over
                return result;
            }
            catch(Exception e)
            {
                Counter++;
                Console.WriteLine(e.Message);
                Console.WriteLine($"retry counter for non-argument {Counter}");

                if (Counter > 2)
                {
                    Counter = 0; // clear the counter when retry is over
                    throw new Exception("Retry reached Max value of 3");
                }
                else
                {
                    return Retry(func);
                }
            }
        }
    }
}
