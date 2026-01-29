🚀 SwiftLink - Yerel Ağ Dosya Transfer Aracı
Show Image Show Image Show Image Show Image
SwiftLink, yerel ağınızdaki bilgisayarlar arasında internet bağlantısı olmadan, kablolu bağlantı hızında güvenli dosya aktarımı yapabileceğiniz modern bir Windows programıdır.
Artık USB bellek aramakla uğraşmayacaksınız; cihazlarınız otomatik olarak birbirini buluyor ve dosyalarınızı saniyeler içinde karşı tarafa gönderiyor.

🔥 Temel Özellikler

🔍 Otomatik Keşif (Auto-Discovery): IP adresi falan girmenize gerek yok. Program, UDP Broadcast teknolojisiyle ağdaki diğer SwiftLink kullanıcılarını otomatik olarak buluyor ve listeliyor.
⚡ Maksimum Hız: Dosyalarınız internete yüklenmiyor. Doğrudan cihazdan cihaza aktarıldığı için modeminizin desteklediği en yüksek hızda (Wi-Fi veya Ethernet) transfer gerçekleşiyor.
🔒 Güvenli & Yerel: Verileriniz hiçbir şekilde bulut sunucusuna gitmiyor. Tamamen yerel ağınızda kalıyor.
📦 Kurulumsuz (Portable): .exe dosyasını indirip çalıştırmanız yeterli. Bilgisayarınızda .NET yüklü olmasa bile sorunsuz çalışıyor (Self-contained).
🖼️ Sürükle & Bırak (Yakında): Dosyaları program penceresine sürükleyip bırakarak kolayca gönderebileceksiniz.

🛠️ Teknik Detaylar
Bu projeyi C# ve WPF kullanarak geliştirdim.
Discovery Protokolü: UDP Port 8888 üzerinden broadcast yayını yaparak cihazlar birbirini buluyor.
Transfer Protokolü: TCP Port 9999 üzerinden güvenilir veri aktarımı sağlanıyor.
Arayüz: Modern WPF (Windows Presentation Foundation) mimarisi kullanılıyor.

🚀 Kurulum ve Kullanım
GitHub sayfasının sağ tarafındaki Releases bölümünden en son sürümü (SwiftLink.exe) indirin.
İndirdiğiniz .exe dosyasına çift tıklayın.
Karşı taraftaki bilgisayarda da aynı programı açın.
Listede cihaz ismini gördüğünüzde seçin ve "DOSYA GÖNDER" butonuna tıklayın.

⚠️ Sorun Giderme (Troubleshooting)
Programı kullanırken sorun yaşıyorsanız, büyük ihtimalle Windows Güvenlik Duvarı veya ağ ayarlarından kaynaklanıyordur. İşte en sık karşılaşılan sorunlar ve kesin çözümleri:
🔴 Sorun 1: "Listede diğer bilgisayarı göremiyorum."
Bu sorunun en büyük sebebi Windows Güvenlik Duvarı. Windows, dışarıdan gelen sinyalleri tehdit olarak algılayıp engelleyebiliyor.
Çözüm:
Programı ilk açtığınızda karşınıza çıkan "Erişime İzin Ver" penceresinde hem "Özel Ağlar" hem de "Ortak Ağlar" seçeneklerini işaretleyip onaylayın.
Eğer bu ekranı kaçırdıysanız:
Denetim Masası > Windows Defender Güvenlik Duvarı > Bir uygulamanın güvenlik duvarını geçmesine izin ver kısmına girin.
Listeden SwiftLink'i bulun ve yanındaki iki kutucuğu da (Özel/Ortak) işaretleyin.
🔴 Sorun 2: "Sanal Makine (VMware/VirtualBox) ana bilgisayarı görmüyor."
Sanal makineler genelde "NAT" modunda çalışır. Bu durumda sanal makine farklı bir IP bloğunda oluyor (mesela 192.168.202.x). SwiftLink sadece aynı IP bloğundaki (örneğin 192.168.1.x) cihazları görebiliyor.
Çözüm:
VMware veya VirtualBox ayarlarından Ağ (Network) kısmına girin.
Bağlantı tipini NAT yerine Bridged (Köprü) olarak değiştirin.
Sanal makine içindeki internet bağlantısını bir kapat aç yapın (veya ipconfig /renew komutunu çalıştırın). IP adresinizin ana bilgisayarla aynı aralıkta olduğunu kontrol edin (ikisi de 192.168.1... gibi başlamalı).
🔴 Sorun 3: "Dosya gönderimi %0'da takılı kalıyor veya hata veriyor."
Cihazlar birbirini görüyor ama dosya gitmiyor mu? Muhtemelen TCP portu (9999) engelleniyordur.
Çözüm:
Alıcı bilgisayarda antivirüs programı varsa geçici olarak kapatıp deneyin.
Dosya çok büyükse Wi-Fi sinyal gücünüzü kontrol edin.

