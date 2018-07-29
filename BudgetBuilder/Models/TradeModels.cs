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

    public class TradeModels
    {
        // Table properties
        public int TradeModelsID { get; set; }

        public TradeName Category { get; set; }     
        public string SubCategory { get; set; }

        public double MaterialCost { get; set; }
        public double LaborCost { get; set; }
        public double TradeBudget { get; set; }

        // Foreign key of BuildingModel
        public int BuildingModelsID { get; set; }

        [ForeignKey("BuildingModelsID")]
        public virtual BuildingModels Building { get; set; }
    }
}
