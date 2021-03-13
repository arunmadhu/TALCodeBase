﻿using PremiumDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PremiumDomain.Infrastructure 
{
    public interface IOccupationRepository
    {
        IList<Occupation> GetOccupations();

         Occupation GetOccupationById(int occupationId);
    }
}
