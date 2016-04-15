using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        [EmailAddress]
        public string SubEmail { get; set; }
    }
}