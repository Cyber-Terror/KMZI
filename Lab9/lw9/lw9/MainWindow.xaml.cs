using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace lw9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ranec r = new Ranec();
        int[] d2;
        int[] E;
        int[] C;
        int a;
        int n;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            d2 = r.superOrder(9);
            string result = "";
            foreach(var item in d2)
            {
                result += item + " ";
            }
            task1result.Text = result;
        }

        private void bTask2_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            foreach(var di in d2)
            {
                sum += di;
            }
            n = r.getN(sum);
            a = r.getA(n);
            E = r.normalOrder(d2, a, n, d2.Length);
            string result = "";
            foreach (var item in E)
            {
                result += item + " ";
            }
            task2result.Text = result;
        }

        private void encodeB_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            var firstTime = DateTime.Now.Ticks;
            C = r.encode(E, encodeText.Text, d2.Length);
            string result = "";
            foreach (var item in C)
            {
                result += item + " ";
            }
            decodeTexr.Text = result;
            stopwatch1.Stop();
            timeWork.Text = stopwatch1.ElapsedMilliseconds.ToString();
            //((DateTime.Now.Ticks - firstTime)/1000).ToString();
        }

        private void decodeB_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
           
            
            int a_1 = r.a_1(a, n);
            string decodeStr = decodeTexr.Text;
            int[] S = new int[C.Length];
            string M2 = "";

            var firstTime = DateTime.Now.Ticks;
            for (int i = 0; i < C.Length; i++)
            {
                S[i] = (C[i] * a_1) % n;
            }

          
            foreach (int Si in S)
            {
                string M2i = r.decode(d2, Si, d2.Length);
                M2 += M2i + " ";
            }
            //((DateTime.Now.Ticks - firstTime) / 1000).ToString();
            
            M2 = M2.Replace(" ", "");
            var stringArray = Enumerable.Range(0, M2.Length / 8).Select(i => Convert.ToByte(M2.Substring(i * 8, 8), 2)).ToArray();
            var str = Encoding.ASCII.GetString(stringArray);
           // var str = Convert.ToBase64String(stringArray);
            encodeText.Text = str;
           // Console.WriteLine("Time dencr:" + stopwatch1.ElapsedMilliseconds + " ms");
            stopwatch1.Stop();
            timeWork.Text = stopwatch1.ElapsedMilliseconds.ToString();
        }
    }
}
