using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using DoSomeThing.Utility;
using System.IO;

namespace DoSomeThing
{
    public partial class frmMain : Form
    {
        #region 初始化

        public frmMain()
        {
            InitializeComponent();
        }

        //显示提示信息的委托
        delegate void ShowTextDelegate(object o, string txt, bool enable);
        private ShowTextDelegate showTextDelegate;

        //数据库链接字符串
        string connSPD = string.Empty;

        private void frmMain_Load(object sender, EventArgs e)
        {
            showTextDelegate = new ShowTextDelegate(_showText);
            connSPD = System.Configuration.ConfigurationManager.ConnectionStrings["SPDAct"].ToString();
        }

        /// <summary>
        /// 使用委托设置控件值及状态
        /// </summary>
        /// <param name="o"></param>
        /// <param name="txt"></param>
        /// <param name="enable"></param>
        private void ShowText(object o, string txt, bool enable)
        {
            this.Invoke(showTextDelegate, new object[] { o, txt, enable });
        }

        /// <summary>
        /// 使用委托设置控件值及状态
        /// </summary>
        /// <param name="o"></param>
        /// <param name="txt"></param>
        /// <param name="enable"></param>
        private void _showText(object o, string txt, bool enable)
        {
            if (o is TextBox)
            {
                ((TextBox)o).Text = txt;
            }
            else if (o is RichTextBox)
            {
                ((RichTextBox)o).Text = txt;
            }
            else if (o is Label)
            {
                ((Label)o).Text = txt;
            }
            else if (o is Button)
            {
                ((Button)o).Text = txt;
                ((Button)o).Enabled = enable;
            }
        }

        #endregion

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (folderPath.ShowDialog() == DialogResult.OK)
            {
                if (folderPath.SelectedPath != "")
                {
                    txtPath.Text = folderPath.SelectedPath;
                }
            }
        }

