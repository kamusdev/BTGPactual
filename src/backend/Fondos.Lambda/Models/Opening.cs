﻿using Fondos.Lambda.Enums;
using Fondos.Lambda.Models.Interfaces;
using System;

namespace Fondos.Lambda.Models
{
    public class Opening : IOpening
    {
        public int ClientId { get; set; }
        public OperationType OperationType { get; set; }
        public int FundId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}