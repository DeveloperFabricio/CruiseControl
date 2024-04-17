using System.ComponentModel.DataAnnotations;

namespace CruiseControl.Core.Enums
{
    public enum CarCategoryType
    {
        [Display(Name = "Economic")]
        Economic,
        [Display(Name = "Intermediate")]
        Intermediate,
        [Display(Name = "Luxury")]
        Luxury,
        [Display(Name = "SUV")]
        SUV,
        [Display(Name = "Van")]
        Van,
        [Display(Name = "Truck")]
        Truck
    }
}
