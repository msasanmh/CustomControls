using MsmhTools;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
/*
* Copyright MSasanMH, May 16, 2022.
*/

namespace CustomControls
{
    public class CustomPanel : Panel
    {
        private static class Methods
        {
            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
            private extern static int SetWindowTheme(IntPtr controlHandle, string appName, string idList);
            internal static void SetDarkControl(Control control)
            {
                _ = SetWindowTheme(control.Handle, "DarkMode_Explorer", null);
                foreach (Control c in control.Controls)
                {
                    _ = SetWindowTheme(c.Handle, "DarkMode_Explorer", null);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle { get; set; }

        private BorderStyle mBorder = BorderStyle.FixedSingle;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Category("Appearance"), Description("Border Style")]
        public BorderStyle Border
        {
            get { return mBorder; }
            set
            {
                mBorder = value;
                Invalidate();
            }
        }

        private ButtonBorderStyle mButtonBorderStyle = ButtonBorderStyle.Solid;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Category("Appearance"), Description("Button Border Style")]
        public ButtonBorderStyle ButtonBorderStyle
        {
            get { return mButtonBorderStyle; }
            set
            {
                if (mButtonBorderStyle != value)
                {
                    mButtonBorderStyle = value;
                    Invalidate();
                }
            }
        }

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

        private Color mBorderColor = Color.Red;
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

        private static Color[]? OriginalColors;
        private static Color BackColorDisabled;
        private static Color ForeColorDisabled;
        private static Color BorderColorDisabled;
        private static bool ApplicationIdle = false;
        private bool once = true;

        private readonly Panel innerPanel = new();

        public CustomPanel() : base()
        {
            SetStyle(ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false); // Fixes Flickers

            BorderStyle = BorderStyle.None;
            ButtonBorderStyle = ButtonBorderStyle.Solid;

            Controls.Add(innerPanel);

            Application.Idle += Application_Idle;
            HandleCreated += CustomPanel_HandleCreated;
            Paint += CustomPanel_Paint;
            EnabledChanged += CustomPanel_EnabledChanged;
            Invalidated += CustomPanel_Invalidated;
            ControlAdded += CustomPanel_ControlAdded;
            ControlRemoved += CustomPanel_ControlRemoved;
            Enter += CustomPanel_Enter;
            MouseEnter += CustomPanel_MouseEnter;
            MouseLeave += CustomPanel_MouseLeave;
            MouseWheel += CustomPanel_MouseWheel;
            ParentChanged += CustomPanel_ParentChanged;
            Resize += CustomPanel_Resize;
            Scroll += CustomPanel_Scroll;
            SizeChanged += CustomPanel_SizeChanged;
        }

