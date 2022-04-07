namespace Units
{
    public interface ISource
    {
        public bool CanAccessCamo { get; set; }
        public void AddToKills(int kill);
    }
}