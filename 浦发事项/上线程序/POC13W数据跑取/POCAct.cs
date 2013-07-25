using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POC13W数据跑取
{
   public  class POCAct
    {
        public int ActID { get; set; }
        public string Act_Name { get; set; }
        public string XML { get; set; }
        public Boolean IsDel { get; set; }
        
        public POCAct()
        {
            IsDel = false;
        }
    }
}
