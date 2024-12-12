using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Example1
{
    public partial class FormMain : Form
    {
        private VideoCapture _capture = null;
        private Mat _frame;
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                bool canCapture = _capture.Retrieve(_frame, 0);
                if (canCapture)
                {
                    imageBox1.Image = _frame;
                }
            }
          
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (_capture != null)
                {
                    _capture.Dispose();
                }

                _capture = new VideoCapture(0);

                // ตรวจสอบการเชื่อมต่อกับกล้อง
                if (!_capture.IsOpened)
                {
                    MessageBox.Show("Cannot access the camera. Please check your device.");

                    // เปลี่ยนพื้นหลังเป็นสีแดง (แค่สี ไม่มีข้อความ)
                    textBox2.BackColor = Color.Red;
                }
                else
                {
                    // เชื่อมต่อกล้องได้
                    _capture.ImageGrabbed += ProcessFrame; // เชื่อมต่อกับ event ที่ประมวลผลเฟรม
                    _frame = new Mat(); // สร้าง Mat สำหรับเก็บภาพจากกล้อง

                    // เริ่มต้นการจับภาพ
                    _capture.Start();

                    // แสดงข้อความ "Connected" และสีพื้นหลังเป็นเขียว
                    c.BackColor = Color.Green;
                    c.Text = "Connected";
                }
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาดหากมี
                MessageBox.Show($"Error: {ex.Message}");

                // เปลี่ยนพื้นหลังเป็นสีแดง (แค่สี ไม่มีข้อความ)
                textBox2.BackColor = Color.Red;
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (_capture != null) 
            {
                _capture.Start();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // ตรวจสอบว่า _capture กล้องเปิดอยู่หรือไม่
                if (_capture != null && _capture.IsOpened)
                {
                    // หยุดการจับภาพจากกล้อง
                    _capture.Stop(); // หยุดการจับภาพ
                    _capture.Dispose(); // ปล่อยทรัพยากร
                    _capture = null; // รีเซ็ต _capture

                    // ล้างภาพใน imageBox1
                    imageBox1.Image = null;

                    // เปลี่ยนข้อความใน TextBox และปุ่ม
                    textBox2.BackColor = Color.Red; // พื้นหลังเป็นสีแดง
                    textBox2.Text = "Disconnected"; // แสดงข้อความ "Disconnected"

                    c.BackColor = Color.Gray; // พื้นหลังปุ่มเป็นสีเทา
                    c.Text = "Not Connected"; // แสดงข้อความ "Not Connected"
                }
                else
                {
                    // แจ้งเตือนหากกล้องไม่ได้เชื่อมต่อ
                    MessageBox.Show("Camera is not connected.");
                }
            }
            catch (Exception ex)
            {
                // แสดงข้อผิดพลาด
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Pause();
            }
        }

        private void buttonFlipHor_Click(object sender, EventArgs e)
        {
            if (_capture != null)
                _capture.FlipHorizontal = !_capture.FlipHorizontal;
        }

        private void buttonFlipVer_Click(object sender, EventArgs e)
        {
            if (_capture != null)
                _capture.FlipVertical = !_capture.FlipVertical;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }



    }
}
