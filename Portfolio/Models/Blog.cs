﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Models
{
    public class Blog
    {
        public Blog()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        [Required]
        [AllowHtml]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        [AllowHtml]
        public string Caption { get; set; }
        public string Tag { get; set; }
        public bool Published { get; set; }

        [EmailAddress]
        public string Subscribe { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}