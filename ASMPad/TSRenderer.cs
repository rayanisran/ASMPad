using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class TSRenderer : System.Windows.Forms.ToolStripProfessionalRenderer
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
    //// Render container background gradient
    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        base.OnRenderToolStripBackground(e);

        LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, clrVerBG_White, clrVerBG_GrayBlue, LinearGradientMode.Vertical);
        System.Drawing.SolidBrush shadow = new System.Drawing.SolidBrush(clrVerBG_Shadow);
        Rectangle rect = new Rectangle(0, e.ToolStrip.Height - 2, e.ToolStrip.Width, 1);
        e.Graphics.FillRectangle(b, e.AffectedBounds);
        e.Graphics.FillRectangle(shadow, rect);
    }

    //// Render button selected and pressed state
    protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
    {
        base.OnRenderButtonBackground(e);
        if (e.Item.Selected | ((ToolStripButton)e.Item).Checked)
        {
            Rectangle rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            Rectangle rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            LinearGradientBrush b = new LinearGradientBrush(rect, clrToolstripBtnGrad_White, clrToolstripBtnGrad_Blue, LinearGradientMode.Vertical);
            System.Drawing.SolidBrush b2 = new System.Drawing.SolidBrush(clrToolstripBtn_Border);

            e.Graphics.FillRectangle(b2, rectBorder);
            e.Graphics.FillRectangle(b, rect);
        }
        if (e.Item.Pressed)
        {
            Rectangle rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            Rectangle rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            LinearGradientBrush b = new LinearGradientBrush(rect, clrToolstripBtnGrad_White_Pressed, clrToolstripBtnGrad_Blue_Pressed, LinearGradientMode.Vertical);
            System.Drawing.SolidBrush b2 = new System.Drawing.SolidBrush(clrToolstripBtn_Border);

            e.Graphics.FillRectangle(b2, rectBorder);
            e.Graphics.FillRectangle(b, rect);
        }
    }



}