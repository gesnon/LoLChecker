using System;
using System.Collections.Generic;
using System.Text;

namespace LoLChecker.Logic.Models
{
    public class WinRatioAgainstChampion
    {
        public float Winratio { get; set; }
        public int AmountGames { get; set; }
        public int AmountWins { get; set; }

        public override string ToString()
        {
            return $"Winratio: {Winratio}; AmountGames: {AmountGames}; AmountWins: {AmountWins}";
        }
    }
}
