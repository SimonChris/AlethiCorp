using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
  public class Playthrough
  {
    public int Id { get; set; }

    public string UserName { get; set; }

    public string PlayerName { get; set; }

    public string Ending { get; set; }

    public HackingProgression HackingProgression { get; set; }

    public string FavoriteColor { get; set; }

    public string BearType { get; set; }

    public DateTime TimeStamp { get; set; }

    public string EkstraInfo { get; set; }
  }
}