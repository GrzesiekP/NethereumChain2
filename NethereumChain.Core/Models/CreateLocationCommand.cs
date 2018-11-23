﻿using System.ComponentModel.DataAnnotations;

namespace NethereumChain2.Core.Models
{
    public class CreateLocationCommand
    {
        [Required]
        [StringLength(50)]
        public string LocationName { get; set; }
        [Required]
        public string UserAddress { get; set; }
        [Required]
        public string UserPrivateKey { get; set; }
        [Required]
        public int Gas { get; set; }
        [Required]
        public int Value { get; set; }
    }
}