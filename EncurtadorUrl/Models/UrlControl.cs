using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EncurtadorUrl.Models
{
    [Table("UrlControl")]
    public class UrlControl
    {
        public int Id { get; set; }

        public int Hits { get; set; }

        public string Url { get; set; }
    }
}