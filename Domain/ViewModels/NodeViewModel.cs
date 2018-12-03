using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data;
using Domain.Validators;

namespace Domain.ViewModels
{
    public class NodeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Алиас"), MaxLength(10), MinLength(2)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Допускаются только латинские символы и цифры")]
        public string Alias { get; set; }
    }
}