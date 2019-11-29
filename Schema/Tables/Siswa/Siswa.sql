CREATE TABLE [dbo].[Siswa]
(
	Id INT IDENTITY(1,1),
	CalonSiswaId INT NOT NULL,
	TanggalMasuk DATE NOT NULL,
	KelasId INT,
	Nis CHAR(6) NOT NULL,
	Status VARCHAR(20) NOT NULL DEFAULT 'Aktif',
	CONSTRAINT [PK_Siswa] PRIMARY KEY(Id),
	CONSTRAINT [UNQ_CalonSiswaId_Siswa] UNIQUE(CalonSiswaId),
	CONSTRAINT [UNQ_Nis_Siswa] UNIQUE(Nis),
	CONSTRAINT [FK_SiswaToCalonSiswa]
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id),
	CONSTRAINT [FK_SiswaToKelas]
		FOREIGN KEY (KelasId) REFERENCES Kelas(Id)
)
