﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class SearchInEventModel
    {
        public Epilogger.Data.Tweet Tweet { get; set; }
        public Epilogger.Data.Image Image { get; set; }
    }
}