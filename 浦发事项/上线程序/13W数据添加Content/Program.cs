using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _13W数据添加Content
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int i = 0;
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.Log = Console.Out;
                count = db.Act.Count();
            }
            
                do{
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        var query = (from act in db.Act
                                    select act).Skip(i).Take(10000);
                        foreach (var item in query)
                        {
                            item.Content = GetContentByItems((from items in db.Act_Items
                                                                  where items.ActID==item.ActID
                                                                  select items).ToList());
                            db.SubmitChanges();
                        }
                        i += 10000;
                    }
                }
                while(i<=count);
        }
        static string GetContentByItems(List<Act_Items> parms)
        {
            var query = parms.OrderBy(f => f.Orders);
            string result = "";
            foreach (var item in query)
            {
                string parmContent = item.Content;
                parmContent = parmContent.Replace(" ", "&nbsp;").Replace("'", "&acute;").Replace("\"", "&quot;");
                switch (item.Item_Type.ToString())
                {
                    case "0": result += LeftAlign(parmContent); break;
                    case "1": result += LeftAlign(parmContent); break;
                    case "2": result += LeftAlign(parmContent); break;
                    case "3": result += MidStrong(parmContent); break;
                    case "4": result += LeftIndent(parmContent); break;
                    case "5": result += LeftIndent(parmContent); break;
                    case "6": result += MidStrong(parmContent); break;
                    default: result += LeftAlign(parmContent);
                        break;
                }
            }
            result = result.Replace("\r\n", "<br/>").Replace("\r", "<br/>").Replace("\n", "<br/>");
            return result;
        }
        static string LeftAlign(string str)
        {
            return "<div align=left>" + str + "</div>";
        }
        static string RightAlign(string str)
        {
            return "<div align=right>" + str + "</div>";
        }
        static string MidStrong(string str)
        {
            return "<div align=center><strong>" + str + "</strong></div>";
        }
        static string LeftIndent(string str)
        {
            return "<div align=left style=text-indent:2em; >" + str + "</div>";
        }
    }
}
