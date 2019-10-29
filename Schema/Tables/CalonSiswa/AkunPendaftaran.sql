CREATE TABLE [dbo].[AkunPendaftaran]
(
	Id INT,
	CalonSiswaId INT,
	NoPendaftaran CHAR(6) NOT NULL,
	Password VARCHAR(64) NOT NULL,
	JenisPendaftaran VARCHAR(20) NOT NULL,
	Status VARCHAR(20) NOT NULL,
	JadwalTes DATE NOT NULL,
	CONSTRAINT [PK_AkunPendaftaran] PRIMARY KEY (Id),
	CONSTRAINT [FK_AkunPendaftaranToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id),
	CONSTRAINT [UNQ_NoPendaftaran_AkunPendaftaran] UNIQUE(NoPendaftaran)
)
