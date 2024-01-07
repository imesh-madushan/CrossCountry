using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GradientTemplate
{
    public class GradientPanel : Panel
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public LinearGradientMode GradientMode { get; set; }

        public GradientPanel()
        {
            // Set default gradient properties
            StartColor = Color.FromArgb(255, 128, 128, 255);
            EndColor = Color.White;
            GradientMode = LinearGradientMode.Vertical;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, StartColor, EndColor, GradientMode))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
    }
}
