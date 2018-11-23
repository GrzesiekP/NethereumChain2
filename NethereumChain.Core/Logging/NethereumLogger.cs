using System;

namespace NethereumChain2.Core.Logging
{
    class NethereumLogger : INethereumLogger
    {
        public void Error(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
