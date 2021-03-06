﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenData.Core
{
    class CompanyOpenDataModel : ICompanyOpenDataModel
    {
        public string shortName { get; set; }
        public string name { get; set; }
        public string edrpou { get; set; }
        public string inn { get; set; }
        public string boss { get; set; }
        public string kved { get; set; }
        public string location { get; set; }
        public string state { get; set; }
        public DateTime? registrationDate { get; set; }
    }
}