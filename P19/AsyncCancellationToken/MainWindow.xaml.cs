using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncCancellationToken
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

        // 用来构建CancellationToken
        private CancellationTokenSource _cancellationTokenSource;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // 超时的Token
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.CancelAfter(5000);


            await DownLoad2Async("https://www.baidu.com", 1000, _cancellationTokenSource.Token);
        }


        private async Task DownLoadAsync(string url, int n, CancellationToken token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var s = await httpClient.GetStringAsync(url);
                    MyTextBox.Text += s.Substring(0, 100);

                    // 处理token的方式一：判断标记为，自行处理
                    if (token.IsCancellationRequested)
                    {
                        //await Task.Run(() =>
                        //{

                        //});
                        MessageBox.Show("任务已取消");
                        break;
                    }

                    //// 处理方式二：抛异常
                    //token.ThrowIfCancellationRequested();
                }
            }
        }

        private async Task DownLoad2Async(string url, int n, CancellationToken token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var s = await httpClient.GetAsync(url, token);
                    MyTextBox.Text += (await s.Content.ReadAsStringAsync()).Substring(0, 100);

                    // 用GetAsync传入的token的好处是能够及时地处理取消（抛出异常）
                    // 而用DownLoad2Async时，若网速特别慢，GetStringAsync耗时特别长
                    // 需要await之后才会响应取消请求（尽管我们设置的时5s超时取消）
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 主动Token
            _cancellationTokenSource?.Cancel();
        }
    }
}
