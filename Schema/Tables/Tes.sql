CREATE TABLE [dbo].[Tes]
(
	AkunPendaftaranId INT NOT NULL,
	SoalId INT NOT NULL,
	TanggalTes DATE NOT NULL,
	CONSTRAINT [FK_TesToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id),
	CONSTRAINT [FK_TesToSoal]
		FOREIGN KEY (SoalId) REFERENCES Soal(Id)
)
