﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MailServices
{
    public interface IMailServices
    {
        void Send(string title, string to, string body);
    }
}
