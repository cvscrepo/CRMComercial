﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.Utility
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Value { get; set; }
    }
}
