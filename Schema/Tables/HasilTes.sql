CREATE TABLE [dbo].[HasilTes]
(
	SoalId INT,
	PertanyaanId INT,
	AkunPendaftaranId INT,
	Jawaban VARCHAR(255),
	Nilai BIT DEFAULT 0 NOT NULL,
	CONSTRAINT [FK_HasilTesToPertanyaan]
		FOREIGN KEY (SoalId, PertanyaanId) REFERENCES Pertanyaan(SoalId, Id),
	CONSTRAINT [FK_HasilTesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
