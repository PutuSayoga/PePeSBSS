Aplikasi pengelolaan penerimaan siswa (PePeS) berbasis web. Aplikasi ini bertujuan untuk mempermudah proses penerimaan siswa khususnya pada saat penerimaan siswa baru. PePeS memiliki beberapa modul, yaitu: modul penerimaan siswa, pengelolaan soal, ujian online, seleksi, dan pengeloaan staff. 

cara menjalankan:
1. publish schema & ubah launchSettings.json sesuai kebutuhan
2. tambahkan data admin pada table staff dengan password: DpImMkNOrsU=   =>  12345

==== sampai sini sudah bisa masuk sebagai staff ====

3. tambahkan 1 data awal pada tabel calon siswa
4. tambahkan 5 data awal pada tabel AkunPendaftaran
NoPendaftaran   Pass            JalurPendaftaran    Status
200100          DpImMkNOrsU=	Reguler	            Admin
200200	        DpImMkNOrsU=	Khusus          	Admin
200300	        DpImMkNOrsU=	Mitra	            Admin
200400	        DpImMkNOrsU=	Prestasi	        Admin
200500	        DpImMkNOrsU=	Mutasi	            Admin

==== ini untuk auto generate NoPendaftaran (NoPendaftaran terakhir+1) ====

5. {localHost}/Auth/LoginStaff untuk login sebagai staff

Demo
Calon Siswa:
https://pepes-webapp.azurewebsites.net/Auth/LoginCalonSiswa
https://pepes-webapp.azurewebsites.net/Ujian

Staff:
https://pepes-webapp.azurewebsites.net/Auth/LoginStaff

role: admin
username: admin
pass: 12345