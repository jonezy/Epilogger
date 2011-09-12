﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Epilogger.Web.Models
{
    public class AllPhotosDisplayViewModel
    {
        public string Name { get; set; }
        public int PhotoCount { get; set; }
        public int Page { get; set; }

        public IEnumerable<Epilogger.Data.Image> Images { get; set;}
        
    }
}