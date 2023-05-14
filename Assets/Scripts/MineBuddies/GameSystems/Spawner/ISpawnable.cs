namespace ZeroPrep.MineBuddies
{
    public interface ISpawnable
    {
        public int PointValue { get; set; }
        public float Probability { get; set; }
    }
}