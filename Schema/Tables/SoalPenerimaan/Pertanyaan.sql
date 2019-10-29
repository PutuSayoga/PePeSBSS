CREATE TABLE [dbo].[Pertanyaan]
(
	SoalId INT,
	IndexPertanyaan TINYINT NOT NULL,
	Pertanyaan VARCHAR(255) NOT NULL,
	PathGambar VARCHAR(255),
	Pilihan VARCHAR(255),
	JawabanBenar TINYINT,
	CONSTRAINT [FK_PertanyaanToSoal]
		FOREIGN KEY (SoalId) REFERENCES Soal(Id),
	CONSTRAINT [CK_Pertanyaan] PRIMARY KEY(SoalId, IndexPertanyaan)
)
