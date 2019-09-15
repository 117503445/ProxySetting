using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ProxySetting
{
    /// <summary>
    /// WdChangeProxy.xaml 的交互逻辑
    /// </summary>
    public partial class WdChangeProxy : Window
    {
        public WdChangeProxy()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        string path = "Proxys.txt";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProxys();
        }

        private void LstProxys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string proxyServer = ((string)LstProxys.SelectedItem);
            bool isSuccess = Proxy.SetProxy(proxyServer);
            TbInfo.Text = $"{proxyServer} 设置 {(isSuccess ? "成功" : "失败")}";
        }

        private void LoadProxys()
        {
            if (File.Exists(path))
            {
                string[] p = File.ReadAllLines(path);

                LstProxys.ItemsSource = p;
            }
            else
            {
                File.WriteAllText(path, Proxy.GetProxyProxyServer());
                EditFileWithDefaultProgram();
            }
        }
        /// <summary>
        /// 使用默认文本编辑器编辑文件
        /// </summary>
        private void EditFileWithDefaultProgram()
        {
            System.Diagnostics.Process.Start(path);

        }
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProxys();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditFileWithDefaultProgram();
        }
    }
}
