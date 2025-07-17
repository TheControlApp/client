namespace ControlApp
{
    public partial class Webform : Form
    {
        public Webform(string Url)
        {
            WebBrowser wb = new WebBrowser();
            {
                wb.Navigate(Url);
                wb.Dock = DockStyle.Fill;
            }
            Controls.Add(wb);
            InitializeComponent();
        }
    }

}
