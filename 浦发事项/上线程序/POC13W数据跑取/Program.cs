using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading;
namespace POC13W数据跑取
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var query =( from a in db.Act
                            join b in db.Act_Items on a.ActID  equals b.ActID
                            where b.Item_Type==6 || b.Item_Type==3
                             select a.ActID).Distinct().OrderBy(f => f).ToList();
                int i = 0;
                int count = query.Count();
                foreach (var item in query)
                {
                    Act_ItemsAddParentID ai = new Act_ItemsAddParentID() { ActID=item };
                    ai.AddParentID();
                    Console.WriteLine(++i +"//"+ count);
                }
            }
        }



        static string GetFileName(string basepath, string name,int i=1,string kuozhan=".xml")
        {
            string filename = null;
                if (File.Exists(basepath + "\\" + name  + i +  kuozhan))
                {
                    filename = GetFileName(basepath,name,++i);
                }
                else
                {
                    filename = basepath + "\\" + name + i  + kuozhan;
                }
            return filename;
        }
    }
}
