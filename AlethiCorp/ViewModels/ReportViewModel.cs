using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.ViewModels
{
  public enum ReportType
  {
    Report,
    EMail,
    Phone,
    Surveillance
  }

  public class ReportViewModel
  {
    public int Id { get; set; }

    [ScaffoldColumn(false)]
    public string Name { get; set; }

    [ScaffoldColumn(false)]
    [JsonConverter(typeof(StringEnumConverter))]
    public ReportType Type { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string Date { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Information { get; set; }

    public bool Flagged { get; set; }

    [ScaffoldColumn(false)]
    public bool Read { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}