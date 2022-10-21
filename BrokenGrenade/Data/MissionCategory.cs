namespace BrokenGrenade.Data
{
    public class MissionCategory
    {
        public Guid MissionCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NoteInMission { get; set; }

        public IList<Mission> Missions { get; set; } = new List<Mission>();
    }
}
