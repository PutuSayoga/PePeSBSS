CREATE TABLE [dbo].[DataDiri]
(
	CalonSiswaId INT,
	NamaPanggilan VARCHAR(10) NOT NULL,
	IsPerempuan BIT NOT NULL,
	TempatLahir VARCHAR(20) NOT NULL,
	TanggalLahir DATE NOT NULL,
	Alamat VARCHAR(100) NOT NULL,
	Agama VARCHAR(20) NOT NULL,
	Rt VARCHAR(5),
	Rw VARCHAR(5),
	Dusun_Desa_Lurah VARCHAR(20),
	Kecamatan VARCHAR(20),
	Kota_Kabupaten VARCHAR(20),
	KodePos CHAR(5),
	NoTelp VARCHAR(20),
	NoHp VARCHAR(20) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	JumlahSaudara TINYINT,
	AnakKe TINYINT,
	StatusDalamKeluarga VARCHAR(20),
	TinggiBadan TINYINT,
	BeratBadan TINYINT,
	GolDarah VARCHAR(5),
	CitaCita VARCHAR(20),
	Hobi VARCHAR(100),
	RiwayatSakit VARCHAR(100),
	KelainanJasmani VARCHAR(20),
	CONSTRAINT [UNQ_CalonSiswaId_DataDiri] UNIQUE (CalonSiswaId),
	CONSTRAINT [FK_DataDiriToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
