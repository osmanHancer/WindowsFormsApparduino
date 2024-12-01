/*****************************************************************************/
/* Project  : Air Speed Indicator User Control                               */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : Air speed instrument control                                   */
/* Creation : 22/06/2008                                                     */
/* Author   : Original code by Guillaume CHOUTEAU                            */
/* History  : Modified to user control format by Stephen Abbadessa           */
/*            University of Maine 4/24/2020                                  */
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_WinForms_Ctrl_Air_Speed_Indicator
{
    public partial class AirSpeedIndicator: UserControl
    {

    public Bitmap bmpCadran = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\AirSpeedIndicator_Background.bmp");
    public Bitmap bmpNeedle = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\AirSpeedNeedle.bmp");

    /* Set up a dummy holding variable */

    private int _Airspeed = 0;

    /* Set up the exchanged parameters for the control */

    public int AirSpeed
    {
        get { return _Airspeed; }
        set
        {
            if (_Airspeed == value) return;
            _Airspeed = value;
            if (OnVariableChange != null)
                OnVariableChange(_Airspeed);
            Invalidate();
        }
    }

    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;

        /* Program entry point */
   
        public AirSpeedIndicator()
        {
        InitializeComponent();

        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
          ControlStyles.AllPaintingInWmPaint, true);

        Invalidate();  //Cause a screen redraw that fires Paint

        }
              
        private void AirSpeedIndicator_Paint_1(object sender, PaintEventArgs pe)
        {
            // Pre Display computings
            Point ptRotation = new Point(150, 150);
            Point ptimgNeedle = new Point(136, 39);

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpNeedle.MakeTransparent(Color.Yellow);

            double alphaNeedle = InterpolPhyToAngle(AirSpeed, 0, 800, 180, 468);

            float scale = (float)this.Width / bmpCadran.Width;

            // diplay mask
            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display small needle
            RotateImage(pe, bmpNeedle, alphaNeedle, ptimgNeedle, ptRotation, scale);
        }

        protected float InterpolPhyToAngle(float phyVal, float minPhy, float maxPhy, float minAngle, float maxAngle)
        {
            float a;
            float b;
            float y;
            float x;

            if (phyVal < minPhy)
            {
                return (float)(minAngle * Math.PI / 180);
            }
            else if (phyVal > maxPhy)
            {
                return (float)(maxAngle * Math.PI / 180);
            }
            else
            {

                x = phyVal;
                a = (maxAngle - minAngle) / (maxPhy - minPhy);
                b = (float)(0.5 * (maxAngle + minAngle - a * (maxPhy + minPhy)));
                y = a * x + b;

                return (float)(y * Math.PI / 180);
            }
        }


        protected void RotateImage(PaintEventArgs pe, Image img, Double alpha, Point ptImg, Point ptRot, float scaleFactor)
        {
            double beta = 0; 	// Angle between the Horizontal line and the line (Left upper corner - Rotation point)
            double d = 0;		// Distance between Left upper corner and Rotation point)		
            float deltaX = 0;	// X componant of the corrected translation
            float deltaY = 0;   // Y componant of the corrected translation

            // Compute the correction translation coeff
            if (ptImg != ptRot)
            {
                //
                if (ptRot.X != 0)
                {
                    beta = Math.Atan((double)ptRot.Y / (double)ptRot.X);
                }

                d = Math.Sqrt((ptRot.X * ptRot.X) + (ptRot.Y * ptRot.Y));

                // Computed offset
                deltaX = (float)(d * (Math.Cos(alpha - beta) - Math.Cos(alpha) * Math.Cos(alpha + beta) - Math.Sin(alpha) * Math.Sin(alpha + beta)));
                deltaY = (float)(d * (Math.Sin(beta - alpha) + Math.Sin(alpha) * Math.Cos(alpha + beta) - Math.Cos(alpha) * Math.Sin(alpha + beta)));
            }

            // Rotate image support
            pe.Graphics.RotateTransform((float)(alpha * 180 / Math.PI));

            // Dispay image
            pe.Graphics.DrawImage(img, (ptImg.X + deltaX) * scaleFactor, (ptImg.Y + deltaY) * scaleFactor, img.Width * scaleFactor, img.Height * scaleFactor);

            // Put image support as found
            pe.Graphics.RotateTransform((float)(-alpha * 180 / Math.PI));

        }

        private void AirSpeedIndicator_Load(object sender, EventArgs e)
        {

        }
    }
}
