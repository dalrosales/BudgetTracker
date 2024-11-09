namespace WalletWatch.Models
{
    public class Income
    {
        public int IncomeId { get; set; }
        public float GrossIncome { get; set; }
        public float FederalTax { get; set; }
        public float StateTax { get; set; }
        public float FICA { get; set; }
        public float MedFICA { get; set; }
        public float HealthInsPremium { get; set; }
    }
}
