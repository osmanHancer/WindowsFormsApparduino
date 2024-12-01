/*****************************************************************************/
/* Project  : Turn Coordinator Instrument User Control                       */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : Turn coordinator instrument control                            */
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

namespace CS_WinForms_Ctrl_TurnCoordinatorInstrument
{
    public partial class TurnCoordinatorInstrument : UserControl
    {


        // Images
        Bitmap bmpCadran = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\TurnCoordinator_Background.bmp");
        Bitmap bmpBall = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\TurnCoordinatorBall.bmp");
        Bitmap bmpAircraft = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\TurnCoordinatorAircraft.bmp");
        Bitmap bmpMarks = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\TurnCoordinatorMarks.bmp");

        /* Set up a dummy holding variable */

        private float _TurnRate = 0;

        /* Set up the exchanged parameters for the control */

        public float TurnRate
        {
            get { return _TurnRate; }
            set
            {
                if (_TurnRate == value) return;
                _TurnRate = value;
                if (OnVariableChange != null)
                    OnVariableChange(_TurnRate);
                Invalidate();
            }
        }


        /* Set up a dummy holding variable */

        private float _TurnQuality = 0;

        /* Set up the exchanged parameters for the control */

        public float TurnQuality
        {
            get { return _TurnQuality; }
            set
            {
                if (_TurnQuality == value) return;
                _TurnQuality = value;
                if (OnVariableChange != null)
                    OnVariableChange(_TurnQuality);
                Invalidate();
            }
        }

        public delegate void OnVariableChangeDelegate(float newVal);
        public event OnVariableChangeDelegate OnVariableChange;

        /* Program entry */

        public TurnCoordinatorInstrument()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
          ControlStyles.AllPaintingInWmPaint, true);


            Invalidate();

        }

        private void TurnCoordinatorInstrument_Paint(object sender, PaintEventArgs pe)
        {

         // Pre Display computings
            Point ptRotationAircraft = new Point(150, 150);
            Point ptImgAircraft = new Point(57, 114);
            Point ptRotationBall = new Point(150, -155);
            Point ptImgBall = new Point(136, 216);
            Point ptMarks = new Point(134, 216);

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpBall.MakeTransparent(Color.Yellow);
            bmpAircraft.MakeTransparent(Color.Yellow);
            bmpMarks.MakeTransparent(Color.Yellow);

            double alphaAircraft = InterpolPhyToAngle(TurnRate, -6, 6, -30, 30);
            double alphaBall = InterpolPhyToAngle(TurnQuality, -10, 10, -11, 11);

            float scale = (float)this.Width / bmpCadran.Width;

            // diplay mask
            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display Ball
            RotateImage(pe, bmpBall, alphaBall, ptImgBall, ptRotationBall, scale);

            // display Aircraft
            RotateImage(pe, bmpAircraft, alphaAircraft, ptImgAircraft, ptRotationAircraft, scale);

            // display Marks
            pe.Graphics.DrawImage(bmpMarks, (int)(ptMarks.X * scale), (int)(ptMarks.Y * scale), bmpMarks.Width * scale, bmpMarks.Height * scale);
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

        protected Point FromCartRefToImgRef(Point cartPoint)
        {
            Point imgPoint = new Point();
            imgPoint.X = cartPoint.X + (this.Width / 2);
            imgPoint.Y = -cartPoint.Y + (this.Height / 2);
            return (imgPoint);
        }

        protected double FromDegToRad(double degAngle)
        {
            double radAngle = degAngle * Math.PI / 180;
            return radAngle;
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

    }

}

