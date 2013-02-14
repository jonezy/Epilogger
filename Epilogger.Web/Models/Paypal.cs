using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epilogger.Web.Models
{
    public class Paypal
    {
        public Paypal()
        {
        }

        public string formUrl { get; set; }
        public string cmd { get; set; }
        public string business { get; set; }   
        public string no_shipping { get; set; }
        public string @return { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public string item_name { get; set; }
        public string amount { get; set; }
        public string item_number { get; set; }
        public string quantity { get; set; }
        public string tax { get; set; }
        public string shipping { get; set; }
        public string custom { get; set; }

    }
}