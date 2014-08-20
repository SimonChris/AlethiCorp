using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
    public class NewsItem
    {
      public int Id { get; set; }

      public string UserName { get; set; }

      public string Name { get; set; }
    }
}