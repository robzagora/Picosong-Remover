namespace Picosong.Remover
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Execute.IsEnabled = false;
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            string value = this.Links.Text;

            value = value.Replace(Environment.NewLine, ",");

            string[] values = value.Split(',').Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => @"https://picosong.com/delete/" + v.Split('/').Last()).ToArray();

            foreach (string uri in values)
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadStringAsync(new Uri(uri));
                }
            }

            this.State.Visibility = Visibility.Visible;
        }

        private void Links_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Links.Text.Length == 0)
            {
                this.Execute.IsEnabled = false;
            }
            else
            {
                this.Execute.IsEnabled = true;
            }
        }
    }
}