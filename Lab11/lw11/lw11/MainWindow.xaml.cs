using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lw11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch=new Stopwatch();
            output.Text = "";
            stopwatch.Start();
            byte[] hash = Encoding.ASCII.GetBytes(input.Text);
            MD5 md5 = new MD5CryptoServiceProvider();
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            byte[] hashenc = md5.ComputeHash(hash);
            stopwatch.Stop();
            startTime.Stop();
            //var resultTime = startTime.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            //                                    resultTime.Hours,
            //                                    resultTime.Minutes,
            //                                    resultTime.Seconds,
            //                                    resultTime.Milliseconds);
            foreach (var b in hashenc)
            {
                output.Text += b.ToString("x2");
            }
            time.Text = stopwatch.ElapsedMilliseconds.ToString(); 
        }
    }
}

