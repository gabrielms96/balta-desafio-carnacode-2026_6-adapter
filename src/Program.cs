using DesignPatternAdapter.Adaptee;
using DesignPatternAdapter.Adapter;
using DesignPatternAdapter.Client;
using DesignPatternAdapter.Service;

namespace DesignPatternAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Checkout ===\n");

            //Processador Moderno
            var modernProcessor = new ModernPaymentProcessor();
            var checkoutWithModern = new CheckoutService(modernProcessor);
            checkoutWithModern.CompleteOrder("cliente@email.com", 150.00m, "4111111111111111");
            Console.WriteLine("\n" + new string('-', 60) + "\n");

            //Adaptador para Sistema Legado
            var legacySystem = new LegacyPaymentSystem();
            var adapter = new PaymentAdapter(legacySystem);
            var checkoutWithLegacy = new CheckoutService(adapter);
            checkoutWithLegacy.CompleteOrder("cliente2@email.com", 200.00m, "4111111111111111");
            Console.WriteLine("\n" + new string('-', 60) + "\n");
        }
    }
}