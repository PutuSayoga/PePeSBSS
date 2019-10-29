CREATE TABLE [dbo].[Penanggungjawab]
(
	CalonSiswaId INT,
	Sebagai VARCHAR(5) NOT NULL,
	NamaLengkap VARCHAR(50) NOT NULL,
	TempatLahir VARCHAR(20) NOT NULL,
	TanggalLahir DATE NOT NULL,
	Alamat VARCHAR(100) NOT NULL,
	Agama VARCHAR(20),
	PendidikanTerakhir VARCHAR(20),
	Pekerjaan VARChAR(20),
	Penghasilan INT,
	NoTelp VARCHAR(20),
	NoHp VARCHAR(20),
	Email VARCHAR(50),
	StatusDalamKeluarga VARCHAR(20),
	Keterangan VARCHAR(20),
	CONSTRAINT [FK_PenanggungjawabToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
