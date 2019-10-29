CREATE TABLE [dbo].[HasilTesWawancara]
(
	SoalId INT,
	IndexPertanyaan TINYINT,
	AkunPendaftaranId INT,
	Jawaban VARCHAR(255),
	CONSTRAINT [FK_HasilTesWawancaraToPertanyaan]
		FOREIGN KEY (SoalId, IndexPertanyaan) REFERENCES Pertanyaan(SoalId, IndexPertanyaan),
	CONSTRAINT [FK_HasilTesWawancaraToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
