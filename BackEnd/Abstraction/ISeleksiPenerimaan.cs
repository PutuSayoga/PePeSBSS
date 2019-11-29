﻿using BackEnd.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Abstraction
{
    public interface ISeleksiPenerimaan
    {
        IEnumerable<AkunPendaftaran> GetAllWithJalur(string jalur);
        void SelectionNonReguler(int akunId, bool isLolos);
    }
}
