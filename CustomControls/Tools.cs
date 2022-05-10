using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Data;
using System.Xml.Serialization;
using CustomControls;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MsmhTools
{
    public static class Extensions
    {
        //-----------------------------------------------------------------------------------
        public static void AddVScrollBar(this DataGridView dataGridView, CustomVScrollBar customVScrollBar)
        {
            customVScrollBar.Dock = DockStyle.Right;
            customVScrollBar.Visible = true;
            customVScrollBar.BringToFront();
            dataGridView.Controls.Add(customVScrollBar);
            dataGridView.ScrollBars = ScrollBars.None;
            dataGridView.SizeChanged += (object? sender, EventArgs e) =>
            {
                // To update LargeChange on form resize
                customVScrollBar.LargeChange = dataGridView.DisplayedRowCount(false);
            };
            dataGridView.RowsAdded += (object? sender, DataGridViewRowsAddedEventArgs e) =>
            {
                customVScrollBar.Maximum = dataGridView.RowCount;
                customVScrollBar.LargeChange = dataGridView.DisplayedRowCount(false);
                customVScrollBar.SmallChange = 1;
            };
            dataGridView.Scroll += (object? sender, ScrollEventArgs e) =>
            {
                if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    if (dataGridView.Rows.Count > 0)
                    {
                        customVScrollBar.Value = e.NewValue;
                    }
                }
            };
            customVScrollBar.Scroll += (object? sender, EventArgs e) =>
            {
                if (dataGridView.Rows.Count > 0)
                    if (customVScrollBar.Value < dataGridView.Rows.Count)
                        dataGridView.FirstDisplayedScrollingRowIndex = customVScrollBar.Value;
            };
        }
        //-----------------------------------------------------------------------------------
        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeBrightness(this Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;
            
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
        //-----------------------------------------------------------------------------------
        /// <summary>
        /// Check Color is Light or Dark.
        /// </summary>
        /// <returns>
        /// Returns "Dark" or "Light" as string.
        /// </returns>
        public static string DarkOrLight(this Color color)
        {
            if (color.R * 0.2126 + color.G * 0.7152 + color.B * 0.0722 < 255 / 2)
            {
                return "Dark";
            }
            else
            {
                return "Light";
            }
        }
        //-----------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public static class Tools
    {
        //=======================================================================================
        public static class Controllers
        {
            //-----------------------------------------------------------------------------------
            public static Control GetTopParent(Control control)
            {
                Control parent = control;
                if (control.Parent != null)
                {
                    parent = control.Parent;
                    if (parent.Parent != null)
                        while (parent.Parent != null)
                            parent = parent.Parent;
                }
                return parent;
            }
            //-----------------------------------------------------------------------------------
            public static IEnumerable<Control> GetAllControls(Control control)
            {
                if (control == null)
                    throw new ArgumentNullException(nameof(control));
                return implementation();
                IEnumerable<Control> implementation()
                {
                    foreach (Control control in control.Controls)
                    {
                        foreach (Control child in GetAllControls(control))
                        {
                            yield return child;
                        }
                        yield return control;
                    }
                }
            }
            //-----------------------------------------------------------------------------------
        }
        //=======================================================================================
    }
}
