using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WBL_Project.Models;
using System.Net.Mail;
using System.Net;

namespace WBL_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(tb_user objchk)
        {
            if (ModelState.IsValid)
            {
                using (db_WBLEntities db = new db_WBLEntities())
                {
                    var obj = db.tb_user.Where(u => u.u_username.Equals(objchk.u_username) && u.u_pwd.Equals(objchk.u_pwd)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["id"] = obj.u_id.ToString();
                        Session["name"] = obj.u_name.ToString();
                        Session["role"] = obj.u_type.ToString();
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "The name or password is incorrect.");
                    }
                }
            }
            return View(objchk);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [NonAction]
        public void SendVerificationEmail(string emailID, string activationCode, string emailFor = "ResetPassword")
        {
            var verifyUrl = "/Login/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("gadgetechrental@gmail.com", "wbl gadgetech");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "bhpvzmtaeuyyavra"; // Replace with actual password

            string subject = "";
            string body = "";

            if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset Password Link
            //Send email

            string message = "";

            using (db_WBLEntities db = new db_WBLEntities())
            {
                var account = db.tb_user.Where(u => u.u_email == EmailID).FirstOrDefault();

                if (account != null)
                {
                    //send email to reset password
                    //generate unique identification number
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationEmail(account.u_email, resetCode, "ResetPassword");//method
                    account.u_resetpwdCode = resetCode;

                    //avid confirm password not match issue
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email address";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //verify the reset password link
            //find account associated with this link
            //redirect user to reset password page
            using (db_WBLEntities db = new db_WBLEntities())
            {
                var user = db.tb_user.Where(u => u.u_resetpwdCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (db_WBLEntities db = new db_WBLEntities())
                {
                    var user = db.tb_user.Where(u => u.u_resetpwdCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.u_pwd = model.NewPassword;
                        user.u_resetpwdCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
    }
}