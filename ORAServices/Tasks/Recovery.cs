using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ORAServices.Tasks
{
    public class Recovery
    {
        public static string PasswordRecovery(string username, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("nilusilu3@gmail.com");
                //msg.To.Add(TextBox1.Text);
                msg.Subject = "Recover your Password";
                msg.Body = ("Your Username is:" + username + "<br/><br/>" + "Your Password is:" + password);
                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                NetworkCredential ntwd = new NetworkCredential();
                ntwd.UserName = "nilusilu3@gmail.com"; //Your Email ID  
                ntwd.Password = ""; // Your Password  
                smt.UseDefaultCredentials = true;
                smt.Credentials = ntwd;
                smt.Port = 587;
                smt.EnableSsl = true;
                smt.Send(msg);
                //lbMsg.Text = "Username and Password Sent Successfully";
                //lbMsg.ForeColor = System.Drawing.Color.ForestGreen;
                return "Processing";
            }
            else return string.Empty;
        }//
    }
}