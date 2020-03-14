using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stu_Client.Properties;
using Stu_Client.dstClientTableAdapters;
using System.Text.RegularExpressions;

namespace Stu_Client
{
    public partial class frmContent : Form
    {
        public frmContent()
        {
            InitializeComponent();
        }
        public frmContent(frmLogin frm)
        {
            InitializeComponent();
            frmThis = frm;
        }
        frmLogin frmThis;
        Random r = new Random();
        //public void bgkColor()
        //{
        //    int num = r.Next() % 6;
        //    switch (num)
        //    {
        //        case 1:
        //            this.BackColor = Color.Gray;
        //            break;
        //        case 2:
        //            this.BackColor = Color.Salmon;
        //            break;
        //        case 3:
        //            this.BackColor = Color.Orange;
        //            break;
        //        case 4:
        //            this.BackColor = Color.SeaGreen;
        //            break;
        //        case 5:
        //            this.BackColor = Color.DodgerBlue;
        //            break;
        //    }
        //}

        private void tmrControl_Tick(object sender, EventArgs e)
        {
           //bgkColor();
            lblTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void frmContent_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dstClient.T_Financing”中。您可以根据需要移动或移除它。
            this.t_FinancingTableAdapter.Fill(this.dstClient.T_Financing);
            tmrControl.Enabled = true;
            lblWeek.Text = DateTime.Now.ToString("dddd");
            lblDate.Text = DateTime.Now.ToString("MM月dd日");
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确实现在就要退出系统吗？", "iCloud 2012", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            { 
                this.Visible = false;
                frmThis.Visible = true;
                frmThis.ClearScreen();
                frmThis.ShowMsg("正在关机中...");
                Application.Exit();
            }
            return;
        }



