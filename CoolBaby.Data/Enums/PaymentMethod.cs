using System.ComponentModel;

namespace CoolBaby.Data.Enums
{
    public enum PaymentMethod
    {
        [Description("Cash on delivery")]
        CashOnDelivery,

        [Description("Onlin Banking")]
        OnlinBanking,

        [Description("Payment Gateway")]
        PaymentGateway,

        [Description("Visa")]
        Visa,

        [Description("Master Card")]
        MasterCard,

        [Description("PayPal")]
        PayPal,

        [Description("Atm")]
        Atm
    }
}