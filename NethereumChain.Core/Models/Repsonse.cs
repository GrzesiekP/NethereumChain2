using Nethereum.ABI.FunctionEncoding.Attributes;

namespace NethereumChain2.Core.Models
{
    [FunctionOutput]
    public class Response
    {
        [Parameter("uint", 1)]
        public int LocationId { get; set; }
        [Parameter("uint", 2)]
        public int PreviousLocationId { get; set; }
        [Parameter("string", 3)]
        public string LocationName { get; set; }
        [Parameter("string", 4)]
        public string PreviousLocationName { get; set; }
        [Parameter("uint", 5)]
        public long Timestamp { get; set; }

        [Parameter("bool", 6)]
        public bool AgencyQ1Aproval { get; set; }
        [Parameter("bool", 7)]
        public bool AgencyE1Aproval { get; set; }
        [Parameter("bool", 8)]
        public bool AgencyQ2Aproval { get; set; }
        [Parameter("bool", 9)]
        public bool AgencyI2Aproval { get; set; }
    }
}
