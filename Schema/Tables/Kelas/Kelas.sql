﻿CREATE TABLE [dbo].[Kelas]
(
	Id INT IDENTITY(1,1),
	NamaKelas VARCHAR(20) NOT NULL,
	Kategori VARCHAR(3),
	Tingkat TINYINT,
	MaxSiswa TINYINT,
	JumlahSiswa TINYINT DEFAULT(0),
	CONSTRAINT [PK_Kelas] PRIMARY KEY (Id)
)
