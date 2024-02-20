namespace MSK.Core.Models
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        public int VotedCount { get; set; }
        public string About { get; set; }
        public string Party { get; set; }
        public double VotedPercent { get; set; }
        public string ImageUrl { get; set; }
        public List<Vote> Votes { get; set; }
        public int? ElectionId { get; set; }
        public Election? Election { get; set; }



    }
}
