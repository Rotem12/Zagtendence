using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Attendence
{
    class ScreenCapture
    {
        private Rectangle canvasBounds;

        public ScreenCapture()
        {
            canvasBounds = Screen.GetBounds(Cursor.Position);
            //        SetCanvas();
        }

        public Bitmap GetSnapShot()
        {
            using (Image image = new Bitmap(canvasBounds.Width, canvasBounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CopyFromScreen(new Point(canvasBounds.Left, canvasBounds.Top), Point.Empty, canvasBounds.Size);
                }
                return new Bitmap(image);
            }
        }

        private Image SetBorder(Image srcImg, Color color, int width)
        {
            // Create a copy of the image and graphics context
            Image dstImg = srcImg.Clone() as Image;
            Graphics g = Graphics.FromImage(dstImg);

            // Create the pen
            Pen pBorder = new Pen(color, width)
            {
                Alignment = PenAlignment.Center
            };

            // Draw
            g.DrawRectangle(pBorder, 0, 0, dstImg.Width - 1, dstImg.Height - 1);

            // Clean up
            pBorder.Dispose();
            g.Save();
            g.Dispose();

            // Return
            return dstImg;
        }

        public void SetCanvas()
        {
            using (Canvas canvas = new Canvas())
            {
                if (canvas.ShowDialog() == DialogResult.OK)
                {
                    this.canvasBounds = canvas.GetRectangle();
                }
            }
        }

        public Bitmap SetCanvasAndGetSnap()
        {
            using (Canvas canvas = new Canvas())
            {
                if (canvas.ShowDialog() == DialogResult.OK)
                {
                    this.canvasBounds = canvas.GetRectangle();
                    return GetSnapShot();
                }
            }

            return null;
        }
    }
}
