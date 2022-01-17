# ONL�NE S�TE Y�NET�M� (OSY)

* Online Site Y�netimi projesi, bir sitenin y�neticilerinin ve daire sakinlerinin bulu�tu�u ortak bir platformdur. 

  * Bu projede; �lk �nce bir y�netici atan�r. Ve y�netici, y�netimini �stlendi�i sitenin blok, daire, daire sakini bilgilerini girer.
  * Daire sakini sisteme kay�t a�amas�nda isim, soyisim, email ve tc bilgisi girer.
  * Ve arka tarafta, y�neticinin veritaban�na girdi�i daire sakini bilgisi ile kay�t olan daire sakini bilgisi e�le�iyorsa; daire sakininin mailine otomatik �ifre atan�r.
	Ve bunun bilgisi daire sakinine verilir. Art�k kullan�c� maili ve �ifresiyle sisteme giri� yapabilir.
  * Mail g�nderimi; veritaban�nda daire sakini tablosunda ho�geldiniz maili bilgisine bak�larak her dk kontrol edilip g�nderilmeyen maillere hangfire ile sa�lan�r.
	(Mail g�nderim i�lemim, yandexte a�t���m hesab�m�n yetki yetersizli�inden kaynakl� ger�ekle�medi. E�itimdeki ba�ka bir arkada��m�n ayn� �ekilde olu�turdu�u mail hesab�
	ile sa�land�, �rnek g�rsel a�a��ya eklendi)

	<br>
	<div align="center"><img src="OSY.Service/Images/mail.PNG" alt="mail-image"></div>
	<br>

  * JWT token yap�s� ile authorization sa�lanm��t�r.
  * Y�netici, site y�netimine gelen faturay�, elektrik, su ve do�algaz olarak topluca daire yans�tabilir. Bunun yap�s�n� ise �rneklendirerek a��klayacak olursam; Mesela ayl�k 
	3000 TL elektrik faturas� geldi siteye. Toplam daire say�s� da 30 adet. Gelen toplam tutar� toplam daire say�s�na b�l�nerek daire ba��na d��en elektrik faturas� 100 TL olarak
	otomatik hesaplan�r. Y�netici; fatura t�r�n�, toplam tutar�, tarih bilgisini ve �deme bilgisini girer ve t�m daireler i�in elektrik faturas� topluca atan�r.
  * Aidat da bir fatura t�r�d�r fakat hesaplamas� daire t�rlerine g�re de�i�ken olarak hesaplan�r. Yine �rneklendirecek olursam; Sitede 30 daire var. 10 tanesi 2+1, 10 tanesi 3+1,
	10 tanesi 4+1 olsun. Y�lba��nda toplam �denecek aidat �creti belirlenmi�tir. Y�netici toplam tutar� girer. Toplam aidat tutar� 12 ye b�l�nerek, sitenin �demesi gereken ayl�k
	tutar bulunur. Ayl�k tutar da, toplam daire say�s�na b�l�nerek daire ba��na ayl�k �denecek aidat tutar� bulunur. 3+1'lere bulunan daire ba��na ayl�k aidat tutar�, 2+1'lere 100 TL
	eksi�i ve 4+1'lere 100 TL fazlas� olarak atan�r. B�ylece daire t�rlerine g�re do�ru orant�da aidat �cretlendirmesi yap�lm�� olur. Aidat atamas� da y�netici taraf�ndan, 
	fatura t�r�, toplam tutar, tarih bilgisi ve �deme bilgisi girilerek t�m dairelere topluca yap�l�r.
  * Daire sakini, gelen t�m faturalar� topluca �deyebilir. Bunun i�in mongoDB ba�lant�s� kurulmu�tur. Kullan�c� kredi kart� numaras�n�, CVV bilgisini, daire bilgisini ve fatura 
	t�r�n� girerek; o faturaya ait �denmemi� t�m bor�lar�n� �deyebilir. Bunun yap�s� da arka tarafta mongoDB ye kredi kart� bilgisi eklenerek, MSSQL de fatura tablosunda hangi
	dairenin hangi fatura t�r� ise �deme bilgisi true d�nerek sa�lan�r.
  * Ayr�ca y�netici, t�m faturalar�, �denmi� faturalar� ve �denmemi� faturalar� ayr� olarak listeleyebilir.