using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumAPI.Models.Filters
{
    public class NewBlocksParameter
    {
        [Required]
        public string FilterId { get; set; }
    }
}
