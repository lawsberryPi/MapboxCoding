using System;

namespace MapboxCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var retryWrapper = new RetryWrapping();

            // test case 1: Retry a function over three times

            retryWrapper.Retry(() =>
           {
               throw new Exception("error");
               return 1;
           });


            // test case 2: retry a function once
            var callFunc = new FunctionWithCounter<int>
                (
                    () => { return 1; }
                );

            var value = retryWrapper.Retry(callFunc.Function);
            Console.WriteLine($"return value should be the first successful invocation {value}");



            // test case 3: retry a function with argument over 3 times
            try
            {
                retryWrapper.Retry(1,
                                (x) =>
                                    {
                                        throw new Exception("it fails everytime");
                                        return 1;
                                    }
                                );
            }
            catch(Exception e)
            {
                Console.WriteLine($"after three times this exception is thrown {e.Message}");
            }

        }
    }
}
