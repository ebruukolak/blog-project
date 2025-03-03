using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.AppSettingsModels
{
    public class EmailSettings
    {
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public required string SenderEmail { get; set; }
        public required string Sender { get; set; }
        public string? SenderPassword { get; set; }
        public bool EnableSsl { get; set; }
    }
}




