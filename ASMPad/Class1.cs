using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ASMPad
{
    public static class clsColors
    {
        public static Color clrHorBG_GrayBlue = Color.FromArgb(255, 233, 236, 250);
        public static Color clrHorBG_White = Color.FromArgb(255, 244, 247, 252);
        public static Color clrSubmenuBG = Color.FromArgb(255, 240, 240, 240);
        public static Color clrImageMarginBlue = Color.FromArgb(255, 212, 216, 230);
        public static Color clrImageMarginWhite = Color.FromArgb(255, 244, 247, 252);
        public static Color clrImageMarginLine = Color.FromArgb(255, 160, 160, 180);
        public static Color clrSelectedBG_Blue = Color.FromArgb(255, 186, 228, 246);
        public static Color clrSelectedBG_Header_Blue = Color.FromArgb(255, 146, 202, 230);
        public static Color clrSelectedBG_White = Color.FromArgb(255, 241, 248, 251);
        public static Color clrSelectedBG_Border = Color.FromArgb(255, 150, 217, 249);
        public static Color clrSelectedBG_Drop_Blue = Color.FromArgb(255, 139, 195, 225);
        public static Color clrSelectedBG_Drop_Border = Color.FromArgb(255, 48, 127, 177);
        public static Color clrMenuBorder = Color.FromArgb(255, 160, 160, 160);
        public static Color clrCheckBG = Color.FromArgb(255, 206, 237, 250);
        public static Color clrVerBG_GrayBlue = Color.FromArgb(255, 196, 203, 219);
        public static Color clrVerBG_White = Color.FromArgb(255, 250, 250, 253);
        public static Color clrVerBG_Shadow = Color.FromArgb(255, 181, 190, 206);
        public static Color clrToolstripBtnGrad_Blue = Color.FromArgb(255, 129, 192, 224);
        public static Color clrToolstripBtnGrad_White = Color.FromArgb(255, 237, 248, 253);
        public static Color clrToolstripBtn_Border = Color.FromArgb(255, 41, 153, 255);
        public static Color clrToolstripBtnGrad_Blue_Pressed = Color.FromArgb(255, 124, 177, 204);
        public static Color clrToolstripBtnGrad_White_Pressed = Color.FromArgb(255, 228, 245, 252);

        public static void DrawRoundedRectangle(Graphics objGraphics, int m_intxAxis, int m_intyAxis, int m_intWidth, int m_intHeight, int m_diameter, Color color)
        {
            Pen pen = new Pen(color);

            //Dim g As Graphics
            RectangleF BaseRect = new RectangleF(m_intxAxis, m_intyAxis, m_intWidth, m_intHeight);
            RectangleF ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
            //top left Arc
            objGraphics.DrawArc(pen, ArcRect, 180, 90);
            objGraphics.DrawLine(pen, m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis, m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis);

            // top right arc
            ArcRect.X = BaseRect.Right - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 270, 90);
            objGraphics.DrawLine(pen, m_intxAxis + m_intWidth, m_intyAxis + Convert.ToInt32(m_diameter / 2), m_intxAxis + m_intWidth, m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));

            // bottom right arc
            ArcRect.Y = BaseRect.Bottom - m_diameter;
            objGraphics.DrawArc(pen, ArcRect, 0, 90);
            objGraphics.DrawLine(pen, m_intxAxis + Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight, m_intxAxis + m_intWidth - Convert.ToInt32(m_diameter / 2), m_intyAxis + m_intHeight);

            // bottom left arc
            ArcRect.X = BaseRect.Left;
            objGraphics.DrawArc(pen, ArcRect, 90, 90);
            objGraphics.DrawLine(pen, m_intxAxis, m_intyAxis + Convert.ToInt32(m_diameter / 2), m_intxAxis, m_intyAxis + m_intHeight - Convert.ToInt32(m_diameter / 2));

        }
    }
}
