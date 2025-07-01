namespace ControlApp.Subroutines;

public partial class Spinner : Form
{
    private System.Windows.Forms.Timer timer;
    private int angle = 15;
    Bitmap bitmapImage;
    string bitmapPath = "";
    string[] spinArgs;
    static Random random = new Random();
    private int duration;
    int position;
    public Spinner(params string[] spinArgs)
    {
        duration = random.Next(50, 401);
        this.spinArgs = spinArgs;
        InitializeComponent();
        timer = new System.Windows.Forms.Timer { Interval = 25 }; // Timer for rotation
        timer.Tick += Timer_Tick;
        pictureBox1.Image = bitmapImage;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        resultbox_txt.Text = spinArgs[position];
        if (duration == 0) timer.Stop(); // Stop when slow enough            
        pictureBox1.Image = RotateImage(bitmapImage, angle);
        pictureBox1.Refresh();
        duration--;
        position = (position + 1) % spinArgs.Length;
    }

    private void SpinBtn_Click(object sender, EventArgs e)
    {
        duration = random.Next(1, 101);
        timer.Start();
    }

    private static Bitmap RotateImage(Bitmap bmp, float angle)
    {
        Bitmap rotated = bmp;
        float centerX = bmp.Width / 2.0f;
        float centerY = bmp.Height / 2.0f;

        using Graphics g = Graphics.FromImage(bmp);
        g.TranslateTransform(centerX, centerY);
        g.RotateTransform(angle);
        g.TranslateTransform(-centerX, -centerY);
        g.DrawImage(bmp, new Point(0, 0));
        g.ResetTransform();
        return rotated;
    }

    private void Spinner_Load(object sender, EventArgs e)
    {
        bitmapPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "spinwheels", $"spinner_{spinArgs.Length}_slices.png");
        bitmapImage = new Bitmap(bitmapPath);
        pictureBox1.Refresh(); 
        if (Utils.CheckEnabled("DarkMode"))
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
            foreach (Control control in Controls)
            {
                if (control is Panel)
                {
                    control.BackColor = Color.Black;
                    control.ForeColor = Color.White;
                }
                if (control is Button)
                {
                    control.BackColor = Color.DarkGray;
                    control.ForeColor = Color.White;
                }
            }
        }
    }
}