﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
    public partial class Friend
    {
        [Key]
        public int Id { get; set; }
        public int ToUserId { get; set; }
        public int FromUserId { get; set; }
    }
}