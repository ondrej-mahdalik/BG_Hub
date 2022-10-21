namespace BrokenGrenade.Data
{
    public class Mission
    {
        public Guid MissionId { get; set; }
        public string Name { get; set; }
        public DateTime? LeaderBriefing { get; set; }
        public DateTime? ConnectingToServer { get; set; }
        public DateTime Start { get; set; }
        public string SlottingSheetUrl { get; set; }

        public User? Author { get; set; }
        public Guid? AuthorId { get; set; }

        public MissionCategory? Category { get; set; }
        public Guid? CategoryId { get; set; }

        public Mission(string name, string slottingSheetUrl, DateTime start, DateTime? leaderBriefing = null, DateTime? connectingToServer = null)
        {
            Name = name;
            SlottingSheetUrl = slottingSheetUrl;
            Start = start;
            LeaderBriefing = leaderBriefing;
            ConnectingToServer = connectingToServer;
        }
    }
}
