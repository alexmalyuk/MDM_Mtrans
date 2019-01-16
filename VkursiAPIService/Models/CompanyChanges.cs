using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkursiAPI.Models
{
    /// <summary>
    /// Class in which response from server would be mapped
    /// </summary>
    public class CompanyChanges
    {
        public string edrpou { get; set; }
        public string field { get; set; }
        public string previousValue { get; set; }
        public string currentValue { get; set; }
        public string date { get; set; }
    }
}