        private void Application_Idle(object? sender, EventArgs e)
        {
            ApplicationIdle = true;
            if (Parent != null)
            {
                if (once == true)
                {
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

        private void TopParent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void Parent_Move(object? sender, EventArgs e)
        {
            Invalidate();
        }

        private void CustomPanel_HandleCreated(object? sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                if (c is not Panel)
                    c.BringToFront();
                foreach (Control c2 in c.Controls)
                {
                    if (c2 is not Panel)
                        c2.BringToFront();
                }
            }

            // Timer is needed in some rare cases.
            var p = sender as Panel;
            int totalTime = 500;
            int elapsedTime = 0;
            var t = new System.Windows.Forms.Timer();
            t.Interval = 100;
            t.Tick += (s, e) =>
            {
                OriginalColors = new Color[] { mBackColor, mForeColor, mBorderColor };
                p.Invalidate();
                elapsedTime += t.Interval;
                if (elapsedTime > totalTime)
                    t.Stop();
            };
            t.Start();
        }

        private void CustomPanel_Paint(object? sender, PaintEventArgs e)
        {
            // Update Colors
            OriginalColors = new Color[] { BackColor, ForeColor, BorderColor };

            if (ApplicationIdle == false)
                return;

            Rectangle rect = new(0, 0, ClientRectangle.Width, ClientRectangle.Height);

            if (sender is Panel panel)
            {
                if (DesignMode)
                {
                    BorderStyle = BorderStyle.FixedSingle;
                    //rect = new(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2); // Make border noticeable in DesignMode.
                    BackColor = mBackColor;
                    ForeColor = mForeColor;
                    BorderColor = mBorderColor;
                }
                else
                {
                    BorderStyle = BorderStyle.None;

                    if (OriginalColors == null)
                        return;

                    if (panel.Enabled)
                    {
                        BackColor = OriginalColors[0];
                        ForeColor = OriginalColors[1];
                        BorderColor = OriginalColors[2];
                    }
                    else
                    {
                        // Disabled Colors
                        if (BackColor.DarkOrLight() == "Dark")
                            BackColorDisabled = OriginalColors[0].ChangeBrightness(0.3f);
                        else if (BackColor.DarkOrLight() == "Light")
                            BackColorDisabled = OriginalColors[0].ChangeBrightness(-0.3f);

                        if (ForeColor.DarkOrLight() == "Dark")
                            ForeColorDisabled = OriginalColors[1].ChangeBrightness(0.2f);
                        else if (ForeColor.DarkOrLight() == "Light")
                            ForeColorDisabled = OriginalColors[1].ChangeBrightness(-0.2f);

                        if (BorderColor.DarkOrLight() == "Dark")
                            BorderColorDisabled = OriginalColors[2].ChangeBrightness(0.3f);
                        else if (BorderColor.DarkOrLight() == "Light")
                            BorderColorDisabled = OriginalColors[2].ChangeBrightness(-0.3f);
                    }
                }

                Color backColor;
                Color foreColor;
                Color borderColor;

                if (panel.Enabled)
                {
                    backColor = BackColor;
                    foreColor = ForeColor;
                    borderColor = BorderColor;
                }
                else
                {
                    backColor = BackColorDisabled;
                    foreColor = ForeColorDisabled;
                    borderColor = BorderColorDisabled;
                }
                ForeColor = foreColor;

                innerPanel.BackColor = backColor;
                innerPanel.ForeColor = foreColor;

                if (DesignMode || !DesignMode)
                {
                    // Fill Background
                    e.Graphics.Clear(backColor);

                    // Draw Border
                    //ControlPaint.DrawBorder(e.Graphics, rect, borderColor, ButtonBorderStyle);

                    if (Border == BorderStyle.FixedSingle)
                        ControlPaint.DrawBorder(e.Graphics, rect, borderColor, ButtonBorderStyle);
                    else if (Border == BorderStyle.Fixed3D)
                    {
                        Color secondBorderColor;
                        if (borderColor.DarkOrLight() == "Dark")
                            secondBorderColor = borderColor.ChangeBrightness(0.5f);
                        else
                            secondBorderColor = borderColor.ChangeBrightness(-0.5f);

                        Rectangle rect3DBorder;

                        rect3DBorder = new(rect.X, rect.Y, rect.Width, rect.Height);
                        ControlPaint.DrawBorder(e.Graphics, rect3DBorder, secondBorderColor, ButtonBorderStyle);

                        rect3DBorder = new(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 1);
                        ControlPaint.DrawBorder(e.Graphics, rect3DBorder, secondBorderColor, ButtonBorderStyle);

                        rect3DBorder = new(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                        ControlPaint.DrawBorder(e.Graphics, rect3DBorder, borderColor, ButtonBorderStyle);
                    }
                }
            }
        }

        private void CustomPanel_EnabledChanged(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            if (p.Enabled)
                innerPanel.Enabled = true;
            else
                innerPanel.Enabled = false;
            p.Invalidate();
        }

        private void CustomPanel_Invalidated(object? sender, InvalidateEventArgs e)
        {
            var p = sender as Panel;

            if (!DesignMode && AutoScroll)
            {
                innerPanel.AutoScroll = true;
                AutoScroll = false;
            }

            innerPanel.AutoScrollMargin = p.AutoScrollMargin;
            innerPanel.AutoScrollMinSize = p.AutoScrollMinSize;
            innerPanel.AutoScrollOffset = p.AutoScrollOffset;

            if (Border == BorderStyle.FixedSingle)
            {
                innerPanel.Location = new(1, 1);
                innerPanel.Width = ClientRectangle.Width - 2;
                innerPanel.Height = ClientRectangle.Height - 2;
            }
            else if (Border == BorderStyle.Fixed3D)
            {
                innerPanel.Location = new(2, 2);
                innerPanel.Width = ClientRectangle.Width - 4;
                innerPanel.Height = ClientRectangle.Height - 4;
            }
            else
            {
                innerPanel.Location = new(0, 0);
                innerPanel.Width = ClientRectangle.Width;
                innerPanel.Height = ClientRectangle.Height;
            }

            if (BackColor.DarkOrLight() == "Dark")
                Methods.SetDarkControl(innerPanel);

            foreach (Control c in p.Controls)
                c.Invalidate();
        }

        private void CustomPanel_ControlAdded(object? sender, ControlEventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
            if (!DesignMode)
            {
                p.Controls.Remove(e.Control);
                innerPanel.Controls.Add(e.Control);
            }
        }

        private void CustomPanel_ControlRemoved(object? sender, ControlEventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
            if (!DesignMode)
                innerPanel.Controls.Remove(e.Control);
        }

        private void CustomPanel_Enter(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_MouseEnter(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_MouseLeave(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_ParentChanged(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_Resize(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_Scroll(object? sender, ScrollEventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }

        private void CustomPanel_SizeChanged(object? sender, EventArgs e)
        {
            var p = sender as Panel;
            p.Invalidate();
        }
    }
}
