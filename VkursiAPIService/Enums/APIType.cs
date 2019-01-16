using System.ComponentModel.DataAnnotations;

namespace VkursiAPI.Enums
{
    /// <summary>
    /// Enum for choose between Vkursi's API
    /// </summary>
    public enum APIType
    {
        [Display(Name = "organizations")]
        GetOrganizationInfo = 1,
        [Display(Name = "monitoring/add")]
        AddToMOnitoring = 2,
        [Display(Name = "monitoring/delete")]
        DeleteFromMOnitoring = 3,
        [Display(Name = "changes")]
        GetChanges = 4
    }
}
