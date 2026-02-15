using DesignPatternAdapter.Client;
using DesignPatternAdapter.Client.Enums;


namespace DesignPatternAdapter.Target
{
    public interface IPaymentProcessor
    {
        PaymentResult ProcessPayment(PaymentRequest request);
        bool RefundPayment(string transactionId, decimal amount);
        EPaymentStatus CheckStatus(string transactionId);
    }
}
