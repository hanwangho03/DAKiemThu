using WebDoDienTu.Models;
using WebDoDienTu.Service.MomoPayment;
using WebDoDienTu.Service.PayPal;
using WebDoDienTu.Service.VNPayPayment;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IMomoPaymentService _momoPaymentService;
        private readonly IPayPalPaymentService _payPalPaymentService;
        private readonly IVnPayService _vnPayService;

        public PaymentService(IMomoPaymentService momoPaymentService, IPayPalPaymentService payPalPaymentService, IVnPayService vnPayService)
        {
            _momoPaymentService = momoPaymentService;
            _payPalPaymentService = payPalPaymentService;
            _vnPayService = vnPayService;
        }

        public async Task<string> ProcessPaymentAsync(Order order, string paymentMethod)
        {
            switch (paymentMethod)
            {
                case "MoMo":
                    return await _momoPaymentService.CreatePaymentUrl(new MomoPaymentRequestModel { Amount = (double)order.TotalPrice, OrderId = order.Id.ToString() });

                case "PayPal":
                    return _payPalPaymentService.CreatePayment(order.TotalPrice, "returnUrl", "cancelUrl").links.FirstOrDefault(l => l.rel == "approval_url")?.href;

                case "VNPay":
                    return _vnPayService.CreatePaymentUrl(null, new VnPaymentRequestModel { Amount = (double)order.TotalPrice, OrderId = order.Id });

                default:
                    throw new NotSupportedException("Phương thức thanh toán không được hỗ trợ.");
            }
        }
    }
}
