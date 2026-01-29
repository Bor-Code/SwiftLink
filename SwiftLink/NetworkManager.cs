using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SwiftLink
{
	public class NetworkManager
	{
		private const int DiscoveryPort = 8888;
		private const int TransferPort = 9999;

		public event Action<Device>? DeviceFound;
		public event Action<string>? FileReceived;

		private string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					string ipStr = ip.ToString();
					if (ipStr.StartsWith("192.168.") || ipStr.StartsWith("10."))
					{
						return ipStr;
					}
				}
			}
			return "127.0.0.1";
		}

		public async Task StartDiscovery()
		{
			_ = Task.Run(async () => {
				string localIP = GetLocalIPAddress();
				string broadcastIP = localIP.Substring(0, localIP.LastIndexOf('.') + 1) + "255";

				using UdpClient client = new UdpClient();
				client.EnableBroadcast = true;
				client.Client.Bind(new IPEndPoint(IPAddress.Parse(localIP), 0));

				var endpoint = new IPEndPoint(IPAddress.Parse(broadcastIP), DiscoveryPort);

				while (true)
				{
					byte[] data = Encoding.UTF8.GetBytes($"SwiftLink|{Environment.MachineName}");
					await client.SendAsync(data, data.Length, endpoint);
					await Task.Delay(3000);
				}
			});

			using UdpClient listener = new UdpClient(DiscoveryPort);
			while (true)
			{
				var result = await listener.ReceiveAsync();
				string msg = Encoding.UTF8.GetString(result.Buffer);
				if (msg.StartsWith("SwiftLink|"))
				{
					string name = msg.Split('|')[1];
					string ip = result.RemoteEndPoint.Address.ToString();
					DeviceFound?.Invoke(new Device { Name = name, IP = ip });
				}
			}
		}

		public async Task SendFile(string ip, string path, IProgress<double> progress)
		{
			using TcpClient client = new TcpClient();
			await client.ConnectAsync(ip, TransferPort);
			using NetworkStream ns = client.GetStream();
			using FileStream fs = File.OpenRead(path);

			byte[] nameBytes = Encoding.UTF8.GetBytes(Path.GetFileName(path));
			ns.Write(BitConverter.GetBytes(nameBytes.Length), 0, 4);
			ns.Write(nameBytes, 0, nameBytes.Length);
			ns.Write(BitConverter.GetBytes(fs.Length), 0, 8);

			byte[] buffer = new byte[1024 * 1024];
			long totalSent = 0;
			int bytesRead;

			while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
			{
				await ns.WriteAsync(buffer, 0, bytesRead);
				totalSent += bytesRead;
				progress?.Report((double)totalSent / fs.Length * 100);
			}
		}

		public async Task StartFileListener()
		{
			TcpListener listener = new TcpListener(IPAddress.Any, TransferPort);
			listener.Start();
			while (true)
			{
				using TcpClient client = await listener.AcceptTcpClientAsync();
				using NetworkStream ns = client.GetStream();

				byte[] nameLenBytes = new byte[4];
				await ns.ReadExactlyAsync(nameLenBytes, 0, 4);
				int nameLen = BitConverter.ToInt32(nameLenBytes, 0);

				byte[] nameBytes = new byte[nameLen];
				await ns.ReadExactlyAsync(nameBytes, 0, nameLen);
				string fileName = Encoding.UTF8.GetString(nameBytes);

				byte[] sizeBytes = new byte[8];
				await ns.ReadExactlyAsync(sizeBytes, 0, 8);

				string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
				using FileStream fs = File.Create(savePath);

				byte[] buffer = new byte[1024 * 1024];
				int bytesRead;
				while ((bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length)) > 0)
				{
					await fs.WriteAsync(buffer, 0, bytesRead);
				}

				FileReceived?.Invoke(fileName);
			}
		}
	}
}