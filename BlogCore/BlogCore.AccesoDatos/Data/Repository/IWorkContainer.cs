﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface IWorkContainer : IDisposable
    {
        ICategoryRepository categoryRepository { get; }

        void Save();
    }
}