using Data;
using Data.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [DataContract(Namespace = Const.DataContractNameSpace)]
    public class BaseModel
    {
        [IgnoreDataMember]
        public string NodeAlias { get; set; }
        [DataMember]
        public string NativeId { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Name { get; set; }


        public BaseModel(Subject subject)
        {
            this.subject = subject;
        }

        public virtual void Validate()
        {
        }

        internal bool isValid = false;
        internal string validationResult;
        internal Subject subject;

        [IgnoreDataMember]
        public bool IsValid 
            => isValid;

        [IgnoreDataMember]
        public string ValidationResult
            => validationResult;

        [IgnoreDataMember]
        public Subject Subject
            => subject;
    }
}
