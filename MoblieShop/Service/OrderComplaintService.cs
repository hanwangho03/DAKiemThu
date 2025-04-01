using Microsoft.AspNetCore.Identity;
using MoblieShop.Repository;
using WebDoDienTu.Models;
using WebDoDienTu.Service.MailKit;

namespace MoblieShop.Service
{
    public class OrderComplaintService : IOrderComplaintService
    {
        private readonly IOrderComplaintRepository _complaintRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public OrderComplaintService(IOrderRepository orderRepository ,IOrderComplaintRepository complaintRepository, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _orderRepository = orderRepository;
            _complaintRepository = complaintRepository;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<OrderComplaint?> GetComplaintFormAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            return new OrderComplaint { OrderId = orderId, Order = order };
        }

        public async Task<bool> SubmitComplaintAsync(OrderComplaint complaint, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            complaint.UserId = userId;
            complaint.ComplaintDate = DateTime.UtcNow;
            complaint.Status = OrderComplaintStatus.Pending;

            await _complaintRepository.AddComplaintAsync(complaint);

            // Gửi email thông báo
            var emailContent = GenerateComplaintSubmittedEmailContent(complaint, user);
            await _emailSender.SendEmailAsync(user.Email, "Thông báo gửi khiếu nại thành công", emailContent);

            return true;
        }

        public async Task<IEnumerable<OrderComplaint>> GetAllComplaintsAsync()
        {
            return await _complaintRepository.GetAllComplaintsAsync();
        }

        public async Task<bool> ResolveComplaintAsync(int id, OrderComplaintStatus status, string adminResponse)
        {
            var complaint = await _complaintRepository.GetComplaintByIdAsync(id);
            if (complaint == null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(complaint.UserId);
            if (user == null)
            {
                return false;
            }

            complaint.Status = status;
            await _complaintRepository.UpdateComplaintAsync(complaint);

            string emailContent = GenerateEmailContent(complaint, adminResponse);
            await _emailSender.SendEmailAsync(user.Email, "Thông báo giải quyết khiếu nại", emailContent);

            return true;
        }

        private string GenerateEmailContent(OrderComplaint complaint, string adminResponse)
        {
            string template = System.IO.File.ReadAllText("Templates/OrderComplaints/ResolveComplaintEmailTemplate.html");
            template = template.Replace("{{UserName}}", complaint.User.LastName);
            template = template.Replace("{{OrderId}}", complaint.OrderId.ToString());
            template = template.Replace("{{Status}}", complaint.Status.ToString());
            template = template.Replace("{{AdminResponse}}", adminResponse);
            return template;
        }

        private string GenerateComplaintSubmittedEmailContent(OrderComplaint complaint, ApplicationUser user)
        {
            string template = System.IO.File.ReadAllText("Templates/OrderComplaints/ComplaintSubmittedEmailTemplate.html");
            template = template.Replace("{{UserName}}", user.LastName);
            template = template.Replace("{{OrderId}}", complaint.OrderId.ToString());
            return template;
        }
    }
}
