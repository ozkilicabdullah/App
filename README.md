# Proje ayağa kaldırma
"App.API" projesi üzerinden
1-> appsettings.json dosyası içerisinde "SqlConnection bilgisi eklenmelidir."
2-> "ConfirmationMailUIRoute" ve "ForgetMyPasswordMailUIRoute"  parametresleri için UI projesinin domaini düzenlenmelidir.

Db oluşturma
1-> Başlangıç projesi "APP.API" seçilmelidir. "App.API" projesi üzerine gelip mouse ile sağ tıklayıp "Set as Startup project" / "Başlangıç projesi olarak ayarla" seçeneğine tıklanmalıdır.
2-> "Package manager console" açılarak "Default project" seçeneği "App.Repositoru" seçilmeli ve aşağıdaki iki kod sırası ile çalıştırılmalıdır;(View-Other Windows-Packaga Manager)
	* "add-migration initial"
	* "update-database"
3-> Solution(Çözüm) üzerine sağ tıklayıp "Set Start up Project" e tıklanmalı. Açılan ekranda "Multi startup projects" seçilmelidir. "App.API"  ve "APP.WebUI" projeleri "Action" Sekmesinden "Start"  seçilmelidir.

Not: Outlook hesap güvenliği sebebi ile email gönderimi belirli bir süre sonra engellemektedir. mail gönderimi yapılmaz ise 'EmailSettings' tablosu üzerinden POP|IMAP etkinleştirilmiş bir email hesap bilgisi ile güncellenebilir.
