CREATE TABLE [dbo].[Tes]
(
	AkunPendaftaranId INT,
	SoalId INT,
	TanggalTes DATE NOT NULL,
	CONSTRAINT [FK_TesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id),
	CONSTRAINT [FK_TesToSoal]
		FOREIGN KEY (SoalId) REFERENCES Soal(Id)
)
