using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AGVproject.Class
{
    class TH_SendCommand
    {
        ////////////////////////////////////////// public attribute ////////////////////////////////////////////////

        public bool IsOpen { get { return controlport != null && controlport.IsOpen; } }
        public bool IsClose { get { return controlport == null || !controlport.IsOpen; } }

        public static TH_DATA TH_data;
        public struct TH_DATA
        {
            public bool IsReceiving_0x86;
            public bool IsReceiving_0x70;
            public bool IsReceiving_0x84;

            public bool TH_cmd_abort;
            public bool TH_runing { get { return TH_control.ThreadState == System.Threading.ThreadState.Running; } }
            
            public int Head_L_X;
            public int Head_L_Y;
            public int Head_R_X;
            public int Head_R_Y;
            public int Tail_L_X;
            public int Tail_L_Y;
            public int Tail_R_X;
            public int Tail_R_Y;

            public uint posX;
            public uint posY;
            public double posA;

            public int TimeForControl;
        }

        ////////////////////////////////////////// private attribute ////////////////////////////////////////////////

        private static System.Threading.Thread TH_control = new System.Threading.Thread(SendCommand_100ms);

        private static SerialPort controlport;

        private static byte[] receData = new byte[40];
        private static byte[] sentData;
        private static int receLength;

        private static PORT_CONFIG portConfig;
        private struct PORT_CONFIG
        {
            public bool sent_0x86;
            public bool sent_0x84;
            public bool sent_0x70;

            public bool IsReading;
            public bool IsClosing;

            public bool IsSettingCommand;
        }

        ////////////////////////////////////////// public method ////////////////////////////////////////////////

        public bool Open(string portName, string baudRate)
        {
            if (IsOpen) { return true; }

            try
            {
                // 初始化线程
                Initial_TH_SendCommand();

                // 打开串口
                controlport = new SerialPort(portName, int.Parse(baudRate));
                controlport.DataReceived -= portDataReceived;
                controlport.DataReceived += portDataReceived;
                controlport.Open();

                // 打开线程
                TH_data.TH_cmd_abort = true;
                while (TH_control != null && TH_control.ThreadState == System.Threading.ThreadState.Running) ;
                TH_data.TH_cmd_abort = false;
                TH_control.Start();

                return true;
            }
            catch { return false; }
        }
        public bool Open()
        {
            if (IsOpen) { return true; }

            try
            {
                // 初始化线程
                Initial_TH_SendCommand();

                // 打开串口
                controlport.DataReceived -= portDataReceived;
                controlport.DataReceived += portDataReceived;
                controlport.Open();

                // 打开线程
                TH_data.TH_cmd_abort = true;
                while (TH_control != null && TH_control.ThreadState == System.Threading.ThreadState.Running) ;
                TH_data.TH_cmd_abort = false;
                TH_control.Start();

                return true;
            }
            catch { return false; }
        }
        public bool Close()
        {
            if (!controlport.IsOpen) { return true; }
            portConfig.IsClosing = true;

            // 等待读取完毕
            while (portConfig.IsReading) { System.Windows.Forms.Application.DoEvents(); }

            // 关闭
            try
            {
                controlport.Close();
                portConfig.IsClosing = false;
                return true;
            }
            catch
            {
                portConfig.IsClosing = false;
                return false;
            }
        }

        public void ResetCommand_speed0()
        {
            // 串口没开
            if (IsClose) { return; }

            // 等待所有命令执行完
            WaitFor_FinishPreviousCommand();

            // 初始化状态
            portConfig.sent_0x86 = false;
            portConfig.sent_0x84 = false;
            portConfig.sent_0x70 = true;

            TH_data.IsReceiving_0x70 = true;

            // 填充命令
            byte[] command = new byte[11];
            command[0] = 0xf1;
            command[1] = 0x70;
            Fill_CheckBytes(ref command);

            portConfig.IsSettingCommand = true;
            receLength = 12;
            sentData = command;
            portConfig.IsSettingCommand = false;
        }
        public void ReStart_TH_control()
        {
            // 已经开启
            if (TH_control.ThreadState == System.Threading.ThreadState.Running) { return; }

            // 重置命令
            ResetCommand_speed0();

            // 重新开启
            TH_control.Start();
        }

        public bool MessureMoveDistance_0x84()
        {
            // 串口没开
            if (IsClose) { return false; }
            
            // 关闭线程
            TH_data.TH_cmd_abort = true;
            while (TH_control.ThreadState == System.Threading.ThreadState.Running) ;

            // 等待所有命令执行完
            WaitFor_FinishPreviousCommand();

            // 初始化状态
            portConfig.sent_0x86 = false;
            portConfig.sent_0x84 = true;
            portConfig.sent_0x70 = false;

            TH_data.IsReceiving_0x84 = true;

            // 填充命令
            byte[] command = new byte[4] { 0xf1, 0x84, 0x00, 0x00 };
            Fill_CheckBytes(ref command);
            receLength = 12;
            sentData = command;
            
            // 尝试发送命令
            try
            {
                controlport.ReceivedBytesThreshold = receLength;
                controlport.DiscardOutBuffer();
                controlport.Write(sentData, 0, sentData.Length);
            }
            catch
            {
                return false;
            }

            // 等待接收数据
            while (TH_data.IsReceiving_0x86) ;
            return true;
        }
        public bool MeasureUltraSonic_0x86()
        {
            // 串口没开
            if (IsClose) { return false; }

            // 关闭线程
            TH_data.TH_cmd_abort = true;
            while (TH_control.ThreadState == System.Threading.ThreadState.Running) ;

            // 等待所有命令执行完
            WaitFor_FinishPreviousCommand();

            // 初始化状态
            portConfig.sent_0x86 = true;
            portConfig.sent_0x84 = false;
            portConfig.sent_0x70 = false;

            TH_data.IsReceiving_0x86 = true;

            // 填充命令
            byte[] command = new byte[4] { 0xf1, 0x86, 0x00, 0x00 };
            Fill_CheckBytes(ref command);
            receLength = 12;
            sentData = command;

            // 尝试发送命令
            try
            {
                controlport.ReceivedBytesThreshold = receLength;
                controlport.DiscardOutBuffer();
                controlport.Write(command, 0, command.Length);
            }
            catch
            {
                return false;
            }

            // 等待接收数据
            while (TH_data.IsReceiving_0x86) ;
            return true;
        }
        public bool AGV_MoveControl_0x70(int xSpeed, int ySpeed, int aSpeed)
        {
            // 串口没开
            if (IsClose) { return false; }

            // 限幅
            aSpeed = (int)Math.Round(aSpeed * 3.14159 / 180);
            if (xSpeed > 800) { xSpeed = 800; }
            if (xSpeed < -800) { xSpeed = -800; }
            if (ySpeed > 800) { ySpeed = 800; }
            if (ySpeed < -800) { ySpeed = -800; }
            if (aSpeed > 127) { aSpeed = 127; }
            if (aSpeed < -128) { aSpeed = -128; }

            // 把三轴速度转换为可输出命令
            if (xSpeed != 0 && (ySpeed != 0 || aSpeed != 0)) { return false; }
            if (ySpeed != 0 && (xSpeed != 0 || aSpeed != 0)) { return false; }
            if (aSpeed != 0 && (xSpeed != 0 || ySpeed != 0)) { return false; }

            int speed = 0, direction = 0, rotate = aSpeed;

            if (xSpeed > 0) { speed = xSpeed; direction = 90; rotate = 0; }
            if (xSpeed < 0) { speed = -xSpeed; direction = 270; rotate = 0; }
            if (ySpeed > 0) { speed = ySpeed; direction = 0; rotate = 0; }
            if (ySpeed < 0) { speed = -ySpeed; direction = 180; rotate = 0; }

            // 等待所有命令执行完
            WaitFor_FinishPreviousCommand();

            // 初始化状态
            portConfig.sent_0x86 = false;
            portConfig.sent_0x84 = false;
            portConfig.sent_0x70 = true;

            TH_data.IsReceiving_0x70 = true;

            // 填充命令
            byte[] command = new byte[11];
            command[0] = 0xf1;
            command[1] = 0x70;
            command[2] = (byte)(speed >> 8);
            command[3] = (byte)(speed);
            command[4] = (byte)(direction >> 8);
            command[5] = (byte)(direction);
            command[6] = (byte)(rotate);
            command[7] = 0x00;
            Fill_CheckBytes(ref command);

            portConfig.IsSettingCommand = true;
            receLength = 12;
            sentData = command;
            portConfig.IsSettingCommand = false;
            return true;
        }
        public bool AGV_MoveControl_0x70(int xSpeed, int ySpeed, int aSpeed, int Time)
        {
            // 串口没开
            if (IsClose) { return false; }

            // 关闭线程
            TH_data.TH_cmd_abort = true;
            while (TH_control.ThreadState == System.Threading.ThreadState.Running) ;

            // 限幅
            aSpeed = (int)Math.Round(aSpeed * 3.14159 / 180);
            if (xSpeed > 800) { xSpeed = 800; }
            if (xSpeed < -800) { xSpeed = -800; }
            if (ySpeed > 800) { ySpeed = 800; }
            if (ySpeed < -800) { ySpeed = -800; }
            if (aSpeed > 127) { aSpeed = 127; }
            if (aSpeed < -128) { aSpeed = -128; }

            // 把三轴速度转换为可输出命令
            if (xSpeed != 0 && (ySpeed != 0 || aSpeed != 0)) { return false; }
            if (ySpeed != 0 && (xSpeed != 0 || aSpeed != 0)) { return false; }
            if (aSpeed != 0 && (xSpeed != 0 || ySpeed != 0)) { return false; }

            int speed = 0, direction = 0, rotate = aSpeed;

            if (xSpeed > 0) { speed = xSpeed; direction = 90; rotate = 0; }
            if (xSpeed < 0) { speed = -xSpeed; direction = 270; rotate = 0; }
            if (ySpeed > 0) { speed = ySpeed; direction = 0; rotate = 0; }
            if (ySpeed < 0) { speed = -ySpeed; direction = 180; rotate = 0; }

            // 等待所有命令执行完
            WaitFor_FinishPreviousCommand();

            // 初始化状态
            portConfig.sent_0x86 = false;
            portConfig.sent_0x84 = false;
            portConfig.sent_0x70 = true;

            TH_data.IsReceiving_0x70 = true;

            // 填充命令
            byte[] command = new byte[11];
            command[0] = 0xf1;
            command[1] = 0x70;
            command[2] = (byte)(speed >> 8);
            command[3] = (byte)(speed);
            command[4] = (byte)(direction >> 8);
            command[5] = (byte)(direction);
            command[6] = (byte)(rotate);
            command[7] = 0x00;
            Fill_CheckBytes(ref command);
            
            receLength = 12;
            sentData = command;
            
            // 尝试发送命令
            try
            {
                controlport.ReceivedBytesThreshold = receLength;
                controlport.DiscardOutBuffer();
                controlport.Write(command, 0, command.Length);
            }
            catch
            {
                return false;
            }

            // 结束，不管返回。
            System.Threading.Thread.Sleep(Time);
            return true;
        }

        ////////////////////////////////////////// private method ////////////////////////////////////////////////

        private static void SendCommand_100ms()
        {
            while (true)
            {
                // 串口已关闭
                if (controlport == null || !controlport.IsOpen) { TH_control.Abort(); TH_data.TH_cmd_abort = false; return; }

                // 外部要求关闭线程
                if (TH_data.TH_cmd_abort) { TH_control.Abort(); TH_data.TH_cmd_abort = false; return; }

                // 等待写入指令完毕，发送指令
                // 写入失败，再次写入。
                while (portConfig.IsSettingCommand) ;
                try
                {
                    controlport.ReceivedBytesThreshold = receLength;
                    controlport.DiscardOutBuffer();
                    controlport.Write(sentData, 0, sentData.Length);
                }
                catch { continue; }

                // 执行命令
                System.Threading.Thread.Sleep(TH_data.TimeForControl);

                // 发送完毕，清除命令
                byte[] command = new byte[11];
                command[0] = 0xf1;
                command[1] = 0x70;
                Fill_CheckBytes(ref command);

                portConfig.IsSettingCommand = true;
                receLength = 12;
                sentData = command;
                portConfig.IsSettingCommand = false;
            }
        }

        private void Initial_TH_SendCommand()
        {
            portConfig.sent_0x70 = false;
            portConfig.sent_0x84 = false;
            portConfig.sent_0x86 = false;
            
            portConfig.IsReading = false;
            portConfig.IsClosing = false;

            portConfig.IsSettingCommand = false;
            
            TH_data.IsReceiving_0x86 = false;
            TH_data.IsReceiving_0x84 = false;
            TH_data.IsReceiving_0x70 = false;
            TH_data.TH_cmd_abort = false;
            TH_data.TimeForControl = 100;

            ResetCommand_speed0();
        }
        private void portDataReceived(object sender, EventArgs e)
        {
            // 正在关闭
            if (portConfig.IsClosing) { return; }

            // 正在读取
            portConfig.IsReading = true;
            try
            {
                controlport.Read(receData, 0, Math.Min(receLength, controlport.ReceivedBytesThreshold));
            }
            catch
            {
                portConfig.IsReading = false; return;
            }
            portConfig.IsReading = false;

            #region 预处理行进控制返回
            if (portConfig.sent_0x70)
            {
                if (!True_ReceiveData()) { return; }
                TH_data.IsReceiving_0x70 = false;
                return;
            }
            #endregion

            #region 预处理超声波数据
            if (portConfig.sent_0x86)
            {
                if (!True_ReceiveData()) { return; }

                // 填充数据
                TH_data.Head_L_Y = receData[2];
                TH_data.Head_L_X = receData[3];
                TH_data.Head_R_X = receData[4];
                TH_data.Head_R_Y = receData[5];
                TH_data.Tail_R_Y = receData[6];
                TH_data.Tail_R_X = receData[7];
                TH_data.Tail_L_X = receData[8];
                TH_data.Tail_L_Y = receData[9];
                TH_data.IsReceiving_0x86 = false;
                
                return;
            }
            #endregion

            #region 预处理里程数据
            if (portConfig.sent_0x84)
            {
                if (!True_ReceiveData()) { return; }

                uint byte1 = receData[2];
                uint byte2 = receData[3];
                uint byte3 = receData[4];
                uint byte4 = receData[5];
                TH_data.posY = byte1 << 24 | byte2 << 16 | byte3 << 8 | byte4;

                byte1 = receData[6];
                byte2 = receData[7];
                byte3 = receData[8];
                byte4 = receData[9];
                TH_data.posX = byte1 << 24 | byte2 << 16 | byte3 << 8 | byte4;

                TH_data.IsReceiving_0x84 = false;
                return;
            }
            #endregion
        }

        private static void Fill_CheckBytes(ref byte[] command)
        {
            uint sumCommand = 0;
            for (int i = 0; i < command.Length - 2; i++) { sumCommand += command[i]; }

            sumCommand = (sumCommand >> 16) + (sumCommand & 0x0000ffff);

            command[command.Length - 2] = (byte)(sumCommand >> 8);
            command[command.Length - 1] = (byte)(sumCommand & 0x000000ff);
        }
        private static bool True_ReceiveData()
        {
            if (receData.Length < 4) { return false; }
            if (sentData.Length < 4) { return false; }

            if (receData[0] != sentData[0]) { return false; }
            if (receData[1] != sentData[1]) { return false; }

            uint sumReceived = 0;
            for (int i = 0; i < receLength - 2; i++) { sumReceived += receData[i]; }
            sumReceived = (sumReceived >> 16) + (sumReceived & 0x0000ffff);

            byte checkH = (byte)(sumReceived >> 8);
            byte checkL = (byte)(sumReceived & 0x00ff);

            if (receData[receLength - 2] != checkH) { return false; }
            if (receData[receLength - 1] != checkL) { return false; }

            return true;
        }

        private void WaitFor_FinishPreviousCommand()
        {
            while (TH_data.IsReceiving_0x84) ;
            while (TH_data.IsReceiving_0x86) ;
            while (TH_data.IsReceiving_0x70) ;
        }
    }
}
