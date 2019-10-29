CREATE TABLE [dbo].[CalonSiswa]
(
	Id INT,
	Nik CHAR(16) NOT NULL,
	Nisn CHAR(10) NOT NULL,
	NamaLengkap VARCHAR NOT NULL,
	CONSTRAINT [PK_CalonSiswa] PRIMARY KEY (Id),
	CONSTRAINT [UNQ_Nik_CalonSiswa] UNIQUE(Nik),
	CONSTRAINT [UNQ_Nisn_CalonSiswa] UNIQUE(Nisn)
)
