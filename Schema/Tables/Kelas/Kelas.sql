CREATE TABLE [dbo].[Kelas]
(
	Id INT,
	NamaKelas VARCHAR(20) NOT NULL,
	Kategori VARCHAR(3),
	Tingkat TINYINT,
	MaxSiswa TINYINT,
	JumlahSiswa TINYINT,
	CONSTRAINT [PK_Kelas] PRIMARY KEY (Id)
)
