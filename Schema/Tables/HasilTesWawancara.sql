CREATE TABLE [dbo].[HasilTesWawancara]
(
	SoalId INT,
	PertanyaanId INT,
	AkunPendaftaranId INT,
	Jawaban VARCHAR(255),
	CONSTRAINT [FK_HasilTesWawancaraToPertanyaan]
		FOREIGN KEY (SoalId, PertanyaanId) REFERENCES Pertanyaan(SoalId, Id),
	CONSTRAINT [FK_HasilTesWawancaraToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
