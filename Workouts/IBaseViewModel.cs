﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp
{
    public interface IBaseViewModel
    {
        public Task ManageException(object ex);
    }
}