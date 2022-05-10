using MsmhTools;
namespace CustomControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set MessageBox Colors
            CustomMessageBox.BackColor = Color.DimGray;
            CustomMessageBox.ForeColor = Color.LightBlue;
            CustomMessageBox.BorderColor = Color.Red;

            // Add VScrollBar to DataGridView
            CustomVScrollBar vsb = new();
            // Set Colors
            vsb.BackColor = Color.DimGray;
            vsb.ForeColor = Color.LightBlue;
            vsb.BorderColor = Color.Red;
            customDataGridView1.AddVScrollBar(vsb);

        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            switch (CustomMessageBox.Show("MessageBox Sample", "Title", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    CustomMessageBox.Show("Clicked on Yes");
                    break;
                case DialogResult.No:
                    CustomMessageBox.Show("Clicked on No");
                    break;
                case DialogResult.Cancel:
                    CustomMessageBox.Show("Clicked on Cancel");
                    break;
                default:
                    break;
            }
        }

        private void customButton5_Click(object sender, EventArgs e)
        {
            // ProgressBar
            customProgressBar1.Value = 0;
            customProgressBar1.Minimum = 0;
            customProgressBar1.Maximum = 1000000;
            customProgressBar1.Step = 1;
            customProgressBar1.StartTime = DateTime.Now;

            for (int n = 0; n < 1000001; n++)
            {
                customProgressBar1.Value = n;
                customProgressBar1.CustomText = "Working on " + n;
            }
        }
    }
}