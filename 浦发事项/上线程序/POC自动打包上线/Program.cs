using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
namespace POC自动打包上线
{
    class Program
    {
        static void Main(string[] args)
        {
            //运行存储过程
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                Console.WriteLine("开始运行存储过程！1/6");
                db.CommandTimeout = 65536;
                db.UPload_Data();
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 6))
                {
                    DateTime theweekbigin = DateTime.Now.AddDays(-6);
                    var count = (from a in db.Act
                                 where a.op_date > theweekbigin
                                 select a).Count();
                    if (count < 225)
                    {
                        db.UPload_Data_Friday(theweekbigin);
                        int newcount = (from a in db.Act
                                        where a.IsOnline == 1
                                        select a).Count();
                        if ((newcount + count) < 255)
                        {
                            db.UPload_Data_Friday2(theweekbigin);
                        }
                    }
                }
            }


            string basepath = AppDomain.CurrentDomain.BaseDirectory + "\\Data\\" + DateTime.Now.ToString("yyyyMMddHHmm");

            List<POCAct> POCActs = new List<POCAct>() ;

            const string TO_XML_STRING = @"select ( select top 1 actid as '@actid',replace(replace(act_name,char(13)+char(10),''),char(10),'') as actname,replace(replace(isnull(fileNumber,''),char(13)+char(10),''),char(10),'') as fileNumber,isnull(convert(nvarchar,pub_date,111),'') as 'pubdate',isnull(convert(nvarchar,sta_date,111),'') as 'stadate',isnull(convert(nvarchar,end_date,111),'') as 'enddate',replace(replace(left( mis.dbo.fn_SolrJoinActDeptName(actid,','),len(mis.dbo.fn_SolrJoinActDeptName(actid,','))-1),char(13)+char(10),''),char(10),'') as depts,dbo.[fn_ActDeptID](actid,',') as deptids,{1} as [state],convert(nvarchar,getdate(),111) as opdate,effect,'{2}'  as 'content',
                (select itemid as '@itemid',actid,isnull(item_name,'') as 'itemname',item_type as 'itemtype',isnull(orders,'') as orders,replace(replace(cast(isnull(content,'') as nvarchar(max) ),char(13)+char(10),'<br/>'),char(10),'<br/>') as content,isnull(Item_ParentID,'') as itemparentid  from Act_Items where actid = act.actid for xml path('item'),type
                ) as 'actitems' ,
                (select c.actid as toactid,
                c.itemid as toitemid,c.objid as fromactid,replace(replace(a.act_name,char(13)+char(10),''),char(10),'') as fromactname,'' as fromitemid,'' as fromitemcontent,'' as fromitemname from (select actid,itemid,objid from Act_Correlation where actid=act.actid ) c  inner join mis.dbo.act a on c.objid=a.actid  for xml path('actcorrelation'),type
                 )  as actcorrelations,
                (SELECT replace(replace(a.toactname,char(13)+char(10),''),char(10),'') as toactname,a.toactid,isnull(a.toitemid,'') as toitemid,replace(replace(isnull(a.fromactname,''),char(13)+char(10),''),char(10),'') as fromactname,isnull(a.fromactid,'') as fromactid,isnull(a.fromitemid,'') as fromitemid,replace(replace(cast(isnull(a.content,'') as nvarchar(max) ),char(13)+char(10),'<br/>'),char(10),'<br/>') as content,replace(replace(isnull(a.summary,''),char(13)+char(10),'<br/>'),char(10),'<br/>') as summary,replace(replace(cast(isnull(a.source,'') as nvarchar(max) ),char(13)+char(10),'<br/>'),char(10),'<br/>') as source,replace(replace(isnull(i.item_name,''),char(13)+char(10),''),char(10),'') as fromitemname FROM [ActClauseNote] a inner join act_items i on a.FromItemID=i.itemid where toactid=act.actid for xml path('actclausenote'),type
                 ) as actclausenotes
                 from act where actid = {0}
                 for xml path('act'),root('root')) a
                ";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Data") && ConfigurationManager.AppSettings["isDel"] == "true")
            {
                Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Data", true);
            }

            if (Directory.Exists(basepath))
            {
                Directory.Delete(basepath,true);
            }
            Directory.CreateDirectory(basepath);

            //导出法规
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                Console.WriteLine("获取上下线数据2/6");
                //上线数据
                var query = (from a in db.Act
                             where a.IsOnline==1
                          select new { a.ActID,a.Act_Name});
                foreach (var item in query)
                {
                    //添加父ID
                    Act_ItemsAddParentID acthander = new Act_ItemsAddParentID() {  ActID=item.ActID };
                    acthander.AddParentID();

                    string sql = string.Format(TO_XML_STRING, item.ActID.ToString(),0, GetContentByItems((from it in db.Act_Items
                                                                                           where it.ActID==item.ActID
                                                                                           select it).ToList()));
                    string result = db.ExecuteQuery<string>(sql).FirstOrDefault();
                    POCActs.Add(new POCAct() { ActID=item.ActID,   Act_Name=item.Act_Name, XML=result });
                }
                //下线数据
                query = (from a in db.Act
                         where a.IsOnline == 4
                         select new { a.ActID, a.Act_Name });
                foreach (var item in query)
                {
                    //添加父ID
                    Act_ItemsAddParentID acthander = new Act_ItemsAddParentID() { ActID = item.ActID };
                    acthander.AddParentID();

                    string sql = string.Format(TO_XML_STRING, item.ActID.ToString(), 1, GetContentByItems((from it in db.Act_Items
                                                                                                           where it.ActID == item.ActID
                                                                                                           select it).ToList()));
                    string result = db.ExecuteQuery<string>(sql).FirstOrDefault();
                    POCActs.Add(new POCAct() { ActID = item.ActID, Act_Name = item.Act_Name, XML = result,IsDel=true });
                }
            }
            //生成xml
            foreach (var item in POCActs)
            {
                //生成全文
                item.DataBind();

                Console.WriteLine("生成XML3/6");
                string filename = GetFileName(basepath,DateTime.Now.ToString("yyyyMMdd"));

                StreamWriter sw = File.CreateText(filename);
                sw.Write(item.XML);
                sw.Close();
            }
            //若存在图片导出图片
            using (DataClasses2DataContext db = new DataClasses2DataContext())
            {
                Console.WriteLine("到处图片库数据4/6");
                var query = from a in db.ActPic
                            where POCActs.Select(f => f.ActID).Contains(a.ActID) && a.PictureState==2
                            select a;
                if (query.Count()>0)
                {
                    string picFromPath = ConfigurationManager.AppSettings["picFromPath"];
                    string picToPath = basepath+"\\actimages";
                    if (Directory.Exists(picToPath))
	                {
		                Directory.Delete(picToPath,true);
	                }
                    Directory.CreateDirectory(picToPath);

                    foreach (var item in query)
                    {
                        string path = picFromPath + "\\" + item.ActID;
                        if (Directory.Exists(path))
                        {
                            Directory.CreateDirectory(picToPath + "\\" + item.ActID);
                            List<string> files = Directory.GetFiles(path, "*.jpg").ToList();
                            files.AddRange(Directory.GetFiles(path, "*.jpeg").ToList());
                            files.AddRange(Directory.GetFiles(path, "*.gif").ToList());
                            files.AddRange(Directory.GetFiles(path, "*.png").ToList());
                            files.AddRange(Directory.GetFiles(path, "*.bmp").ToList());
                            foreach (var file in files)
                            {
                                string filename = file.Substring(file.LastIndexOf('\\'));
                                File.Copy(file, picToPath + "\\" + item.ActID + "\\" + filename);
                            }
                        }
                    }
                }
            }

            //压包
            Console.WriteLine("压包5/6");
            string zipBasePath = AppDomain.CurrentDomain.BaseDirectory + "Zip";
            string zipPath =zipBasePath+"\\"+DateTime.Now.ToString("yyyyMMddHHmm")+ ".zip";
            
            if (Directory.Exists(zipBasePath))
            {
                Directory.Delete(zipBasePath,true);
            }
            Directory.CreateDirectory(zipBasePath);
            ZipFloClass zip = new ZipFloClass();
            if (Directory.GetFiles(basepath, "*.*", SearchOption.AllDirectories).Count() > 0)
            {
                zip.ZipFile(basepath, zipPath);


                //上传
                Console.WriteLine("上传6/6");
                FtpWeb web = new FtpWeb();
                web.Upload(zipPath);


                //更改数据库状态
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    var query = from d in db.Act
                                where POCActs.Where(f => f.IsDel == false).Select(f => f.ActID).Contains(d.ActID)
                                select d;
                    foreach (var item in query)
                    {
                        item.IsOnline = 2;
                        item.op_date = DateTime.Now;
                    }
                    query = from d in db.Act
                            where POCActs.Where(f => f.IsDel == true).Select(f => f.ActID).Contains(d.ActID)
                            select d;
                    foreach (var item in query)
                    {
                        item.IsOnline = 3;
                        item.op_date = DateTime.Now;
                    }
                    db.SubmitChanges();

                }

            }
            else
            { 
                //不上传，考虑写个日志什么的，暂留
            }
        }
        static string ReplaceEnter(string content)
        {
            return content.Replace("\r\n", "<br/>").Replace("\n","<br/>");
        }
        static string GetContentByItems(List<Act_Items> parms)
        {
            var query = parms.OrderBy(f => f.orders);
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
            result = result.Replace("\r\n", "<br/>").Replace("\n", "<br/>");
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
