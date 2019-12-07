CREATE TABLE [dbo].[HasilTes]
(
	SoalId INT NULL,
	PertanyaanId INT NULL,
	AkunPendaftaranId INT NULL,
	Jawaban VARCHAR(255) NULL,
	Nilai BIT DEFAULT 0 NULL,
	CONSTRAINT [FK_HasilTesToPertanyaan]
		FOREIGN KEY (SoalId, PertanyaanId) REFERENCES Pertanyaan(SoalId, Id),
	CONSTRAINT [FK_HasilTesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
