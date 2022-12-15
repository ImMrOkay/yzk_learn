﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogServices
{
    public interface ILogProvider
    {
        void LogError(string message);

        void LogWarning(string message);
    }
}
