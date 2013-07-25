using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace _13W
{
    class Program
    {
        static void Main(string[] args)
        {
            string topath = @"E:\";
            
            int fullcount = 0;
            using(DataClasses1DataContext db = new DataClasses1DataContext())
	        {
		        fullcount = (from act in db.Act
                            select 1).Count();
	        }
            int i;
            i = 0;
            do
            {
                IList<Act> query = null;
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    query = (from act in db.Act
                                where act.Content != null && act.Content.Length > 0
                                select act).Skip(i * 10000).Take(10000).ToList();
                    //query = (from act in db.Act
                    //            where act.Content != null && act.Content.Length > 0
                    //            select act).Take(10000).ToList();
                }
                StringBuilder Facts = new StringBuilder(); StringBuilder Fitems = new StringBuilder(); StringBuilder Fcorr = new StringBuilder(); StringBuilder Facn = new StringBuilder();
                        IList<Act_Items> actitems=null;
                        IList<Act_Correlation> Act_Correlation=null;
                        IList<ActClauseNote> ActClauseNote=null;
                        Console.WriteLine("开始跑去数据!");
                        foreach (var item in query)
                        {
                            using (DataClasses1DataContext db = new DataClasses1DataContext())
                            {
                                Console.WriteLine(item.ActID+"/"+item.ActName);
                                Facts .Append( item.ID.ToString() + "@|@" + item.ActID.ToString() + "@|@" + item.ActName + "@|@" + item.FileNumber + "@|@" + GetDateTime(item.Pub_Date) + "@|@" + GetDateTime(item.Sta_Date) + "@|@" + item.Depts + "@|@" + item.Content + "@|@" + item.State.ToString() + "@|@" + GetDateTime(item.OpDate) + "@|@" + item.Effect.ToString() + "\n");
                                 actitems =( from items in db.Act_Items
                                               where items.ActID==item.ActID
                                               select items).ToList();
                                 Act_Correlation =( from corr in db.Act_Correlation
                                                      where corr.ToActID==item.ActID
                                                      select corr).ToList();
                                 ActClauseNote = (from acn in db.ActClauseNote
                                                    where acn.ToActID==item.ActID
                                                    select acn).ToList();
                            }
                                 foreach (var ite in actitems)
                                 {
                                     Fitems .Append( ite.ItemID.ToString() + "@|@" + ite.ActID.ToString() + "@|@" + ite.Item_Name + "@|@" + ite.Item_Type.ToString() + "@|@" + ite.Item_ParentID.ToString() + "@|@" + ite.Orders.ToString() + "@|@" + ite.Content + "" + "\n");
                                 }
                                 foreach (var ite in Act_Correlation)
                                 {
                                     Fcorr .Append( ite.ID.ToString() + "@|@" + ite.ToActID.ToString() + "@|@" + ite.ToItemID.ToString() + "@|@" + ite.FromActID.ToString() + "@|@" + ite.FromItemID + "@|@" + ite.FromActName + "@|@" + ite.FromItemContent + "@|@" + ite.FromItemName + "" + "\n");
                                 }
                                 foreach (var ite in ActClauseNote)
                                 {
                                     Facn .Append( ite.ID.ToString() + "@|@" + ite.ToActName.ToString() + "@|@" + ite.ToActID.ToString() + "@|@" + ite.ToItemID.ToString() + "@|@" + ite.FromActName + "@|@" + ite.FromActID + "@|@" + ite.FromItemID + "@|@" + ite.Content + "@|@" + ite.Summary + "@|@" + ite.source + "@|@" + ite.FromItemName + "" + "\n");
                                 }
                        }
                Console.WriteLine("数据跑去完成，开始写入！");
            FileStream fs = new FileStream(topath + "Act_"+i+".txt",FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GBK"));
            sw.Write(Facts.ToString());
            sw.Close();
            fs.Close();
            fs = new FileStream(topath + "Act_Items_"+i+".txt", FileMode.Create);
            sw = new StreamWriter(fs, Encoding.GetEncoding("GBK"));
            sw.Write(Fitems.ToString());
            sw.Close();
            fs.Close();
            fs = new FileStream(topath + "Act_Correlation_"+i+".txt", FileMode.Create);
            sw = new StreamWriter(fs, Encoding.GetEncoding("GBK"));
            sw.Write(Fcorr.ToString());
            sw.Close();
            fs.Close();
            fs = new FileStream(topath + "ActClauseNote_"+i+".txt", FileMode.Create);
            sw = new StreamWriter(fs, Encoding.GetEncoding("GBK"));
            sw.Write(Facn.ToString());
            sw.Close();
            fs.Close();  
        }
            while((i++)*10000<=fullcount);
          //  while (1==0);
        }
        static string GetDateTime(DateTime? datetime)
        {
            if (datetime == null)
            {
                return "";
            }
            else
            {
               return   ((DateTime)datetime).ToString("yyyy-MM-dd");
            }
        }
    }
}
