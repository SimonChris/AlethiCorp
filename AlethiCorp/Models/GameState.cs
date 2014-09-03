using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlethiCorp.Models
{
  public enum GameProgression
  {
    Ongoing,
    Arrested,
    Promoted,
    Comply,
    Bear,
    BearBearBear
    //Finalized
  }

  public enum HackingProgression
  {
    Innocent,
    Infiltrator,
    Concurrency
  }

  public class GameState
  {
    [Key]
    public string UserName { get; set; } 

    public bool Employee { get; set; }

    public int Day { get; set; }

    public HackingProgression HackingProgression { get; set; }

    public GameProgression GameProgression { get; set; }
  }
}