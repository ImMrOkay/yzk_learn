using ConfigServices;
using LogServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailServices
{
    public class MailServices : IMailServices
    {
        private readonly ILogProvider log;
        //private readonly IConfigServices config;
        private readonly IConfigReader config;

        public MailServices(ILogProvider log, IConfigReader configReader)
        {
            this.log = log;
            this.config = configReader;
        }

        //public MailServices(ILogProvider log, IConfigServices config)
        //{
        //    this.log = log;
        //    this.config = config;
        //}

        public void Send(string title, string to, string body)
        {
            log.LogWarning("开始发送邮件：");
            string smtpServer = config.GetValue("SmtpServer");
            string userName=config.GetValue("MyName");
            string password=config.GetValue("Password");
            Console.WriteLine($"{title},{to},{body}");
            Console.WriteLine($"邮件服务器地址:{smtpServer},{userName},{password}");
            log.LogError("邮件发送完成！");
        }
    }
}
