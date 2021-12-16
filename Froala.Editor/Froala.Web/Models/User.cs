using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Froala.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        [Required]
        [AllowHtml]
        public string Description { set; get; }
    }
}