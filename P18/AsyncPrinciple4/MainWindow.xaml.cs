using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncPrinciple4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var s = await client.GetStringAsync("https://www.youzack.com");
                MyTextBox.Text = s.Substring(0, 1000);

                // Thread.Sleep(3000);
                // 这里不要用Thread.Sleep，虽然这个方法是异步的，但是一旦调用了Thread.Sleep,
                // 将让调用线程也在这等待，表现为界面卡死
                // 从理论的层次来讲，Thread.Sleep会降低并发

                // 应该用下面的方式进行延时操作
                // 这句话的含义是当前异步方法等待以下，而不是调用线程
                await Task.Delay(3000);


                s = await client.GetStringAsync("https://www.baidu.com");
                MyTextBox.Text = s.Substring(0, 1000);
            }
        }
    }
}
