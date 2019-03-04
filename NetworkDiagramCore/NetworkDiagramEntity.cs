
namespace NetworkDiagramCore
{
    public class NetworkDiagramEntity
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int Duration { get; set; }
        public int EarliestStart { get; set; }
        public int EarliestFinish { get; set; }
        public int LatestStart { get; set; }
        public int LatestFinish { get; set; }
        public int TotalFloat { get => LatestStart - EarliestStart; }
    }
}
