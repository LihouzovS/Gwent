﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent
{
    interface IPasswordSupplier
    {
        string GetPassword();
    }
}