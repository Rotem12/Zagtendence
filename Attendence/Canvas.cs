using System;
using System.Drawing;
using System.Windows.Forms;

namespace Attendence
{
    public partial class Canvas : Form
    {
        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        bool drawing;

        public Canvas()
        {
            StartPosition = FormStartPosition.Manual;
            Location = Cursor.Position;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Opacity = 0.55;
            this.Cursor = Cursors.Cross;
            this.MouseDown += Canvas_MouseDown;
            this.MouseMove += Canvas_MouseMove;
            this.MouseUp += Canvas_MouseUp;
            this.Paint += Canvas_Paint;
            this.KeyDown += Canvas_KeyDown;
            this.DoubleBuffered = true;
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        public Rectangle GetRectangle()
        {
            int x, y;

            Screen screen = Screen.from()

            return new Rectangle(
                Left + Math.Min(startPos.X, currentPos.X),
                Top + Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }

        public Rectangle GetInnerRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            currentPos = e.Location;
            if (drawing) this.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            currentPos = e.Location;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (drawing)
            {
                Rectangle rect = new Rectangle(Point.Empty, Size);
                Rectangle snip = GetInnerRectangle();
                e.Graphics.DrawRectangle(Pens.Red, snip);

                Region currentRegion = new Region(rect);

                System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
                Rectangle exclude = new Rectangle(snip.Left + 1, snip.Top + 1, snip.Width - 1, snip.Height - 1);
                g.AddRectangle(exclude);
                currentRegion.Exclude(g);

                Region = currentRegion;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Canvas";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
    }
}
