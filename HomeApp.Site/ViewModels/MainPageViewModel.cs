using HomeApp.Core.Models;

namespace HomeApp.Site.ViewModels
{
    public class MainPageViewModel
    {
        public RealEstatesModel RealEstatesTop4Sale { get; set; }
        public RealEstatesModel RealEstatesTop4Rent { get; set; }
        public RealEstatesModel RealEstatesTopLocation { get; set; }
        public UsersWithRealEstateCountModel AgentTops { get; set; }
        public long AllActiveRealEstateCount { get; set; }
    }
}
