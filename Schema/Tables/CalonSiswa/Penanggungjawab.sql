CREATE TABLE [dbo].[Penanggungjawab]
(
	CalonSiswaId INT NOT NULL,
	Sebagai VARCHAR(5) NOT NULL,
	NamaLengkap VARCHAR(50) NOT NULL,
	TempatLahir VARCHAR(20) NULL,
	TanggalLahir DATE NULL,
	Alamat VARCHAR(100) NULL,
	Agama VARCHAR(20) NULL,
	PendidikanTerakhir VARCHAR(20) NULL,
	Pekerjaan VARChAR(20),
	Penghasilan INT,
	NoTelp VARCHAR(20),
	NoHp VARCHAR(20) NULL,
	Email VARCHAR(50),
	StatusDalamKeluarga VARCHAR(20),
	Keterangan VARCHAR(20),
	CONSTRAINT [FK_PenanggungjawabToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
