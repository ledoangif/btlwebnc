﻿using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models
{
    public partial class TblYeuThich
    {
        public TblYeuThich()
        {
            TblCtyeuThiches = new HashSet<TblCtyeuThich>();
        }

        public int IdyeuThich { get; set; }
        public string? IdkhachHang { get; set; }

        public virtual TKhachHang? IdkhachHangNavigation { get; set; }
        public virtual ICollection<TblCtyeuThich> TblCtyeuThiches { get; set; }
    }
}
