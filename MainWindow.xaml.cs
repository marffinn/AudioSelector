using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NAudio.CoreAudioApi;

namespace AudioOutputListApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAudioDevices();
            AdjustWindowSize();
        }

        private void LoadAudioDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            foreach (var device in devices)
            {
                AudioDevicesList.Items.Add(device.FriendlyName);
            }
        }

        private void AdjustWindowSize()
        {
            // Measure the width of each item
            double maxWidth = 0;
            foreach (var item in AudioDevicesList.Items)
            {
                var listBoxItem = new ListBoxItem { Content = item };
                listBoxItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                if (listBoxItem.DesiredSize.Width > maxWidth)
                {
                    maxWidth = listBoxItem.DesiredSize.Width;
                }
            }

            // Add padding for the ListBox and Window border
            const double padding = 40;

            // Set the width of the ListBox and the window
            AudioDevicesList.Width = maxWidth + padding / 2;
            this.Width = maxWidth + padding;

            // Calculate the required height based on the number of items
            const double itemHeight = 20; // Approximate height of a ListBox item
            double totalHeight = (AudioDevicesList.Items.Count * itemHeight) + padding;

            // Set the height of the window
            this.Height = totalHeight;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
