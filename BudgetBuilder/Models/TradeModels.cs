using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuilder.Models
{
    public enum TradeName
    {
        // Category Types
        Foundation = 0,
        Framing = 1,
        Exterior = 3,
        Interior = 4,
        MajorSystems = 5,
        Details = 6,
        Other = 7
    }

    public class Trade
    {
        [Key]
        public int TradeID { get; set; }

        public TradeName Category { get; set; }     
        public string SubCategory { get; set; }

        public double MaterialCost { get; set; }
        public double LaborCost { get; set; }
        public double TradeBudget { get; set; }

        // Foreign key of BuildingModel
        public int BuildingID { get; set; }

        [ForeignKey("BuildingID")]
        public virtual Building Building { get; set; }
    }

    public class TradesUpdateModel
    {
        public string ApplicationUserID { get; set; }
        public int BuildingID { get; set; }

        public ICollection<Trade> Trades { get; set; }
    }

    public class TradeListRequestModel
    {
        public int BuildingID { get; set; }
    }
}
