using System;
using System.Collections.Generic;
using System.Text;

namespace NethereumChain2.Core.Utilities
{
    public static class ContractUtilities
    {
        public static string GetAbiFromFile(string abiFileLocation)
        {
            var contractFile = System.IO.File.ReadAllText(abiFileLocation);
            var abi = contractFile.Substring(contractFile.IndexOf('['));

            return abi;
        }
    }
}
