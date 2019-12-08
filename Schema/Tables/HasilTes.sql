CREATE TABLE [dbo].[HasilTes]
(
	SoalId INT NULL,
	PertanyaanId INT NULL,
	AkunPendaftaranId INT NULL,
	Jawaban VARCHAR(255) NULL,
	IsCorrect BIT NULL,
	CONSTRAINT [FK_HasilTesToPertanyaan]
		FOREIGN KEY (SoalId, PertanyaanId) REFERENCES Pertanyaan(SoalId, Id),
	CONSTRAINT [FK_HasilTesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
