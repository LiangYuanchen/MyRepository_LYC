using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace POC自动打包上线
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
       /// <summary>
       /// 去除&gt;&lt;添加
       /// </summary>
        public void DataBind()
        {
            string[] strs = new[] { "content", "actname", "fileNumber", "depts", "fromactname", "fromitemcontent", "toactname", "summary", "source", "fromitemname" };
            XML = XML.Replace("&gt;", ">").Replace("&lt;","<");
            foreach (var item in strs)
            {
                if (Regex.IsMatch(XML,"<"+item+">([\\s\\S]+?)</"+item+">"))
                {
                    XML = Regex.Replace(XML, "<" + item + ">([\\s\\S]+?)</" + item + ">", "<" + item + "><![CDATA[$1]]></" + item + ">");
                }
            }
           
        }
    }
}
