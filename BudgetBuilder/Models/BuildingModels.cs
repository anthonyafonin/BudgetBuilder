using BudgetBuilder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBuilder.Models
{
    public class BuildingModels
    {
        // Table properties
        [Key]
        public int BuildingModelsID { get; set; }
        public string Title { get; set; }
        public double Budget { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

        // Foreign Key of AspNetUsers
        public string ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser User { get; set; }

        // Collection of Child TradeModels
        public ICollection<TradeModels> Trade { get; set; }
    }

    public class BuildingRequestModel
    {
        public string UserID { get; set; }
        public int? BuildingModelsID { get; set; }
    }
}
