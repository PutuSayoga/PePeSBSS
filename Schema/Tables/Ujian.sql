CREATE TABLE [dbo].[Ujian]
(
	AkunPendaftaranId INT,
	SoalId INT,
	WaktuBerakhir SMAllDATETIME,
	Status BIT DEFAULT 0,
	CONSTRAINT [FK_UjianToAkunPendaftaran] 
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id),
	CONSTRAINT [FK_UjianToSoal] 
		FOREIGN KEY (SoalId) REFERENCES Soal(Id),
)
