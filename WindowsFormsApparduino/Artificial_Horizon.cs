/*****************************************************************************/
/* Project  : Artificial Horizon User Control                                */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The attitude indicator instrument control                      */
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

namespace CS_WinForms_Ctrl_Artificial_Horizon
{
    public partial class Artifical_Horizon: UserControl
    {

        // Images
        Bitmap bmpCadran = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\Horizon_Background.bmp");
        Bitmap bmpBoule = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\Horizon_GroundSky.bmp");
        Bitmap bmpAvion = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\Maquette_Avion.bmp");

        private double MyRollAngle = 0;
               /* Set up a dummy holding variable */

        private double _RollAngle = 0;

        /* Set up the exchanged parameters for the control */

        public double RollAngle
        {
            get  //When data is asked for do this
            {
                return _RollAngle;
            }
            set  //When data is sent to the variable do this
            {
                if (_RollAngle == value)
                {
                    return;
                }
                _RollAngle = value;
                if (OnVariableChange != null)
                {
                    OnVariableChange(_RollAngle);
                }
                Invalidate();
            }
        }

        /* Set up a dummy holding variable */

        private double _PitchAngle = 0;

        /* Set up the exchanged parameters for the control */

        public double PitchAngle
        {
            get { return _PitchAngle; }
            set
            {
                if (_PitchAngle == value) return;
                _PitchAngle = value;
                if (OnVariableChange != null)
                    OnVariableChange(_PitchAngle);
                Invalidate();
            }
        }

        public delegate void OnVariableChangeDelegate(double newVal);
        public event OnVariableChangeDelegate OnVariableChange;

        //private System.ComponentModel.Container components = null;

        /* Program entry point */

        public Artifical_Horizon()
        {
            InitializeComponent();

            // Double bufferisation
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint, true);

            Invalidate();
        }

        private void UserControl1_Paint(object sender, PaintEventArgs pe)
        {
            MyRollAngle = RollAngle * (3.14 / 180);
             
            // Pre Display computings

            Point ptBoule = new Point(25, -210);
            Point ptRotation = new Point(150, 150);

            float scale = (float)this.Width / bmpCadran.Width;

            // Affichages - - - - - - - - - - - - - - - - - - - - - - 

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpAvion.MakeTransparent(Color.Yellow);

            // display Horizon
            RotateAndTranslate(pe, bmpBoule, MyRollAngle, 0, ptBoule, (int)(4 * PitchAngle), ptRotation, scale);

            // diplay mask
            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display aircraft symbol
            pe.Graphics.DrawImage(bmpAvion, (float)((0.5 * bmpCadran.Width - 0.5 * bmpAvion.Width) * scale), (float)((0.5 * bmpCadran.Height - 0.5 * bmpAvion.Height) * scale), (float)(bmpAvion.Width * scale), (float)(bmpAvion.Height * scale));


        }

        protected void RotateImage(PaintEventArgs pe, Image img, Double alpha, Point ptImg, Point ptRot, float scaleFactor)
        {
            double beta = 0;    // Angle between the Horizontal line and the line (Left upper corner - Rotation point)
            double d = 0;       // Distance between Left upper corner and Rotation point)		
            float deltaX = 0;   // X componant of the corrected translation
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


        protected void RotateAndTranslate(PaintEventArgs pe, Image img, Double alphaRot, Double alphaTrs, Point ptImg, int deltaPx, Point ptRot, float scaleFactor)
        {
            double beta = 0;
            double d = 0;
            float deltaXRot = 0;
            float deltaYRot = 0;
            float deltaXTrs = 0;
            float deltaYTrs = 0;

            // Rotation

            if (ptImg != ptRot)
            {
                // Internals coeffs
                if (ptRot.X != 0)
                {
                    beta = Math.Atan((double)ptRot.Y / (double)ptRot.X);
                }

                d = Math.Sqrt((ptRot.X * ptRot.X) + (ptRot.Y * ptRot.Y));

                // Computed offset
                deltaXRot = (float)(d * (Math.Cos(alphaRot - beta) - Math.Cos(alphaRot) * Math.Cos(alphaRot + beta) - Math.Sin(alphaRot) * Math.Sin(alphaRot + beta)));
                deltaYRot = (float)(d * (Math.Sin(beta - alphaRot) + Math.Sin(alphaRot) * Math.Cos(alphaRot + beta) - Math.Cos(alphaRot) * Math.Sin(alphaRot + beta)));
            }

            // Translation

            // Computed offset
            deltaXTrs = (float)(deltaPx * (Math.Sin(alphaTrs)));
            deltaYTrs = (float)(-deltaPx * (-Math.Cos(alphaTrs)));

            // Rotate image support
            pe.Graphics.RotateTransform((float)(alphaRot * 180 / Math.PI));

            // Dispay image
            pe.Graphics.DrawImage(img, (ptImg.X + deltaXRot + deltaXTrs) * scaleFactor, (ptImg.Y + deltaYRot + deltaYTrs) * scaleFactor, img.Width * scaleFactor, img.Height * scaleFactor);

            // Put image support as found
            pe.Graphics.RotateTransform((float)(-alphaRot * 180 / Math.PI));
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
    }
}
