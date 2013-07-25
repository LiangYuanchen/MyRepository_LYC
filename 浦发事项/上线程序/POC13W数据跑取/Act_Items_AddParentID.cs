using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POC13W数据跑取
{
   public  class Act_ItemsAddParentID:Act
    {
       public List<Act_Items> ItemsList { get; set; }
       /// <summary>
       /// 用于父ID计算方法
       /// </summary>
       Act_Items ZhangItem;
       /// <summary>
       /// 用于父ID计算方法
       /// </summary>
       Act_Items JieItem ;

       /// <summary>
       /// 获取ItemType为6的数据，并赋值3
       /// </summary>
       public void GetItemType6()
       {
           if (ItemsList != null)
           {


               foreach (var item in ItemsList.Where(f => f.Item_Type == 3).OrderBy(f => f.Orders))
               {
                   if (item.Content.Length>4&&item.Content.Substring(0, 4).Contains("节") && item.Content.Substring(0, 4).Contains("第"))
                   {
                       item.Item_Type = 6;
                   }
               }
           }
           else
           {
               throw new Exception("还未获取Act_Items");
           }
       }

       /// <summary>
       /// 获取父ActID的算法
       /// 算出节的item
       /// 算法简要描述：获取排序好的法条，获取每一个条和节
       /// 向下遍历
       /// </summary>
       public void AddParentID() {
           GetItems();
         //  GetItemType6();
           foreach (var item in ItemsList)
           {
               switch (item.Item_Type)
               {
                   case 3: ZhangHandler(item); break;
                   case 4: TiaoHandler(item); break;
                   case 6: JieHandler(item); break;
                   default:
                       break;
               }
           }
           Insert();
       }

       private void ZhangHandler(Act_Items item)
       {
           ZhangItem = item;
       }

       private void JieHandler(Act_Items item)
       {
           JieItem = item;
           if (ZhangItem != null)
           {
               item.Item_ParentID = ZhangItem.ItemID;
           }
           //else
           //{
           //    throw new Exception("节的前面没有章ID！");
           //}
       }

       private void TiaoHandler(Act_Items item)
       {
           if (JieItem != null && ZhangItem != null && JieItem.Orders > ZhangItem.Orders)
           {
               item.Item_ParentID = JieItem.ItemID;
           }
           else if (JieItem != null)
           {
               item.Item_ParentID = JieItem.ItemID; ;
           }
           else if (ZhangItem != null)
           {
               item.Item_ParentID = ZhangItem.ItemID;
           }
           //else
           //{
           //    throw new Exception("条添加父ID时出现异常！");
           //}
       }

       /// <summary>
       /// 获取按照orders排序的本法规Items列表
       /// </summary>
       private void GetItems()
       {
           using (DataClasses1DataContext db = new DataClasses1DataContext())
           {
               ItemsList = (from a in db.Act_Items
                            where a.ActID == ActID &&( a.Item_Type==3 || a.Item_Type==4|| a.Item_Type==6)
                            orderby a.Orders
                            select a).ToList();
           }
       }
       private void Insert()
       {
           if (ItemsList != null)
           {
               using (DataClasses1DataContext db = new DataClasses1DataContext())
               {
                   foreach (var item in ItemsList)
                   {
                       db.ExecuteCommand("update Act_Items set Item_ParentID='" + item.Item_ParentID + "' where itemid = '"+item.ItemID+"'"); 
                   }
               }
           }
       }
    }
}
