using MsmhTools;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
/*
* Copyright MSasanMH, April 19, 2022.
*/

namespace CustomControls
{
    public class CustomButton : Button
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatButtonAppearance? FlatAppearance { get; set; }

        private Color mBackColor = Color.DimGray;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Back Color")]
        public override Color BackColor
        {
            get { return mBackColor; }
            set
            {
                if (mBackColor != value)
                {
                    mBackColor = value;
                    Invalidate();
                }
            }
        }

        private Color mForeColor = Color.White;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Fore Color")]
        public override Color ForeColor
        {
            get { return mForeColor; }
            set
            {
                if (mForeColor != value)
                {
                    mForeColor = value;
                    Invalidate();
                }
            }
        }

        private Color mBorderColor = Color.Blue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Border Color")]
        public Color BorderColor
        {
            get { return mBorderColor; }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    Invalidate();
                }
            }
        }

        private Color mSelectionColor = Color.LightBlue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Selection Color")]
        public Color SelectionColor
        {
            get { return mSelectionColor; }
            set
            {
                if (mSelectionColor != value)
                {
                    mSelectionColor = value;
                    Invalidate();
                }
            }
        }

        private int mRoundedCorners = 0;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Category("Appearance"), Description("Rounded Corners")]
        public int RoundedCorners
        {
            get { return mRoundedCorners; }
            set
            {
                if (mRoundedCorners != value)
                {
                    mRoundedCorners = value;
                    Invalidate();
                }
            }
        }

        private static bool ButtonMouseHover { get; set; }
        private static bool ButtonMouseDown { get; set; }
        private static bool ApplicationIdle = false;
        private bool once = true;

        public CustomButton() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, true);

            FlatStyle = FlatStyle.Flat;
            
            Application.Idle += Application_Idle;
            HandleCreated += CustomButton_HandleCreated;
            LocationChanged += CustomButton_LocationChanged;
            Move += CustomButton_Move;
            MouseDown += CustomButton_MouseDown;
            MouseUp += CustomButton_MouseUp;
            MouseEnter += CustomButton_MouseEnter;
            MouseLeave += CustomButton_MouseLeave;
            EnabledChanged += CustomButton_EnabledChanged;
            Paint += CustomButton_Paint;
        }

        private void Application_Idle(object? sender, EventArgs e)
        {
            ApplicationIdle = true;
            if (Parent != null)
            {
                if (once)
                {
                    Control topParent = FindForm();
                    topParent.Move -= TopParent_Move;
                    topParent.Move += TopParent_Move;
                    Parent.Move -= Parent_Move;
                    Parent.Move += Parent_Move;
                    Invalidate();
                    once = false;
                }
            }
        }

        private void TopParent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void Parent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CustomButton_HandleCreated(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CustomButton_LocationChanged(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.Invalidate();
            }
        }

        private void CustomButton_Move(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.Invalidate();
            }
        }

        private void CustomButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                ButtonMouseDown = true;
                button.Invalidate();
            }
        }

        private void CustomButton_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                ButtonMouseDown = false;
                button.Invalidate();
            }
        }

        private void CustomButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ButtonMouseHover = true;
                button.Invalidate();
            }
        }

        private void CustomButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ButtonMouseHover = false;
                button.Invalidate();
            }
        }

        private void CustomButton_EnabledChanged(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CustomButton_Paint(object? sender, PaintEventArgs e)
        {
            if (ApplicationIdle == false)
                return;

            if (sender is Button button)
            {
                Color backColor = GetBackColor(button);
                Color foreColor = GetForeColor();
                Color borderColor = GetBorderColor();

                if (Parent != null)
                    e.Graphics.Clear(Parent.BackColor);

                Rectangle rect = new(0, 0, button.ClientSize.Width - 1, button.ClientSize.Height - 1);

                // Draw Button Background
                using SolidBrush sbBG = new(backColor);
                //e.Graphics.FillRectangle(sbBG, rect);
                e.Graphics.FillRoundedRectangle(sbBG, rect, RoundedCorners, RoundedCorners, RoundedCorners, RoundedCorners);

                // Draw Hover Button Background (// MousePosition Or Cursor.Position) (Faster than rect.Contains(button.PointToClient(MousePosition)))
                if (button.PointToScreen(Point.Empty).X <= MousePosition.X
                        && MousePosition.X <= (button.PointToScreen(Point.Empty).X + rect.Width)
                        && button.PointToScreen(Point.Empty).Y <= MousePosition.Y
                        && MousePosition.Y <= (button.PointToScreen(Point.Empty).Y + rect.Height))
                {
                    if (ButtonMouseHover)
                    {
                        if (ButtonMouseDown)
                        {
                            Color mouseDownBackColor;
                            if (backColor.DarkOrLight() == "Dark")
                                mouseDownBackColor = backColor.ChangeBrightness(0.2f);
                            else
                                mouseDownBackColor = backColor.ChangeBrightness(-0.2f);

                            using SolidBrush sbDBG = new(mouseDownBackColor);
                            //e.Graphics.FillRectangle(sbDBG, rect);
                            e.Graphics.FillRoundedRectangle(sbDBG, rect, RoundedCorners, RoundedCorners, RoundedCorners, RoundedCorners);
                            ButtonMouseDown = false; // Fix a minor bug.
                        }
                        else
                        {
                            Color mouseHoverBackColor;
                            if (backColor.DarkOrLight() == "Dark")
                                mouseHoverBackColor = BackColor.ChangeBrightness(0.1f);
                            else
                                mouseHoverBackColor = BackColor.ChangeBrightness(-0.1f);

                            using SolidBrush sbHBG = new(mouseHoverBackColor);
                            //e.Graphics.FillRectangle(sbHBG, rect);
                            e.Graphics.FillRoundedRectangle(sbHBG, rect, RoundedCorners, RoundedCorners, RoundedCorners, RoundedCorners);
                        }
                    }
                }

                if (button.Enabled && button.Focused)
                {
                    rect.Inflate(-2, -2);
                    using Pen pen = new(SelectionColor) { DashStyle = DashStyle.Dash };
                    //if (RoundedCorners == 0)
                    //    e.Graphics.DrawRectangle(pen, rect);
                    //else
                    e.Graphics.DrawRoundedRectangle(pen, rect, RoundedCorners, RoundedCorners, RoundedCorners, RoundedCorners);
                    rect.Inflate(+2, +2);
                }

                // Draw Button Text
                TextFormatFlags flags;

                if (RightToLeft == RightToLeft.No)
                {
                    if (TextAlign == ContentAlignment.BottomCenter)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    else if (TextAlign == ContentAlignment.BottomLeft)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.Left;
                    else if (TextAlign == ContentAlignment.BottomRight)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.Right;
                    else if (TextAlign == ContentAlignment.MiddleCenter)
                        flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    else if (TextAlign == ContentAlignment.MiddleLeft)
                        flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    else if (TextAlign == ContentAlignment.MiddleRight)
                        flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    else if (TextAlign == ContentAlignment.TopCenter)
                        flags = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    else if (TextAlign == ContentAlignment.TopLeft)
                        flags = TextFormatFlags.Top | TextFormatFlags.Left;
                    else if (TextAlign == ContentAlignment.TopRight)
                        flags = TextFormatFlags.Top | TextFormatFlags.Right;
                    else
                        flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                }
                else
                {
                    if (TextAlign == ContentAlignment.BottomCenter)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    else if (TextAlign == ContentAlignment.BottomLeft)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.Right;
                    else if (TextAlign == ContentAlignment.BottomRight)
                        flags = TextFormatFlags.Bottom | TextFormatFlags.Left;
                    else if (TextAlign == ContentAlignment.MiddleCenter)
                        flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    else if (TextAlign == ContentAlignment.MiddleLeft)
                        flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    else if (TextAlign == ContentAlignment.MiddleRight)
                        flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    else if (TextAlign == ContentAlignment.TopCenter)
                        flags = TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    else if (TextAlign == ContentAlignment.TopLeft)
                        flags = TextFormatFlags.Top | TextFormatFlags.Right;
                    else if (TextAlign == ContentAlignment.TopRight)
                        flags = TextFormatFlags.Top | TextFormatFlags.Left;
                    else
                        flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

                    flags |= TextFormatFlags.RightToLeft;
                }

                TextRenderer.DrawText(e.Graphics, button.Text, button.Font, rect, foreColor, flags);

                // Draw Button Border
                using Pen penb = new(borderColor);
                //e.Graphics.DrawRectangle(penb, rect);
                e.Graphics.DrawRoundedRectangle(penb, rect, RoundedCorners, RoundedCorners, RoundedCorners, RoundedCorners);
            }
        }

        private Color GetBackColor(Button button)
        {
            if (button.Enabled)
                return BackColor;
            else
            {
                if (BackColor.DarkOrLight() == "Dark")
                    return BackColor.ChangeBrightness(0.3f);
                else
                    return BackColor.ChangeBrightness(-0.3f);
            }
        }

        private Color GetForeColor()
        {
            if (Enabled)
                return ForeColor;
            else
            {
                if (ForeColor.DarkOrLight() == "Dark")
                    return ForeColor.ChangeBrightness(0.2f);
                else
                    return ForeColor.ChangeBrightness(-0.2f);
            }
        }

        private Color GetBorderColor()
        {
            if (Enabled)
                return BorderColor;
            else
            {
                if (BorderColor.DarkOrLight() == "Dark")
                    return BorderColor.ChangeBrightness(0.3f);
                else
                    return BorderColor.ChangeBrightness(-0.3f);
            }
        }

    }

    static class Extentions
    {
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            GraphicsPath path;
            path = RoundedRectangle(bounds, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(pen, path);
            graphics.SmoothingMode = SmoothingMode.Default;
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            GraphicsPath path;
            path = RoundedRectangle(bounds, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.SmoothingMode = SmoothingMode.Default;
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            int diameterTopLeft = radiusTopLeft * 2;
            int diameterTopRight = radiusTopRight * 2;
            int diameterBottomRight = radiusBottomRight * 2;
            int diameterBottomLeft = radiusBottomLeft * 2;

            Rectangle arc1 = new(bounds.Location, new Size(diameterTopLeft, diameterTopLeft));
            Rectangle arc2 = new(bounds.Location, new Size(diameterTopRight, diameterTopRight));
            Rectangle arc3 = new(bounds.Location, new Size(diameterBottomRight, diameterBottomRight));
            Rectangle arc4 = new(bounds.Location, new Size(diameterBottomLeft, diameterBottomLeft));
            GraphicsPath path = new();

            // Top Left Arc  
            if (radiusTopLeft == 0)
            {
                path.AddLine(arc1.Location, arc1.Location);
            }
            else
            {
                path.AddArc(arc1, 180, 90);
            }
            // Top Right Arc  
            arc2.X = bounds.Right - diameterTopRight;
            if (radiusTopRight == 0)
            {
                path.AddLine(arc2.Location, arc2.Location);
            }
            else
            {
                path.AddArc(arc2, 270, 90);
            }
            // Bottom Right Arc
            arc3.X = bounds.Right - diameterBottomRight;
            arc3.Y = bounds.Bottom - diameterBottomRight;
            if (radiusBottomRight == 0)
            {
                path.AddLine(arc3.Location, arc3.Location);
            }
            else
            {
                path.AddArc(arc3, 0, 90);
            }
            // Bottom Left Arc 
            arc4.X = bounds.Right - diameterBottomLeft;
            arc4.Y = bounds.Bottom - diameterBottomLeft;
            arc4.X = bounds.Left;
            if (radiusBottomLeft == 0)
            {
                path.AddLine(arc4.Location, arc4.Location);
            }
            else
            {
                path.AddArc(arc4, 90, 90);
            }
            path.CloseFigure();
            return path;
        }
    }
}
