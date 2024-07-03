using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name="Email Address")]
        [Required(ErrorMessage ="Email Address Is Required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Do Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
