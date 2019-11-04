CREATE TABLE [dbo].[Pertanyaan]
(
	SoalId INT,
	IndexPertanyaan TINYINT NOT NULL,
	BadanPertanyaan VARCHAR(255) NOT NULL,
	Pilihan VARCHAR(512),
	JawabanBenar TINYINT,
	CONSTRAINT [FK_PertanyaanToSoal]
		FOREIGN KEY (SoalId) REFERENCES Soal(Id)
		ON DELETE CASCADE,
	CONSTRAINT [CK_Pertanyaan] PRIMARY KEY(SoalId, IndexPertanyaan)
)
