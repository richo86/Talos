﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class GeneroDTO
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
    }
}
