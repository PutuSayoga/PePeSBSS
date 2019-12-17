CREATE TABLE [dbo].[HasilTes]
(
	SoalId INT NULL,
	PertanyaanId INT NULL,
	AkunPendaftaranId INT NULL,
	Jawaban VARCHAR(255) NULL,
	IsBenar BIT NULL,
	CONSTRAINT [FK_HasilTesToPertanyaan]
		FOREIGN KEY (SoalId, PertanyaanId) REFERENCES Pertanyaan(SoalId, Id),
	CONSTRAINT [UNQ_HasilTes]
		UNIQUE (SoalId, PertanyaanId, AkunPendaftaranId),
	CONSTRAINT [FK_HasilTesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
