﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetStore.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}