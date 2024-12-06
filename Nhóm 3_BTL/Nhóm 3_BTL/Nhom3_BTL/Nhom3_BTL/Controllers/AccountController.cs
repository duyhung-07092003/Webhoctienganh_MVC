using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nhom3_BTL_NguyenDuyHung.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> SendOtp(string email)
        {
            try
            {
                // Check if the email is empty
                if (string.IsNullOrWhiteSpace(email))
                {
                    ViewBag.ErrorMessage = "Địa chỉ email không được để trống.";
                    return View("Index");
                }

                // Generate a random OTP
                var otp = new Random().Next(100000, 999999).ToString();
                Session["UserOtp"] = otp;
                Session["UserEmail"] = email;

                // Prepare email content
                string subject = "Mã OTP của bạn";
                string message = $"Xin chào, mã OTP của bạn là: {otp}";

                // Send the email asynchronously
                await SendEmailAsync(email, subject, message);

                // Redirect to VerifyOtp view
                return RedirectToAction("VerifyOtp");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi khi gửi OTP: {ex.Message}";
                return View("Index");
            }
        }

        private Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("duyhung792003@gmail.com", "agbumwsrhyukpuen")
            };

            var mailMessage = new MailMessage(
                from: "duyhung792003@gmail.com",
                to: email,
                subject,
                message
            );

            return client.SendMailAsync(mailMessage);
        }
        public ActionResult VerifyOtp()
        {
            if (Session["UserEmail"] == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Email = Session["UserEmail"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult CheckOtp(string otp1, string otp2, string otp3, string otp4, string otp5, string otp6)
        {
            var enteredOtp = $"{otp1.Trim()}{otp2.Trim()}{otp3.Trim()}{otp4.Trim()}{otp5.Trim()}{otp6.Trim()}";

            // Kiểm tra mã OTP rỗng
            if (string.IsNullOrWhiteSpace(enteredOtp))
            {
                ViewBag.ErrorMessage = "Mã OTP không được để trống.";
                return View("VerifyOtp");
            }

            // Lấy mã OTP từ session
            var sessionOtp = Session["UserOtp"] as string;
            if (sessionOtp == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy mã OTP trong hệ thống. Vui lòng thử lại.";
                return View("VerifyOtp");
            }

            // So sánh mã OTP nhập vào với mã trong session
            if (sessionOtp == enteredOtp)
            {
                // OTP chính xác, xóa session và chuyển hướng
                Session.Remove("UserOtp");
                Session.Remove("UserEmail");
                return RedirectToAction("Index", "TrangChu");
            }

            // OTP không chính xác
            ViewBag.ErrorMessage = "Mã OTP không đúng.";
            return View("VerifyOtp");
        }
        public async Task<ActionResult> ResendOtp()
        {
            var email = Session["UserEmail"] as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(new { success = false, message = "Không tìm thấy email để gửi lại OTP." });
            }

            var otp = new Random().Next(100000, 999999).ToString();
            Session["UserOtp"] = otp;

            string subject = "Mã OTP mới của bạn";
            string message = $"Xin chào, mã OTP mới của bạn là: {otp}";

            await SendEmailAsync(email, subject, message);

            return Json(new { success = true, message = "Mã OTP mới đã được gửi tới email của bạn." });
        }

    }
}