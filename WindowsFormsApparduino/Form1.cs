using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using MySql.Data.MySqlClient;
using SharpDX.DirectInput;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApparduino
{
    public partial class Form1 : Form
    {

        DirectInput input = new DirectInput();
        Joystick stick;
        string data_row = "";
        byte speed=0;
        byte row_1 = 0;
        byte row_2 = 0;
        byte row_3 = 0;
        byte row_4 = 0;

        public Form1()
        {
            InitializeComponent();
            foreach (DeviceInstance device in input.GetDevices
                  (DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly))
            {
                stick = new Joystick(input, device.InstanceGuid);

                // Instantiate the joystick

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", stick);

                // Query all suported ForceFeedback effects
                var allEffects = stick.GetEffects();
                foreach (var effectInfo in allEffects)
                    Console.WriteLine("Effect available {0}", effectInfo.Name);

                // Set BufferSize in order to use buffered data.
                stick.Properties.BufferSize = 128;

                // Acquire the joystick
                stick.Acquire();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();  //Seri portları diziye ekleme
            foreach (string port in ports)
                comboBox1.Items.Add(port); //Seri portları comBox1' ekleme

            

        }
      

        private void timer1_Tick_1(object sender, EventArgs e)
        {

            stick.Poll();
            var datas = stick.GetBufferedData();
           foreach (var state in datas)
            {
                if (state.Offset == JoystickOffset.X)
                {
                    var data = state.Value.ToString();
                    textBox1.Text = data.ToString();
                    this.row_3 = (byte)(map(float.Parse(data), 0, 65353, 0, 180));
                    this.row_4 = (byte)(map(float.Parse(data), 0, 65353, 0, 180));
                 
                }
                if (state.Offset == JoystickOffset.Y)
                {
                    var data = state.Value.ToString();
                    textBox2.Text = data.ToString();
                    this.row_1 = (byte)(map(float.Parse(data), 0, 65353, 0, 180));
                    this.row_2 = (byte)(map(float.Parse(data), 0, 65353, 0, 180));

                }
                if (state.Offset == JoystickOffset.Z)
                {
                   var data = state.Value.ToString();
                    textBox3.Text = data.ToString();
                    this.speed = (byte)(map(float.Parse(data), 0, 65353, 0, 255));


                }
               

            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            serialPort1.PortName = comboBox1.SelectedItem.ToString(); //comboBox1'de seçili olan portu port ismine ata
             //Seri portu aç
           
           


        }
        float map(float val, float iMin, float iMax, float oMin, float oMax)
        {

            return (val - iMin) * (oMax - oMin) / (iMax - iMin) + oMin;
        }
         double Mapd(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            serialPort1.Write(new byte[] { speed,row_1,row_2,row_3,row_4 }, 0, 5);


        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data_row = serialPort1.ReadLine();                      //Veriyi al
            this.Invoke(new EventHandler(displayData_event));
        }
        private void displayData_event(object sender, EventArgs e)
        {
            
            if (data_row.Length > 2)
            {

                string x = data_row.Split(',')[0];
                string y = data_row.Split(',')[1];

                float xFloat;
                float yFloat;
                float.TryParse(x, out xFloat);
                float.TryParse(y, out yFloat);



                Console.WriteLine("xf:  " + xFloat);
                yFloat = map(yFloat/100, -10, 10, -80, 80);
                turnCoordinatorInstrument1.TurnRate = xFloat / 100;
                artifical_Horizon1.PitchAngle = yFloat;




                /*
                textBox1.Text = x;
                textBox2.Text = y;

                /* string x = data_row.Substring(18, 3);
                 string y = data_row.Substring(21,3);
                 string z = data_row.Substring(24, 3);
                 textBox5.Text = x;
                 textBox6.Text = y;
                 textBox7.Text = z;
                 SaveData(lat, lon);
                */

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            timer1.Start();
            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            timer1.Stop();
            timer2.Stop();
        }



        public void SaveData(string lat, string lon)
        {
            Mysql database = new Mysql();

            if (database.connect_db())
            {
                
                string query = "INSERT INTO gps (Latitude, Longitude) VALUES (@value1, @value2)";
                MySqlCommand mySqlCommand = new MySqlCommand(query, database.MySqlConnection);

                // Parametreleri ekliyoruz
                mySqlCommand.Parameters.AddWithValue("@value1", lat);
                mySqlCommand.Parameters.AddWithValue("@value2", lon);

                mySqlCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("database error");
            }
        }


    }
}
