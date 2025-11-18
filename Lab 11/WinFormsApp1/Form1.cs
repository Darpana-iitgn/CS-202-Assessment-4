namespace WinFormsApp1
{
    public partial class EventPlayground : Form
    {
        private EventPublisher pub = new EventPublisher();

        public EventPlayground()
        {
            InitializeComponent();

            // combo box is filled using the designer
            //cmbColors.Items.Clear();
            //cmbColors.Items.AddRange(new string[] { "Red", "Green", "Blue" });
            cmbColors.SelectedIndex = 0;

            // subscribe to events
            pub.ColorChanged += UpdateLabelColor;
            pub.ColorChanged += ShowNotification;

            pub.TextChanged += UpdateLabelText;
        }

        private void UpdateLabelColor(object sender, ColorEventArgs e)
        {
            lblStatus.ForeColor = System.Drawing.Color.FromName(e.ColorName);
        }

        private void ShowNotification(object sender, ColorEventArgs e)
        {
            MessageBox.Show($"Selected color: {e.ColorName}", "Color Changed");
        }

        private void UpdateLabelText(object sender, EventArgs e)
        {
            lblStatus.Text = DateTime.Now.ToString("F");
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            string selected = cmbColors.SelectedItem.ToString();
            pub.RaiseColorChanged(selected);
        }

        private void btnChangeText_Click(object sender, EventArgs e)
        {
            pub.RaiseTextChanged();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
