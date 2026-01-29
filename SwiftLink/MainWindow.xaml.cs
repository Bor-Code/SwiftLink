using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SwiftLink
{
	public partial class MainWindow : Window
	{
		public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<Device>();
		NetworkManager _net = new NetworkManager();

		public MainWindow()
		{
			InitializeComponent();
			DeviceList.ItemsSource = Devices;

			_net.DeviceFound += (d) => Dispatcher.Invoke(() => {
				if (d.Name == Environment.MachineName) return;

				if (!Devices.Any(x => x.Name == d.Name))
				{
					Devices.Add(d);
				}
			});

			_net.FileReceived += (fileName) => Dispatcher.Invoke(() => {
				System.Media.SystemSounds.Asterisk.Play();
				MessageBox.Show($"Dosya alındı: {fileName}", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
			});

			Task.Run(() => _net.StartDiscovery());
			Task.Run(() => _net.StartFileListener());
		}

		private async void SendFile_Click(object sender, RoutedEventArgs e)
		{
			if (DeviceList.SelectedItem is Device d)
			{
				var dialog = new Microsoft.Win32.OpenFileDialog();
				if (dialog.ShowDialog() == true)
				{
					try
					{
						StatusText.Text = $"Gönderiliyor: {System.IO.Path.GetFileName(dialog.FileName)}";
						TransferProgress.Value = 0;
						var progress = new Progress<double>(p => TransferProgress.Value = p);

						((Button)sender).IsEnabled = false;

						await _net.SendFile(d.IP, dialog.FileName, progress);

						StatusText.Text = "Tamamlandı!";
						MessageBox.Show("Dosya gönderildi.", "Başarılı");
						TransferProgress.Value = 0;
					}
					catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
					finally { ((Button)sender).IsEnabled = true; }
				}
			}
			else { MessageBox.Show("Lütfen listeden bir cihaz seçin!", "Uyarı"); }
		}
	}
}