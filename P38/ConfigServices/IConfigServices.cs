﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigServices
{
    public interface IConfigServices
    {
        string GetValue(string name);
    }
}
