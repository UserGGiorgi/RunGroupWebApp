using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace RunGroupWebApp.ViewModels
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public int? AddressId { get; set; }
        public Address  Address { get; set; }
        public string? Url { get; set; }

    }
}
