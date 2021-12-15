using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Froala.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }
        public string FullNameResx
        { get { return Froala.Web.Message.FullName; } }

        [Required]
        [AllowHtml]
        public string Description { set; get; }

        public string DescriptionResx
        { get { return Froala.Web.Message.Description; } }
    }
}