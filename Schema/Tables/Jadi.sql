CREATE TABLE [dbo].[Jadi]
(
	SiswaId INT,
	CalonSiswaId INT,
	TanggalMasuk DATE,
	CONSTRAINT [UNQ_SiswaId_Jadi] UNIQUE(SiswaId),
	CONSTRAINT [UNQ_CalonSiswaId_Jadi] UNIQUE(CalonSiswaId),
	CONSTRAINT [FK_JadiToSiswa]
		FOREIGN KEY (SiswaId) REFERENCES Siswa(Id),
	CONSTRAINT [FK_JadiToCalonSiswa]
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
)
