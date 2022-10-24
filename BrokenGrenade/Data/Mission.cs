using System.ComponentModel.DataAnnotations;

namespace BrokenGrenade.Data
{
    public class Mission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LeaderBriefing { get; set; }
        public DateTime ConnectingToServer { get; set; }
        public DateTime Start { get; set; }
        public string SlottingSheetUrl { get; set; }
        public string? ModpackUrl { get; set; }

        public User? Author { get; set; }
        public string? AuthorId { get; set; }

        public MissionCategory? Category { get; set; }
        public Guid? CategoryId { get; set; }
        
        public ModpackType? ModpackType { get; set; }
        public Guid? ModpackTypeId { get; set; }

        public Mission(string name, string slottingSheetUrl, DateTime start, DateTime connectingToServer, DateTime? leaderBriefing = null, string? modpackUrl = null)
        {
            Name = name;
            SlottingSheetUrl = slottingSheetUrl;
            Start = start;
            LeaderBriefing = leaderBriefing;
            ConnectingToServer = connectingToServer;
            ModpackUrl = modpackUrl;
        }

        public void CopyFrom(Mission mission)
        {
            Name = mission.Name;
            LeaderBriefing = mission.LeaderBriefing;
            ConnectingToServer = mission.ConnectingToServer;
            Start = mission.Start;
            SlottingSheetUrl = mission.SlottingSheetUrl;
            ModpackUrl = mission.ModpackUrl;
            // AuthorId = mission.AuthorId;
            CategoryId = mission.CategoryId;
            ModpackTypeId = mission.ModpackTypeId;
        }
    }
}
