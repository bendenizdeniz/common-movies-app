﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entity.Entity
{
    [Table("SearchedMovie")]
    public partial class SearchedMovie
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
    }
}