        string zipPath = string.Empty;//压缩文件路径
        string xmlPath = string.Empty;//解压后Xml文件路径
        int total = 0;//更新包数量

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //选择更新包目录
            total = 0;
            zipPath = txtPath.Text;
            if (!Directory.Exists(zipPath))
            {
                MessageBox.Show(this, "请首先选择更新包目录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[] zipfiles = Directory.GetFiles(zipPath, @"*.zip");
            if (zipfiles.Length == 0)
            {
                MessageBox.Show(this, "更新包目录下面没有任何更新包zip！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string[]> filesList = new List<string[]>();
            foreach (string zipItem in zipfiles)
            {
                total++;
                xmlPath = System.IO.Directory.GetCurrentDirectory() + "\\zipupdate\\" + DateTime.Now.ToString("yyyyMMddHHmmss")+ total;
                Tools.unZipFile(zipItem, xmlPath);
                if (!Directory.Exists(xmlPath))
                {
                    MessageBox.Show(this, "第" + total + "个更新包中无更新文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string[] files = Directory.GetFiles(xmlPath, @"*.xml");
                filesList.Add(files);
                if (files.Length == 0)
                {
                    MessageBox.Show(this, "第" + total + "个更新包中没有任何更新包xml文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnUpdate.Enabled = false;

            } 
            Thread th = new Thread(new ParameterizedThreadStart(_threadGO));
            th.IsBackground = true;
            th.Start(filesList);
        }

        /// <summary>
        /// 更新主线程
        /// </summary>
        /// <param name="o"></param>
        private void _threadGO(object tho)
        {
            int totalth = 0;
            int totalcountth = 0;
            string msg = "";
            string logname = DateTime.Now.ToString("yyyyMMddHHmmss") + "_Log.txt";
            foreach (var o in (List<string[]>)tho)
            {
                totalth++;
                string[] files = (string[])o;
                //记录日志
                Tools.Log("\r\n第" + totalth + "个更新包开始更新 " + xmlPath + " 下所有更新文件...", logname);
                msg += "\r\n第" + totalth + "个更新包共有" + files.Length + "个更新文件，更新开始...\r\n\r\n";
                ShowText(txtMsg, msg, true);


                //循环读取每个更新文件
                DataSet ds = null;
                int totalcount = 0;
                foreach (String f in files)
                {
                    try
                    {
                        //提示信息
                        Tools.Log("第" + totalth + "个更新包正在更新 " + Path.GetFileName(f) + "...", logname);
                        msg += "第" + totalth + "个更新包正在更新 " + Path.GetFileName(f) + "...\r\n";
                        ShowText(txtMsg, msg, true);

                        //读取一个更新文件内容到DataSet
                        ds = new DataSet();
                        ds.ReadXml(f);

                        //更新一个更新文件里面法规，如果更新出错就返回错误信息
                        string errorMsg = UpdateOneXMLFile(ds, ref totalcount);
                        totalcountth ++;//更新总数量
                        if (errorMsg != "")
                        {
                            Tools.Log("第" + totalth + "个更新包更新文件" + Path.GetFileName(f) + "更新过程中遇到以下情况：\r\n" + errorMsg, logname);
                            msg += "第" + totalth + "个更新包更新文件" + Path.GetFileName(f) + "更新过程中出现错误，请查看更新日志！\r\n";
                            ShowText(txtMsg, msg, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Tools.Log("第" + totalth + "个更新包更新文件" + Path.GetFileName(f) + "读取异常：" + ex.Message, logname);
                        msg += "第" + totalth + "个更新包更新文件" + Path.GetFileName(f) + "更新过程中出现错误，请查看更新日志！\r\n";
                        ShowText(txtMsg, msg, true);
                    }
                }


                //更新完成
                Tools.Log("第" + totalth + "个更新包更新完成,共更新了" + totalcount + "部法规！", logname);
                msg += "\r\n第" + totalth + "个更新包更新完成,共更新了" + totalcount + "部法规！";
                ShowText(txtMsg, msg, true);
            }
            Tools.Log("全部更新包共更新了" + totalcountth + "部法规！", logname);
            msg += "\r\n全部更新包共更新了" + totalcountth + "部法规！";
            ShowText(txtMsg, msg, true);
            ShowText(btnUpdate, "开始", true);
            MessageBox.Show(this, "更新完成,共更新了" + totalcountth + "部法规！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 更新一个更新文件的内容
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private string UpdateOneXMLFile(DataSet ds, ref int num)
        {
            //检测更新文件数据正确性
            string errormsg = CheckXMLFile(ds);
            if (errormsg != "")
            {
                return errormsg;
            }

            //首先删除要更新的法规
            string strSqlDelete = "DELETE FROM dbo.Act WHERE ActID={0};DELETE FROM dbo.Act_Items WHERE ActID={0};DELETE FROM Act_Correlation WHERE toactid={0};DELETE FROM ActClauseNote WHERE toactid={0} ";
            //更新法规要素
            string strSqlAct = "INSERT INTO dbo.Act(ActID,ActName,FileNumber,Pub_Date,Sta_Date,Depts,State,effect,Content) VALUES ({0},'{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}')";
            //更新法条
            string strSqlItem = "INSERT INTO dbo.Act_Items(itemid,ActID,Item_Name,Item_Type,orders,Content,Item_ParentID) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            //更新关联法规
            string strCorrelation = "INSERT INTO [Act_Correlation]([ToActID],[ToItemID],[FromActID],[FromItemID],[FromActName],[FromItemContent],[FromItemName]) VALUES ('{0}','{1}' ,'{2}','{3}','{4}' ,'{5}' ,'{6}')";
            //更新法条注释
            string strActClauseNote = "INSERT INTO [ActClauseNote] ([ToActName],[ToActID] ,[ToItemID],[FromActName] ,[FromActID],[FromItemID] ,[Content] ,[FromItemName]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'  )";
            DataTable dtAct = ds.Tables["act"];
            DataView dvItem = ds.Tables["item"].DefaultView;
            DataView dvCorrelation = null, dvActClauseNote = null;
            if (ds.Tables["actcorrelation"] != null)
                dvCorrelation = ds.Tables["actcorrelation"].DefaultView;
            if (ds.Tables["actclausenote"] != null)
                dvActClauseNote = ds.Tables["actclausenote"].DefaultView;

            List<string> listSQL = new List<string>();
            foreach (DataRow dr in dtAct.Rows)
            {
                num++;

                listSQL.Clear();

                listSQL.Add(string.Format(strSqlDelete, dr["actid"].ToString()));

                listSQL.Add(string.Format(strSqlAct, dr["actid"].ToString(), dr["actname"].ToString().modifysql(), dr["filenumber"].ToString().modifysql(), dr["pubdate"].ToString(), dr["stadate"].ToString(), dr["depts"].ToString().modifysql(), dr["state"].ToString(), dr["effect"].ToString(), dr["content"].ToString()));

                dvItem.RowFilter = "actid=" + dr["actid"].ToString();
                foreach (DataRowView dritem in dvItem)
                {
                    listSQL.Add(string.Format(strSqlItem, dritem["itemid"].ToString(), dritem["actid"].ToString(), dritem["itemname"].ToString(), dritem["itemtype"].ToString(), dritem["orders"].ToString(), dritem["content"].ToString().modifysql(), dritem["itemparentid"].ToString()));
                }
                if (dvCorrelation != null)
                {
                    dvCorrelation.RowFilter = "toactid=" + dr["actid"].ToString();
                    foreach (DataRowView drc in dvCorrelation)
                    {
                        listSQL.Add(string.Format(strCorrelation, drc["toactid"].ToString(), drc["toitemid"].ToString(), drc["fromactid"].ToString(), drc["fromitemid"].ToString(), drc["fromactname"].ToString(), drc["fromitemcontent"].ToString(), drc["fromitemname"].ToString()));
                    }
                }
                if (dvActClauseNote != null)
                {
                    dvActClauseNote.RowFilter = "toactid=" + dr["actid"].ToString();
                    foreach (DataRowView dra in dvActClauseNote)
                    {
                        listSQL.Add(string.Format(strActClauseNote, dra["toactname"].ToString(), dra["toactid"].ToString(), dra["toitemid"].ToString(), dra["fromactname"].ToString(), dra["fromactid"].ToString(), dra["fromitemid"].ToString(), dra["content"].ToString(), dra["fromitemname"].ToString()));
                    }
                }
                //使用事务更新
                if (DBHelper.DbHelperSQL.ExecuteSqlTran(listSQL, connSPD) <= 0)
                {
                    errormsg += dr["actid"].ToString() + ",";
                }
            }

            if (errormsg != "")
            {
                errormsg = "以下法规更新失败：" + errormsg.TrimEnd(',');
            }
            return errormsg;
        }

        /// <summary>
        /// 检测更新文件数据是否正确
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private string CheckXMLFile(DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 2)
            {
                return "更新文件没有数据或更新格式不正确";
            }

            return string.Empty;
        }
    }
}
