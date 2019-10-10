using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace ProxySetting
{
    /// <summary>
    /// WdMain.xaml 的交互逻辑
    /// </summary>
    public partial class WdMain : Window
    {

        public WdMain()
        {
            InitializeComponent();
            Title += " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
            Title += " DEBUG";
#endif
            DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1), IsEnabled = true };
            timer.Tick += Timer_Tick;
            Console.WriteLine(Proxy.GetPAC());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Proxy.UsedManualProxy())
            {
                TbIsUseingProxy.Text = $"全局 {Proxy.GetProxyProxyServer()}";
            }
            else if (!string.IsNullOrEmpty(Proxy.GetPAC()))
            {
#pragma warning disable CA1303 // 请不要将文本作为本地化参数传递
                TbIsUseingProxy.Text = "PAC MODE";
            }
            else
            {
                TbIsUseingProxy.Text = "未启用代理";
#pragma warning restore CA1303 // 请不要将文本作为本地化参数传递
            }
        }

        private void BtnStartProxy_Click(object sender, RoutedEventArgs e)
        {
            if (Proxy.UsedManualProxy())
            {
                Proxy.UnsetProxy();
            }
            else
            {
                Proxy.SetProxy(Proxy.GetProxyProxyServer());
            }
        }

        private void CkbIsTopMost_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)CkbIsTopMost.IsChecked;
        }

        private void BtnChangeProxy_Click(object sender, RoutedEventArgs e)
        {
            App.WdChangeProxy.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.WdMain = this;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
