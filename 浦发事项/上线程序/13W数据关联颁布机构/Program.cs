using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13W数据关联颁布机构
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ActDept> depts = null;
            List<int> ActIDs = null;
            Dictionary<int, string> result = new Dictionary<int, string>();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                depts = (from ad in db.ActDept
                        select ad).ToList();
                ActIDs = (from ad in db.Act
                          select ad.ActID).ToList();
            }
            using (DataClasses2DataContext db = new DataClasses2DataContext())
            {
                foreach (var actid in ActIDs)
                {
                    string sql = "select count(1) from  act inner join (select actid,deptid from act_dept where actid=" + actid + ") act_dept on act.actid = act_dept.actid inner join (select * from dept  ) dept on act_dept.deptid=dept.deptid {0}";
                    foreach (var item in depts)
                    {
                        if (item.Summary!=null&&item.Summary.Length>0)
                        {
                            if (db.ExecuteQuery<int>(string.Format(sql, item.Summary)).First() > 0)
                            {
                                using (DataClasses1DataContext dbs = new DataClasses1DataContext())
                                {
                                    dbs.Log = Console.Out;
                                    var DeptIDs = (from a in dbs.Act
                                                   where a.ActID == actid
                                                   select a.DeptIDs).First();
                                    string sqlp = "";
                                    if (DeptIDs!=null&&DeptIDs.Length > 0)
                                    {
                                        sqlp = "update act set DeptIDs =DeptIds + '," + item.ID + "' where actid  =" + actid;
                                    }
                                    else
                                    {
                                        sqlp = "update act set DeptIDs = " + item.ID + " where actid = " + actid;
                                    }
                                    dbs.ExecuteCommand(sqlp);
                                    Console.WriteLine(sqlp);
                                    dbs.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