💻 Geliştiriciler İçin (Build)
Projeyi kendi bilgisayarınızda derleyip geliştirmek isterseniz:
bash# Repoyu klonlayın
git clone [https://github.com/Bor-Code/SwiftLink.git](https://github.com/Bor-Code/SwiftLink.git)
# Klasöre girin
cd SwiftLink
# Projeyi Visual Studio 2022 ile açın veya terminalden derleyin:
dotnet build

Herhangi bir sorun anında benimle "non.mrbora@gmail.com" üzerinden iletişime geçebilirsinz.

Keyifli kullanımlar-[Bor-Code]

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

🚀 SwiftLink - Local Network File Transfer Tool
Show Image Show Image Show Image Show Image
SwiftLink is a modern Windows program that allows you to securely transfer files at wired connection speeds between computers on your local network without an internet connection.
No more searching for USB drives; your devices automatically find each other and send your files to the other side in seconds.

🔥 Key Features

🔍 Auto-Discovery: No need to enter IP addresses. The program automatically finds and lists other SwiftLink users on the network using UDP Broadcast technology.
⚡ Maximum Speed: Your files are not uploaded to the internet. Since they are transferred directly from device to device, the transfer occurs at the highest speed supported by your modem (Wi-Fi or Ethernet).
🔒 Secure & Local: Your data never goes to a cloud server. It stays entirely on your local network.
📦 Portable (No Installation Required): Simply download and run the .exe file. It works seamlessly even if .NET is not installed on your computer (Self-contained).
🖼️ Drag & Drop (Coming Soon): You will be able to easily send files by dragging and dropping them into the program window.

🛠️ Technical Details
I developed this project using C# and WPF.
Discovery Protocol: Devices find each other by broadcasting over UDP Port 8888.
Transfer Protocol: Reliable data transfer is provided over TCP Port 9999.
Interface: Modern WPF (Windows Presentation Foundation) architecture is used.

🚀 Installation and Usage
Download the latest version (SwiftLink.exe) from the Releases section on the right side of the GitHub page.
Double-click the downloaded .exe file.
Open the same program on the other computer.
When you see the device name in the list, select it and click the “SEND FILE” button.

⚠️ Troubleshooting
If you encounter problems while using the program, it is most likely due to Windows Firewall or network settings. Here are the most common problems and their definitive solutions:
🔴 Problem 1: “I can't see the other computer in the list.”
The main cause of this problem is Windows Firewall. Windows can detect and block incoming signals as threats.
Solution:
When you first open the program, check both the “Private Networks” and “Public Networks” options in the “Allow Access” window that appears and confirm.
If you missed this screen:
Go to Control Panel > Windows Defender Firewall > Allow an app through firewall.
Find SwiftLink in the list and check both boxes (Private/Public) next to it.
🔴 Issue 2: “The host computer does not see the virtual machine (VMware/VirtualBox).”
Virtual machines usually run in “NAT” mode. In this case, the virtual machine is on a different IP block (e.g., 192.168.202.x). SwiftLink can only see devices on the same IP block (e.g., 192.168.1.x).
Solution:
Go to the Network section in the VMware or VirtualBox settings.
Change the connection type from NAT to Bridged.
Disconnect and reconnect the internet connection within the virtual machine (or run the ipconfig /renew command). Verify that your IP address is in the same range as the host computer (both should start with 192.168.1...).
🔴 Issue 3: “File transfer is stuck at 0% or gives an error.”
Do the devices see each other but the file isn't transferring? The TCP port (9999) is probably blocked.
Solution:
If the receiving computer has antivirus software, try temporarily disabling it.
If the file is very large, check your Wi-Fi signal strength.

💻 For Developers (Build)
If you want to compile and develop the project on your own computer:
bash# Clone the repository
git clone [https://github.com/Bor-Code/SwiftLink.git](https://github.com/Bor-Code/SwiftLink.git)
# Enter the directory
cd SwiftLink
# Open the project with Visual Studio 2022 or compile it from the terminal:
dotnet build

If you encounter any issues, feel free to contact me at “non.mrbora@gmail.com”.

Enjoy using it! -[Bor-Code]
