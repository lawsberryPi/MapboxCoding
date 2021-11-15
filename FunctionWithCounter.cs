using System;
using System.Collections.Generic;
using System.Text;

namespace MapboxCoding
{
    class FunctionWithCounter<TResult>
    {
        public readonly Func<TResult> Function;
        public int Counter { get; set; }

        public FunctionWithCounter(Func<TResult> function)
        {
            Counter = 0;
            Function = () =>
            {
                Counter++;

                if(Counter < 2)
                {
                    throw new Exception("First try is failed");
                }

                return function.Invoke();
            };
        }
    }
}
