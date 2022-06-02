using MsmhTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
/*
* Copyright MSasanMH, June 01, 2022.
*/

namespace CustomControls
{
    public class CustomTabControl : TabControl
    {
        private static class Methods
        {
            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
            private extern static int SetWindowTheme(IntPtr controlHandle, string appName, string? idList);
            internal static void SetDarkControl(Control control)
            {
                _ = SetWindowTheme(control.Handle, "DarkMode_Explorer", null);
                foreach (Control c in control.Controls)
                {
                    _ = SetWindowTheme(c.Handle, "DarkMode_Explorer", null);
                }
            }
        }

        private static Color mBackColor = Color.DimGray;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Back Color")]
        public new Color BackColor
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

        private static Color mTabBackColor = Color.DodgerBlue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Selected Tab Back Color")]
        public Color TabBackColor
        {
            get { return mTabBackColor; }
            set
            {
                if (mTabBackColor != value)
                {
                    mTabBackColor = value;
                    Invalidate();
                }
            }
        }

        private static Color mForeColor = Color.White;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(WindowsFormsComponentEditor), typeof(Color))]
        [Category("Appearance"), Description("Fore Color")]
        public new Color ForeColor
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

        private static Color mBorderColor = Color.Blue;
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

        private static int mBorderThickness = 1;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Category("Appearance"), Description("Border Thickness")]
        public int BorderThickness
        {
            get { return mBorderThickness; }
            set
            {
                if (mBorderThickness != value)
                {
                    mBorderThickness = value;
                    Invalidate();
                }
            }
        }

        private static bool ControlEnabled = true;
        private bool once = true;

        public CustomTabControl() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, true);

            ControlEnabled = Enabled;

            ControlAdded += CustomTabControl_ControlAdded;
            ControlRemoved += CustomTabControl_ControlRemoved;
            Application.Idle += Application_Idle;
            LocationChanged += CustomTabControl_LocationChanged;
            Move += CustomTabControl_Move;
            SizeChanged += CustomTabControl_SizeChanged;
            EnabledChanged += CustomTabControl_EnabledChanged;
            Invalidated += CustomTabControl_Invalidated;
            Paint += CustomTabControl_Paint;
        }

        private void SearchTabPages()
        {
            for (int n = 0; n < TabPages.Count; n++)
            {
                var tabPage = TabPages[n];
                tabPage.Paint -= TabPage_Paint;
                tabPage.Paint += TabPage_Paint;
            }
        }

        private void CustomTabControl_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (e.Control is TabPage)
                SearchTabPages();
            Invalidate();
        }

        private void CustomTabControl_ControlRemoved(object? sender, ControlEventArgs e)
        {
            if (e.Control is TabPage)
                SearchTabPages();
            Invalidate();
        }

        private void Application_Idle(object? sender, EventArgs e)
        {
            if (Parent != null)
            {
                if (once)
                {
                    SearchTabPages();

                    Control topParent = Tools.Controllers.GetTopParent(this);
                    topParent.Move -= TopParent_Move;
                    topParent.Move += TopParent_Move;
                    Parent.Move -= Parent_Move;
                    Parent.Move += Parent_Move;
                    Invalidate();
                    once = false;
                }
            }
        }

        private void TabPage_Paint(object? sender, PaintEventArgs e)
        {
            var tabPage = sender as TabPage;

            Color tabPageColor;
            if (Enabled)
                tabPageColor = tabPage.BackColor;
            else
            {
                if (tabPage.BackColor.DarkOrLight() == "Dark")
                    tabPageColor = tabPage.BackColor.ChangeBrightness(0.3f);
                else
                    tabPageColor = tabPage.BackColor.ChangeBrightness(-0.3f);
            }

            using SolidBrush sb = new(tabPageColor);
            e.Graphics.FillRectangle(sb, e.ClipRectangle);
        }

        private void TopParent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void Parent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CustomTabControl_LocationChanged(object? sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
                tabControl.Invalidate();
        }

        private void CustomTabControl_Move(object? sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
                tabControl.Invalidate();
        }

        private void CustomTabControl_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is TabControl tabControl)
                tabControl.Invalidate();
        }

        private void CustomTabControl_EnabledChanged(object? sender, EventArgs e)
        {
            ControlEnabled = Enabled;
            if (sender is TabControl tabControl)
                tabControl.Invalidate();
        }

        private void CustomTabControl_Invalidated(object? sender, InvalidateEventArgs e)
        {
            if (BackColor.DarkOrLight() == "Dark")
                Methods.SetDarkControl(this);
        }

        private void CustomTabControl_Paint(object? sender, PaintEventArgs e)
        {
            new TabControlRenderer(sender as TabControl, e).Paint();
        }

        private class TabControlRenderer
        {
            private readonly Point mMouseCursor;
            private readonly Graphics mGraphics;
            private readonly Rectangle mClipRectangle;
            private readonly int mSelectedIndex;
            private readonly int mTabCount;
            private readonly Size mImageSize;
            private readonly Font mFont;
            private readonly bool mEnabled;
            private readonly Image[]? mTabImages;
            private readonly Rectangle[]? mTabRects;
            private readonly string[]? mTabTexts;
            private readonly Size mSize;
            private readonly bool mFailed;

            private static readonly int mImagePadding = 6;
            private static readonly int mSelectedTabPadding = 2;

            public TabControlRenderer(TabControl? tabs, PaintEventArgs e)
            {
                mMouseCursor = tabs.PointToClient(Cursor.Position);
                mGraphics = e.Graphics;
                mClipRectangle = e.ClipRectangle;
                mSize = tabs.Size;
                mSelectedIndex = tabs.SelectedIndex;
                mTabCount = tabs.TabCount;
                mFont = tabs.Font;
                mImageSize = tabs.ImageList?.ImageSize ?? Size.Empty;
                mEnabled = tabs.Enabled;

                try
                {
                    mTabTexts = new string[mTabCount];
                    for (int a = 0; a < mTabCount; a++)
                    {
                        string text = tabs.TabPages[a].Text;
                        if (text != null)
                            mTabTexts[a] = text;
                    }

                    mTabImages = new Image[mTabCount];
                    for (int a = 0; a < mTabCount; a++)
                    {
                        Image image = GetTabImage(tabs, a);
                        if (image != null)
                            mTabImages[a] = image;
                    }

                    mTabRects = new Rectangle[mTabCount];
                    for (int a = 0; a < mTabCount; a++)
                    {
                        Rectangle rect = tabs.GetTabRect(a);
                        mTabRects[a] = rect;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    mFailed = true;
                }
            }

            public void Paint()
            {
                if (mFailed)
                    return;

                using Brush sb = GetBackgroundBrush();
                mGraphics.FillRectangle(sb, mClipRectangle);

                RenderSelectedPageBackground();

                IEnumerable<int> pageIndices;
                if (mSelectedIndex >= 0 && mSelectedIndex < mTabCount)
                {
                    // Render tabs in pyramid order with selected on top
                    pageIndices = Enumerable.Range(0, mSelectedIndex)
                        .Concat(Enumerable.Range(mSelectedIndex, mTabCount - mSelectedIndex).Reverse());
                }
                else
                {
                    pageIndices = Enumerable.Range(0, mTabCount);
                }

                for (int a = 0; a < pageIndices.Count(); a++)
                {
                    int index = pageIndices.ToList()[a];
                    RenderTabBackground(index);
                    RenderTabImage(index);
                    RenderTabText(index, mTabImages[index] != null);
                }
            }

            private static Image? GetTabImage(TabControl tabs, int index)
            {
                var images = tabs.ImageList?.Images;
                if (images is null)
                    return null;

                var page = tabs.TabPages[index];
                if (!string.IsNullOrEmpty(page.ImageKey))
                {
                    return images[page.ImageKey];
                }

                if (page.ImageIndex >= 0 && page.ImageIndex < images.Count)
                {
                    return images[page.ImageIndex];
                }

                return null;
            }

            private void RenderSelectedPageBackground()
            {
                if (mSelectedIndex < 0 || mSelectedIndex >= mTabCount)
                    return;

                Rectangle tabRect = mTabRects[mSelectedIndex];
                Rectangle pageRect = Rectangle.FromLTRB(0, tabRect.Bottom, mSize.Width - 1, mSize.Height - 1);

                if (!mClipRectangle.IntersectsWith(pageRect))
                    return;

                using Brush sb = GetBackgroundBrush();
                mGraphics.FillRectangle(sb, pageRect);

                using Pen borderPen = GetBorderPen();
                mGraphics.DrawRectangle(borderPen, pageRect);
            }

            private void RenderTabBackground(int index)
            {
                Rectangle outerRect = GetOuterTabRect(index);
                using Brush sb = GetTabBackgroundBrush(index);
                mGraphics.FillRectangle(sb, outerRect);

                var points = new List<Point>(4);
                if (index <= mSelectedIndex)
                {
                    points.Add(new Point(outerRect.Left, outerRect.Bottom - 1));
                }

                points.Add(new Point(outerRect.Left, outerRect.Top));
                points.Add(new Point(outerRect.Right - 1, outerRect.Top));

                if (index >= mSelectedIndex)
                {
                    points.Add(new Point(outerRect.Right - 1, outerRect.Bottom - 1));
                }

                using Pen borderPen = GetBorderPen();
                mGraphics.DrawLines(borderPen, points.ToArray());
            }

            private void RenderTabImage(int index)
            {
                Image image = mTabImages[index];
                if (image is null)
                    return;

                Rectangle imgRect = GetTabImageRect(index);
                mGraphics.DrawImage(image, imgRect);
            }

            private void RenderTabText(int index, bool hasImage)
            {
                if (string.IsNullOrEmpty(mTabTexts[index]))
                    return;

                Rectangle textRect = GetTabTextRect(index, hasImage);

                const TextFormatFlags format =
                    TextFormatFlags.NoClipping |
                    TextFormatFlags.NoPrefix |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.HorizontalCenter;

                Color textPen = GetTextColor();

                Color foreColorUnfocused;
                if (textPen.DarkOrLight() == "Dark")
                    foreColorUnfocused = textPen.ChangeBrightness(0.2f);
                else
                    foreColorUnfocused = textPen.ChangeBrightness(-0.2f);

                Color textColor = mEnabled ? textPen : foreColorUnfocused;

                TextRenderer.DrawText(mGraphics, mTabTexts[index], mFont, textRect, textColor, format);
            }

            private Rectangle GetOuterTabRect(int index)
            {
                Rectangle innerRect = mTabRects[index];

                if (index == mSelectedIndex)
                {
                    return Rectangle.FromLTRB(
                        innerRect.Left - mSelectedTabPadding,
                        innerRect.Top - mSelectedTabPadding,
                        innerRect.Right + mSelectedTabPadding,
                        innerRect.Bottom + 1); // +1 to overlap tabs bottom line
                }

                return Rectangle.FromLTRB(innerRect.Left, innerRect.Top + 1, innerRect.Right, innerRect.Bottom);
            }

            private Rectangle GetTabImageRect(int index)
            {
                Rectangle innerRect = mTabRects[index];
                int imgHeight = mImageSize.Height;
                Rectangle imgRect = new(new Point(innerRect.X + mImagePadding, innerRect.Y + ((innerRect.Height - imgHeight) / 2)), mImageSize);

                if (index == mSelectedIndex)
                {
                    imgRect.Offset(0, -mSelectedTabPadding);
                }

                return imgRect;
            }

            private Rectangle GetTabTextRect(int index, bool hasImage)
            {
                Rectangle innerRect = mTabRects[index];
                Rectangle textRect;
                if (hasImage)
                {
                    int deltaWidth = mImageSize.Width + mImagePadding;
                    textRect = new Rectangle(innerRect.X + deltaWidth, innerRect.Y, innerRect.Width - deltaWidth, innerRect.Height);
                }
                else
                {
                    textRect = innerRect;
                }

                if (index == mSelectedIndex)
                {
                    textRect.Offset(0, -mSelectedTabPadding);
                }

                return textRect;
            }

            private Brush GetBackgroundBrush()
            {
                if (ControlEnabled)
                    return new SolidBrush(mBackColor);
                else
                {
                    Color disabledBackColor;
                    if (mBackColor.DarkOrLight() == "Dark")
                        disabledBackColor = mBackColor.ChangeBrightness(0.3f);
                    else
                        disabledBackColor = mBackColor.ChangeBrightness(-0.3f);
                    return new SolidBrush(disabledBackColor);
                }
            }

            private Brush GetTabBackgroundBrush(int index)
            {
                if (index == mSelectedIndex)
                {
                    if (ControlEnabled)
                        return new SolidBrush(mTabBackColor);
                    else
                    {
                        Color disabledTabBackColor;
                        if (mTabBackColor.DarkOrLight() == "Dark")
                            disabledTabBackColor = mTabBackColor.ChangeBrightness(0.2f);
                        else
                            disabledTabBackColor = mTabBackColor.ChangeBrightness(-0.2f);
                        return new SolidBrush(disabledTabBackColor);
                    }
                }
                else
                {
                    if (ControlEnabled)
                    {
                        bool isHighlighted = mTabRects[index].Contains(mMouseCursor);

                        Color highlightedTabBackColor;
                        if (mTabBackColor.DarkOrLight() == "Dark")
                            highlightedTabBackColor = mTabBackColor.ChangeBrightness(0.2f);
                        else
                            highlightedTabBackColor = mTabBackColor.ChangeBrightness(-0.2f);

                        return isHighlighted ? new SolidBrush(highlightedTabBackColor) : new SolidBrush(mBackColor);
                    }
                    else
                    {
                        Color disabledBackColor;
                        if (mBackColor.DarkOrLight() == "Dark")
                            disabledBackColor = mBackColor.ChangeBrightness(0.3f);
                        else
                            disabledBackColor = mBackColor.ChangeBrightness(-0.3f);
                        return new SolidBrush(disabledBackColor);
                    }
                }
            }

            private static Color GetTextColor()
            {
                if (ControlEnabled)
                    return mForeColor;
                else
                {
                    Color disabledForeColor;
                    if (mForeColor.DarkOrLight() == "Dark")
                        disabledForeColor = mForeColor.ChangeBrightness(0.2f);
                    else
                        disabledForeColor = mForeColor.ChangeBrightness(-0.2f);
                    return disabledForeColor;
                }
            }

            private static Pen GetBorderPen()
            {
                if (ControlEnabled)
                    return new Pen(mBorderColor, mBorderThickness);
                else
                {
                    Color disabledBorder;
                    if (mBorderColor.DarkOrLight() == "Dark")
                        disabledBorder = mBorderColor.ChangeBrightness(0.3f);
                    else
                        disabledBorder = mBorderColor.ChangeBrightness(-0.3f);
                    return new Pen(disabledBorder, mBorderThickness);
                }
            }
            
        }

    }

}
