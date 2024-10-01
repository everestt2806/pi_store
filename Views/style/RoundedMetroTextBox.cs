using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace pi_store.Views.style
{
    public class RoundedTextBox : UserControl
    {
        private TextBox innerTextBox;
        private int _cornerRadius = 30;

        public int CornerRadius
        {
            get { return _cornerRadius; }
            set
            {
                _cornerRadius = value;
                this.Invalidate();
            }
        }

        public string Text
        {
            get { return innerTextBox.Text; }
            set { innerTextBox.Text = value; }
        }

        public RoundedTextBox()
        {
            innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(10, 7), // Điều chỉnh padding
                Width = this.Width - 20,     // Để có khoảng trống cho đường viền bo góc
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            this.Controls.Add(innerTextBox);
            this.Padding = new Padding(10);
            this.BackColor = Color.White;
            this.Resize += RoundedTextBox_Resize;
        }

        private void RoundedTextBox_Resize(object sender, EventArgs e)
        {
            innerTextBox.Width = this.Width - 20;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var gp = new GraphicsPath())
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = this.ClientRectangle;
                int diameter = _cornerRadius * 2;
                gp.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                gp.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                gp.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                gp.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
                gp.CloseAllFigures();

                this.Region = new Region(gp);

                using (var pen = new Pen(this.ForeColor, 1.5f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, gp);
                }
            }
        }
    }
}
