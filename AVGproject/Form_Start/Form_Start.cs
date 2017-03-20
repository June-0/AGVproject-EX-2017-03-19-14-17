using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 待测内容：
// 1 超声波与激光雷达测数据是否有冲突
// 2 直线拟合效果
// 3 控制算法的控制输出是否过大
// 4 控制算法的PID系数
// 5 控制算法下小车能否正常运行
// 6 控制效果

namespace AGVproject
{
    public partial class Form_Start : Form
    {
        public Form_Start()
        {
            InitializeComponent();

            this.textBox1.Text = "COM5";
            this.textBox2.Text = "115200";

            this.xSpeed.Text = "0";
            this.ySpeed.Text = "0";
            this.aSpeed.Text = "0";

            this.urgPortName.Text = "COM7";
            this.urgBaudRate.Text = "115200";
        }

        private static Class.TH_Process TH_process = new Class.TH_Process();
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class.TH_Process.TH_data.control_PortName = this.textBox1.Text;
            Class.TH_Process.TH_data.control_BaudRate = this.textBox2.Text;

            Class.TH_Process.TH_command.Open(Class.TH_Process.TH_data.control_PortName, Class.TH_Process.TH_data.control_BaudRate);

            Class.TH_Process.TH_command.MeasureUltraSonic_0x86();

            Class.TH_SendCommand.TH_DATA data = new Class.TH_SendCommand.TH_DATA();

            string[] show = new string[11];

            show[0] = "Head_L_X: " + Class.TH_SendCommand.TH_data.Head_L_X;
            show[1] = "Head_L_Y: " + Class.TH_SendCommand.TH_data.Head_L_Y;
            show[2] = "Head_R_X: " + Class.TH_SendCommand.TH_data.Head_R_X;
            show[3] = "Head_R_Y: " + Class.TH_SendCommand.TH_data.Head_R_Y;
            show[4] = "Tail_L_X: " + Class.TH_SendCommand.TH_data.Tail_L_X;
            show[5] = "Tail_L_Y: " + Class.TH_SendCommand.TH_data.Tail_L_Y;
            show[6] = "Tail_R_X: " + Class.TH_SendCommand.TH_data.Tail_R_X;
            show[7] = "Tail_R_Y: " + Class.TH_SendCommand.TH_data.Tail_R_Y;

            this.textBox3.Lines = show;
        }

        private void xSpeed_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void ySpeed_TextChanged(object sender, EventArgs e)
        {

        }

        private void aSpeed_TextChanged(object sender, EventArgs e)
        {

        }

        private void xSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.ySpeed.Text = "0";
                this.aSpeed.Text = "0";

                int SpeedX = 0;
                try { SpeedX = int.Parse(this.xSpeed.Text); } catch { MessageBox.Show("Input Error !"); return; }

                Class.TH_Process.TH_command.ReStart_TH_control();
                Class.TH_Process.TH_command.AGV_MoveControl_0x70(SpeedX, 0, 0);
            }
        }

        private void ySpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                //agvMove.Abort();

                this.xSpeed.Text = "0";
                this.aSpeed.Text = "0";

                int SpeedY = 0;
                try { SpeedY = int.Parse(this.ySpeed.Text); } catch { MessageBox.Show("Input Error !"); return; }

                Class.TH_Process.TH_command.AGV_MoveControl_0x70(0, SpeedY, 0);
            }
        }

        private void aSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.xSpeed.Text = "0";
                this.ySpeed.Text = "0";

                int SpeedA = 0;
                try { SpeedA = int.Parse(this.aSpeed.Text); } catch { MessageBox.Show("Input Error !"); return; }

                Class.TH_Process.TH_command.AGV_MoveControl_0x70(0, 0, SpeedA);
            }
        }

        private void urgBaudRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void currKeyPos_Click(object sender, EventArgs e)
        {
            Class.TH_Process.correctPos.GetCurrentPoint(Class.TH_Process.TH_command);
        }

        private void Form_Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            Class.TH_Process.TH_data.TH_cmd_abort = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
        }

        private void Form_Start_Load(object sender, EventArgs e)
        {
            
        }
    }
}
