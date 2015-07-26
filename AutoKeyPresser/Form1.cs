using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoKeyPresser
{
    public partial class Form1 : Form
    {
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private string keySource;
        public Form1()
        {
            InitializeComponent();
            simpleRegistKey(Keys.F1);
            simpleRegistKey(Keys.F2);
            simpleRegistKey(Keys.F3);
            simpleRegistKey(Keys.F4);
        }

        private void simpleRegistKey(Keys keys)
        {
            RegisterHotKey(this.Handle, (int)keys, 0, (int)keys);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                switch ((Keys)m.WParam)
                {

                    case Keys.F1:
                        SendKey("{e}");
                        break;
                    case Keys.F2:
                        SendKey("^",100);
                        break;
                    case Keys.F3:
                        SendKey(textBox_Customkeys.Text);
                        break;
                    case Keys.F4:
                        keySource = "";
                        timer1.Enabled = false;
                        break;
                    //case Keys.F1:
                    //    keySource = "";
                    //    isRunning = !isRunning;
                    //    timer1.Enabled = isRunning;
                    //    break;
                    //case Keys.E:
                    //    if (!isRunning)
                    //    {
                    //        SendKeys.Send("{e}");
                    //    }
                    //   SendKey("{e}");
                    //    break;
                    //case Keys.F5:
                    //    if (!isRunning)
                    //    {
                    //        SendKeys.Send("^");
                    //    }
                    //    SendKey("^", 100);
                    //    break;
                    //case Keys.F2:
                    //    SendKey(textBox_Customkeys.Text, 100);
                    //    break;
                }
            }
            base.WndProc(ref m);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (keySource != "")
            {
                SendKeys.Send(keySource);
               // SendKeys.Send("{Enter}");
            }
        }
        private void SendKey(string key, int speed=1000)
        {
            keySource = key;
            timer1.Enabled = true;
            timer1.Interval = speed * Convert.ToInt32(textBox_Interval.Text);
        }
    }
}
