CREATE TABLE [dbo].[Soal]
(
	Id INT,
	Kode VARCHAR(10),
	Judul VARCHAR(50) NOT NULL,
	Kategori VARCHAR(10) NOT NULL,
	Target VARCHAR(20) NOT NULL,
	MaxPertanyaan TINYINT,
	JumlahPertanyaan TINYINT,
	BatasWaktu TINYINT,
	CONSTRAINT [PK_Soal] PRIMARY KEY(Id),
	CONSTRAINT [UNQ_Kode_Soal] UNIQUE(Kode)
)
