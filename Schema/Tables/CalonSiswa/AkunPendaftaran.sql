CREATE TABLE [dbo].[AkunPendaftaran]
(
	Id INT IDENTITY(1,1),
	CalonSiswaId INT NOT NULL,
	NoPendaftaran CHAR(6) NOT NULL,
	Password VARCHAR(64) NOT NULL,
	JalurPendaftaran VARCHAR(20) NOT NULL,
	Status VARCHAR(20) NOT NULL DEFAULT 'Daftar Baru',
	JadwalTes DATE NOT NULL,
	CONSTRAINT [PK_AkunPendaftaran] PRIMARY KEY (Id),
	CONSTRAINT [FK_AkunPendaftaranToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id),
	CONSTRAINT [UNQ_NoPendaftaran_AkunPendaftaran] UNIQUE(NoPendaftaran)
)
