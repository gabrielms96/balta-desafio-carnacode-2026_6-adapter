using DesignPatternAdapter.Client.Enums;
using DesignPatternAdapter.Target;

namespace DesignPatternAdapter.Client
{
    public class ModernPaymentProcessor : IPaymentProcessor
    {
        public PaymentResult ProcessPayment(PaymentRequest request)
        {
            Console.WriteLine("[Processador Moderno] Processando pagamento...");
            return new PaymentResult
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Pagamento aprovado"
            };
        }

        public bool RefundPayment(string transactionId, decimal amount)
        {
            Console.WriteLine($"[Processador Moderno] Reembolsando {amount:C}");
            return true;
        }

        public EPaymentStatus CheckStatus(string transactionId)
        {
            return EPaymentStatus.Approved;
        }
    }
}
