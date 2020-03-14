using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Stu_Client
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public void ClearScreen()
        {
            txtStuID.Visible = false;
            txtPassword.Visible = false;
            lblShutdown.Visible = true;
            btnExit.Visible = false;
            btnLogin.Visible = false;
        }
        public void ReturnScreen()
        {
            txtStuID.Visible = true;
            txtPassword.Visible = true;
            lblShutdown.Visible = false;
            btnExit.Visible = true;
        }
        public void ShowMsg(string msg)
        {
            lblShutdown.Text = msg;
        }
        frmContent frmMain;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            frmMain = new frmContent(this);
        }
        
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length > 5)
            {
                btnLogin.Visible = true;
            }
            else
            {
                btnLogin.Visible = false;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lblErrorMsg.Visible = false;
            txtPassword.PasswordChar = '●';
            txtPassword.Text = "";
            txtPassword.ForeColor = Color.DarkGray;
        }

        private void txtStuID_Enter(object sender, EventArgs e)
        {
            lblIDErrorMsg.Visible = false;
            txtStuID.Text = "";
            txtStuID.ForeColor = Color.FromArgb(128, 128, 255);
        }

        private void txtStuID_Leave(object sender, EventArgs e)
        {
            if (txtStuID.Text == "")
            {
                txtStuID.ForeColor = Color.Gray;
                txtStuID.Text = "学号";
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.ForeColor = Color.Gray;
                txtPassword.PasswordChar = (char)0;
                txtPassword.Text = "密码";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //int rec = -1;
            //string stuID = txtStuID.Text.Trim();
            //if(!Regex.IsMatch(stuID,@"^(\d{7})-(\d{2}$)"))
            //{
            //    lblIDErrorMsg.Text="学号格式不正确";
            //    lblIDErrorMsg.Visible=true;
            //    return;
            //}
            this.ClearScreen();
            this.ShowMsg("正在登录中...");
            this.Refresh();
            ////用套接字发送学号和密码到服务器；
            ////得到服务器返回值；
            //if (txtStuID.Text == "0906401-23")
            //{
            //    rec = 2;
            //    if (txtPassword.Text == "611109")
            //    {
            //        rec = 0;
            //    }
            //}
            //else
            //{
            //    rec = 1;
            //}
            
            //switch (rec)
            //{
            //    case 0://登陆成功
            this.ShowMsg("欢迎您!邓宇 正在加载个人配置信息...");
                    frmMain.Show();
                    this.Visible = false;
                //    break;
                //case 1://学号不存在
                //    this.ReturnScreen();
                //    lblIDErrorMsg.Text = "该学号不存在";
                //    lblIDErrorMsg.Visible = true;
                //    break;
                //case 2://密码错误；
                //    this.ReturnScreen();
                //    lblErrorMsg.Text = "密码错误";
                //    lblErrorMsg.Visible = true;
                //    break;
                //default://登录连接超时;
                //    this.ReturnScreen();
                //    MessageBox.Show("网络连接超时，请重新登录！", "连接超时", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    break;
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确实现在就要退出吗？", "iCloud 2012", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                this.Close();
                Application.Exit();
            }
            return;
        }
    }
}
