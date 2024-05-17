namespace bankingApplication_API.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string InitiatorAccountNumber { get; set; }
        public int ReceiverBankCode { get; set; }
        public string ReceiverBankCountry { get; set; }
        public int ContractorAccountNumber { get; set; }
        public string BeneficiaryData { get; set; }
        public string InitiatorReferences { get; set; }
        public string BeneficiaryCountry { get; set; }
        public string ChargesAccount { get; set; }
        public int ChargesInstructions { get; set; }
        public string TransactionDetails { get; set; }
    }
}
