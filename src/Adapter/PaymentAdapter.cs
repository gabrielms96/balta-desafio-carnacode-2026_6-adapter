using DesignPatternAdapter.Adaptee;
using DesignPatternAdapter.Client;
using DesignPatternAdapter.Client.Enums;
using DesignPatternAdapter.Target;

namespace DesignPatternAdapter.Adapter
{
    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacyPaymentSystem;

        public PaymentAdapter(LegacyPaymentSystem legacyPaymentSystem)
        {
            _legacyPaymentSystem = legacyPaymentSystem;
        }

        public EPaymentStatus CheckStatus(string transactionId)
        {
            var legacyStatus = _legacyPaymentSystem.QueryTransactionStatus(transactionId);
            var modernStatus = legacyStatus.ToUpper() switch
            {
                "PENDING" => EPaymentStatus.Pending,
                "APPROVED" => EPaymentStatus.Approved,
                "DECLINED" => EPaymentStatus.Declined,
                "REFUNDED" => EPaymentStatus.Refunded,
                _ => throw new InvalidOperationException("Unknown status from legacy system")
            };
            return modernStatus;
        }

        public PaymentResult ProcessPayment(PaymentRequest request)
        {

            int expMonth = request.ExpirationDate.Month;
            int expYear = request.ExpirationDate.Year;
            var legacyResponse = _legacyPaymentSystem.AuthorizeTransaction(
            request.CreditCardNumber,
            Convert.ToInt16(request.Cvv),
            expMonth,
            expYear,
            (double)request.Amount * 100,
            request.CustomerEmail);
            PaymentResult paymentResult = new PaymentResult
            {
                Success = legacyResponse.ResponseCode == "00",
                TransactionId = legacyResponse.TransactionRef,
                Message = legacyResponse.ResponseMessage
            };

            return paymentResult;
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            return _legacyPaymentSystem.ReverseTransaction(transactionId, (double)amount * 100);
        }
    }
}
