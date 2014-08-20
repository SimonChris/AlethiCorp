using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
  public class Report
  {
    public int Id { get; set; }

    [ScaffoldColumn(false)]
    public string UserName { get; set; }

    [ScaffoldColumn(false)]
    public string Name { get; set; }

    public int Day { get; set; }

    public bool Flagged { get; set; }

    [ScaffoldColumn(false)]
    public bool Read { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}