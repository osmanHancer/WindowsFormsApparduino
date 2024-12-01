/*****************************************************************************/
/* Project  : Altimeter User Control                                         */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : Altimeter instrument control                                   */
/* Creation : 22/06/2008                                                     */
/* Author   : Original code by Guillaume CHOUTEAU                            */
/* History  : Modified to user control format by Stephen Abbadessa           */
/*            University of Maine 4/24/2020                                  */
/*****************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CS_WinForms_Ctrl_Altimeter
{
    public partial class Altimeter: UserControl
    {

        // Images
        Bitmap bmpCadran = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\Altimeter_Background.bmp");
        Bitmap bmpSmallNeedle = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\SmallNeedleAltimeter.bmp");
        Bitmap bmpLongNeedle = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\LongNeedleAltimeter.bmp");
        Bitmap bmpScroll = new Bitmap("C:\\Users\\ohanc\\OneDrive\\Masaüstü\\Bitirme_Yedek\\WindowsFormsApparduino\\WindowsFormsApparduino\\Assets\\Bandeau_Dérouleur.bmp");

        /* Set up a dummy holding variable */

       private int _Altitude = 0;

        /* Set up the exchanged parameters for the control */

        public int Altitude
        {
            get { return _Altitude; }
            set
            {
                if (_Altitude == value) return;
                _Altitude = value;
                if (OnVariableChange != null)
                    OnVariableChange(_Altitude);
                Invalidate();
            }
        }

        public delegate void OnVariableChangeDelegate(int newVal);
        public event OnVariableChangeDelegate OnVariableChange;

        /* Program entry point */




        public Altimeter()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint, true);

            Invalidate();


        }

        private void Altimeter_Paint(object sender, PaintEventArgs pe)
        {
            // Calling the base class OnPaint
          //  base.OnPaint(pe);

            // Pre Display computings
            Point ptCounter = new Point(35, 135);
            Point ptRotation = new Point(150, 150);
            Point ptimgNeedle = new Point(136, 39);

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpLongNeedle.MakeTransparent(Color.Yellow);
            bmpSmallNeedle.MakeTransparent(Color.Yellow);

            double alphaSmallNeedle = InterpolPhyToAngle(Altitude, 0, 10000, 0, 359);
            double alphaLongNeedle = InterpolPhyToAngle(Altitude % 1000, 0, 1000, 0, 359);

            float scale = (float)this.Width / bmpCadran.Width;

            // display counter
            ScrollCounter(pe, bmpScroll, 5, Altitude, ptCounter, scale);

            // diplay mask
            Pen maskPen = new Pen(this.BackColor, 30 * scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display small needle
            RotateImage(pe, bmpSmallNeedle, alphaSmallNeedle, ptimgNeedle, ptRotation, scale);

            // display long needle
            RotateImage(pe, bmpLongNeedle, alphaLongNeedle, ptimgNeedle, ptRotation, scale);
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


        protected void ScrollCounter(PaintEventArgs pe, Image imgBand, int nbOfDigits, int counterValue, Point ptImg, float scaleFactor)
        {
            int indexDigit = 0;
            int digitBoxHeight = (int)(imgBand.Height / 11);
            int digitBoxWidth = imgBand.Width;

            for (indexDigit = 0; indexDigit < nbOfDigits; indexDigit++)
            {
                int currentDigit;
                int prevDigit;
                int xOffset;
                int yOffset;
                double fader;

                currentDigit = (int)((counterValue / Math.Pow(10, indexDigit)) % 10);

                if (indexDigit == 0)
                {
                    prevDigit = 0;
                }
                else
                {
                    prevDigit = (int)((counterValue / Math.Pow(10, indexDigit - 1)) % 10);
                }

                // xOffset Computing
                xOffset = (int)(digitBoxWidth * (nbOfDigits - indexDigit - 1));

                // yOffset Computing	
                if (prevDigit == 9)
                {
                    fader = 0.33;
                    yOffset = (int)(-((fader + currentDigit) * digitBoxHeight));
                }
                else
                {
                    yOffset = (int)(-(currentDigit * digitBoxHeight));
                }

                // Display Image
                pe.Graphics.DrawImage(imgBand, (ptImg.X + xOffset) * scaleFactor, (ptImg.Y + yOffset) * scaleFactor, imgBand.Width * scaleFactor, imgBand.Height * scaleFactor);
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






    }
}
