﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagmentSystemBackend.Core.Domain.Models;

namespace TransportManagmentSystemBackend.Core.Interfaces.Repositories
{
    public interface ICabRepository
    {
        Task<CabResponse> InsertCab(CabRequest request);
        Task<CabResponse> UpdateCab(CabRequest request);
    }
}
