using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundButton : Button
{
    protected override void OnPaint(PaintEventArgs e)
    {
        GraphicsPath buttonPath = new GraphicsPath();
        int cornerRadius = 20; // Adjust this value to change the roundness

        int width = this.Width;
        int height = this.Height;

        buttonPath.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
        buttonPath.AddArc(width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
        buttonPath.AddArc(width - cornerRadius * 2, height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
        buttonPath.AddArc(0, height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
        buttonPath.CloseFigure();

        this.Region = new Region(buttonPath);

        base.OnPaint(e);
    }
}
