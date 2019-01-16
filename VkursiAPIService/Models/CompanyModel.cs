using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VkursiAPI.Models
{
    /// <summary>
    /// Class in which response from server would be mapped
    /// </summary>
    public class CompanyModel
    {
        public bool ShouldSerializeDateRegisterInn()
        {
            return DateRegisterInn.HasValue;
        }
        public bool ShouldSerializeDateAddedToMonitoring()
        {
            return DateAddedToMonitoring.HasValue;
        }
        public string name { get; set; }
        public string shortName { get; set; }
        public string edrpou { get; set; }
        public string location { get; set; }
        public string state { get; set; }
        public string boss { get; set; }
        public string kved { get; set; }
        public string inn { get; set; }

        [XmlElement(ElementName = "dateRegisterInn")]
        public DateTime? DateRegisterInn { get; set; }

        [XmlElement(ElementName = "dateAddedToMonitoring")]
        public DateTime? DateAddedToMonitoring { get; set; }
    }
}
