using bankingApplication_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace bankingApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpPut]
        [ProducesResponseType(201)]
        public CreatedResult appendToTransactionsHistory([FromForm] Transaction transaction)
        {
            string transactionHistoryFile = "history.csv";
            string newLine = $"{transaction.Date};{transaction.Amount};{transaction.Currency};" +
                             $"{transaction.InitiatorAccountNumber};{transaction.ReceiverBankCode};" +
                             $"{transaction.ReceiverBankCountry};{transaction.ContractorAccountNumber};" +
                             $"{transaction.BeneficiaryData};{transaction.InitiatorReferences};" +
                             $"{transaction.BeneficiaryCountry};{transaction.ChargesAccount};" +
                             $"{transaction.ChargesInstructions};{transaction.TransactionDetails}\n";

            System.IO.File.AppendAllText(transactionHistoryFile, newLine);
            var filePath = Path.GetFullPath(transactionHistoryFile);
            return Created(filePath, "Transaction history updated successfully.");

        }
    }
}
