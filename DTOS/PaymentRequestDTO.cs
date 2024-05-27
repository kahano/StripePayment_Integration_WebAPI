﻿using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.DTOS
{
    public class PaymentRequestDTO {

        [Required]
        [Range(50, 1000000)]
        public long amount {  get; set; }


        [Required]
        public string Currency { get; set; }


        [Required]
        public string source { get; set; } = string.Empty;



        };
    
}
