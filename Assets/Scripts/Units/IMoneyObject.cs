namespace Units
{
    public interface IMoneyObject
    {
        public int GetBuyValue();
        public int GetSellValue();
        
        public string name { get; }

    }
}
