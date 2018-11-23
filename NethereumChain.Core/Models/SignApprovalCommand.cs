using System.ComponentModel.DataAnnotations;

namespace NethereumChain2.Core.Models
{
    public class SignApprovalCommand
    {
        [Required]
        public int Gas { get; set; }
        [Required]
        public int Value { get; set; }
    }
}