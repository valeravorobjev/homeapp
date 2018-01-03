namespace HomeApp.Core.ViewModels
{
    public class MainPage
    {
        public RealEstateList RealEstatesTop4Sale { get; set; }
        public RealEstateList RealEstatesTop4Rent { get; set; }
        public RealEstateList RealEstatesTopLocation { get; set; }
        public UserWithRealEstateCountList AgentTops { get; set; }
        public long AllActiveRealEstateCount { get; set; }
    }
}
