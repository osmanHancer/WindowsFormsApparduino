namespace WindowsFormsApparduino
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.altimeter1 = new CS_WinForms_Ctrl_Altimeter.Altimeter();
            this.turnCoordinatorInstrument1 = new CS_WinForms_Ctrl_TurnCoordinatorInstrument.TurnCoordinatorInstrument();
            this.airSpeedIndicator1 = new CS_WinForms_Ctrl_Air_Speed_Indicator.AirSpeedIndicator();
            this.verticalSpeedIndicator1 = new CS_WinForm_Ctrl_Vertical_Speed_Indicator.VerticalSpeedIndicator();
            this.artifical_Horizon1 = new CS_WinForms_Ctrl_Artificial_Horizon.Artifical_Horizon();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kumanda-X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kumanda-Y:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(179, 44);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(179, 110);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(415, 44);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // timer2
            // 
            this.timer2.Interval = 5;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LawnGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(415, 110);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "Bağlan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(415, 167);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 10;
            this.button2.Text = "Ayrıl";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Kumanda-Z:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(179, 175);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 18;
            // 
            // altimeter1
            // 
            this.altimeter1.Altitude = 1080;
            this.altimeter1.Location = new System.Drawing.Point(487, 555);
            this.altimeter1.Margin = new System.Windows.Forms.Padding(5);
            this.altimeter1.Name = "altimeter1";
            this.altimeter1.Size = new System.Drawing.Size(221, 214);
            this.altimeter1.TabIndex = 16;
            // 
            // turnCoordinatorInstrument1
            // 
            this.turnCoordinatorInstrument1.Location = new System.Drawing.Point(14, 555);
            this.turnCoordinatorInstrument1.Margin = new System.Windows.Forms.Padding(5);
            this.turnCoordinatorInstrument1.Name = "turnCoordinatorInstrument1";
            this.turnCoordinatorInstrument1.Size = new System.Drawing.Size(221, 214);
            this.turnCoordinatorInstrument1.TabIndex = 14;
            this.turnCoordinatorInstrument1.TurnQuality = 9F;
            this.turnCoordinatorInstrument1.TurnRate = 15F;
            // 
            // airSpeedIndicator1
            // 
            this.airSpeedIndicator1.AirSpeed = 0;
            this.airSpeedIndicator1.Location = new System.Drawing.Point(878, 329);
            this.airSpeedIndicator1.Margin = new System.Windows.Forms.Padding(5);
            this.airSpeedIndicator1.Name = "airSpeedIndicator1";
            this.airSpeedIndicator1.Size = new System.Drawing.Size(217, 203);
            this.airSpeedIndicator1.TabIndex = 13;
            // 
            // verticalSpeedIndicator1
            // 
            this.verticalSpeedIndicator1.Location = new System.Drawing.Point(491, 329);
            this.verticalSpeedIndicator1.Margin = new System.Windows.Forms.Padding(5);
            this.verticalSpeedIndicator1.Name = "verticalSpeedIndicator1";
            this.verticalSpeedIndicator1.Size = new System.Drawing.Size(217, 216);
            this.verticalSpeedIndicator1.TabIndex = 12;
            this.verticalSpeedIndicator1.VerticalSpeed = 0;
            // 
            // artifical_Horizon1
            // 
            this.artifical_Horizon1.Location = new System.Drawing.Point(16, 329);
            this.artifical_Horizon1.Margin = new System.Windows.Forms.Padding(5);
            this.artifical_Horizon1.Name = "artifical_Horizon1";
            this.artifical_Horizon1.PitchAngle = -80D;
            this.artifical_Horizon1.RollAngle = 0D;
            this.artifical_Horizon1.Size = new System.Drawing.Size(221, 192);
            this.artifical_Horizon1.TabIndex = 11;
            this.artifical_Horizon1.Tag = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1563, 747);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.altimeter1);
            this.Controls.Add(this.turnCoordinatorInstrument1);
            this.Controls.Add(this.airSpeedIndicator1);
            this.Controls.Add(this.verticalSpeedIndicator1);
            this.Controls.Add(this.artifical_Horizon1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private CS_WinForms_Ctrl_Artificial_Horizon.Artifical_Horizon artifical_Horizon1;
        private CS_WinForm_Ctrl_Vertical_Speed_Indicator.VerticalSpeedIndicator verticalSpeedIndicator1;
        private CS_WinForms_Ctrl_Air_Speed_Indicator.AirSpeedIndicator airSpeedIndicator1;
        private CS_WinForms_Ctrl_TurnCoordinatorInstrument.TurnCoordinatorInstrument turnCoordinatorInstrument1;
        private CS_WinForms_Ctrl_Altimeter.Altimeter altimeter1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
    }
}

