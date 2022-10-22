namespace BrokenGrenade.Data
{
    public class MissionCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }

        public IList<Mission> Missions { get; set; } = new List<Mission>();

        public MissionCategory(string name, string? note = null)
        {
            Name = name;
            Note = note;
        }
    }
}