        private void btnWeather_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
            pnlWeather.Visible = true;
            pnlWeather.Refresh();
            Stu_Client.Weather.WeatherWebServiceSoapClient w = new Stu_Client.Weather.WeatherWebServiceSoapClient("WeatherWebServiceSoap");
            string[] strWeather = new string[23];
            try
            {
                strWeather = w.getWeatherbyCityName("益阳");
            }
            catch (Exception)
            {
                lblWeather.Text = "获取天气信息失败...";
                lblTemperature.Text = "请连接网络后再重试";
                return;
                throw;
            }
            lblCity.Text = strWeather[0] + " " + strWeather[1];
            lblToday.Text = "更新时间：" + strWeather[4];
            string str = strWeather[6];
            if (str.EndsWith("晴"))
            { 
                picWeather.Image = Resources._1;
            }
            else if (str.EndsWith("多云"))
            {
                picWeather.Image = Resources._2;
            }
            else if (str.EndsWith("阴"))
            {
                picWeather.Image = Resources._3;
            }
            else if (str.EndsWith("阵雨"))
            {
                picWeather.Image = Resources._7;
            }
            else if (str.EndsWith("雨"))
            {
                picWeather.Image = Resources._5;
            }
            else
            {
                picWeather.Image = Resources._8;
            }
            lblWeather.Text = str;
            lblTemperature.Text = strWeather[5];
            lblWindy.Text = strWeather[7];
            lblTomorrow.Text = strWeather[13];
            lblTemperature1.Text = strWeather[12];
            lblWindy1.Text = strWeather[14];
            lblAfterTomorrow.Text = strWeather[18];
            lblTemperature2.Text = strWeather[17];
            lblWindy2.Text = strWeather[19];
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            pnlWeather.Visible = false;
            pnlMain.Visible = true;
        }

        private void btnEdu_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
            pnlEdu.Visible = true;
            webBrowserEdu.Navigate("http://www.hnu.edu.cn/");
        }

        private void btnEduBack_Click(object sender, EventArgs e)
        {
            pnlEdu.Visible = false;
            pnlMain.Visible = true;
        }

        private void btnFinancing_Click(object sender, EventArgs e)
        {
            this.Refresh();
            pnlFinancing.Visible = true;
            pnlMain.Visible = false;
            pnlFinancing.Refresh();
        }

        private void btnFinancingBack_Click(object sender, EventArgs e)
        {
            pnlFinancing.Visible = false;
            pnlMain.Visible = true;
        }
        int state = 0;
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboType.SelectedIndex)
            {
                case 0:
                    cboItem.Items.Clear();
                    cboItem.Items.Add("饭卡充钱");
                    cboItem.Items.Add("生活用品");
                    cboItem.Items.Add("衣服鞋子");
                    cboItem.Items.Add("书籍资料");
                    cboItem.Items.Add("网上购物");
                    cboItem.Items.Add("寝费班费");
                    cboItem.Items.Add("聚会活动");
                    cboItem.Items.Add("生日应酬");
                    cboItem.Items.Add("手机花费");
                    cboItem.Items.Add("电信宽带");
                    cboItem.Items.Add("其它");
                    state = -1;
                    break;
                case 1:
                    cboItem.Items.Clear();
                    cboItem.Items.Add("发生活费");
                    cboItem.Items.Add("兼职");
                    cboItem.Items.Add("勤工俭学");
                    cboItem.Items.Add("实习");
                    cboItem.Items.Add("其它");
                    state = 1;
                    break;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cboType.SelectedIndex >= 0 && cboItem.SelectedIndex >= 0)
            {
                int count,total;
                if(int.TryParse(txtCount.Text,out count))
                {
                    if (string.IsNullOrEmpty(txtRemark.Text))
                    {
                        MessageBox.Show("详细文本框里建议您输入相应备注事项！", "详细框为空", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    string strType = cboType.SelectedItem.ToString();
                    string strItem = cboItem.SelectedItem.ToString();
                    string strDetail = txtRemark.Text;
                    T_FinancingTableAdapter adapter = new T_FinancingTableAdapter();
                    total = Convert.ToInt32(adapter.GetLastBalance());
                    total = total + state * count;
                    adapter.Insert(DateTime.Now, strItem, strType, state * count, total, strDetail);
                    this.t_FinancingTableAdapter.Fill(this.dstClient.T_Financing);
                }
                else
                {
                    MessageBox.Show("金额文本框里请输入整数！", "数值类型错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择类型和项目！", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void btnNumQuery_Click(object sender, EventArgs e)
        {
            string strReturnMsg;
            string mobileCode=txtMobileCode.Text.Trim();
            if(!Regex.IsMatch(mobileCode,@"^[1][3-5]\d{9}$"))
            {
                MessageBox.Show("手机号码格式不正确！","输入有误",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            lblMobileCode.Visible = true;
            this.Refresh();
            Stu_Client.MobileCode.MobileCodeWSSoapClient m = new Stu_Client.MobileCode.MobileCodeWSSoapClient("MobileCodeWSSoap");
            try
            {
                strReturnMsg = m.getMobileCodeInfo(mobileCode, "");
            }
            catch (Exception)
            {
                lblMobileCode.Text = "请检查网络是否连接";
                return;
                throw;
            }
            lblMobileCode.Text = strReturnMsg.Remove(0, 12);
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
            pnlTool.Visible = true;
            pnlTool.Refresh();
        }

        private void btnToolBack_Click(object sender, EventArgs e)
        {
            pnlTool.Visible = false;
            pnlMain.Visible = true;
            webBrowserExpress.Navigate("http://www.kuaidi100.com/frame/app/index2.html");
        }

        private void txtMobileCode_Enter(object sender, EventArgs e)
        {
            txtMobileCode.Text = "";
            lblMobileCode.Visible = false;
        }

        private void picMsg_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
            pnlMsg.Visible = true;
        }

        private void btnMsgBack_Click(object sender, EventArgs e)
        {
            pnlMsg.Visible = false;
            pnlMain.Visible = true;
        }
    }
}
