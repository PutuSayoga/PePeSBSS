﻿CREATE TABLE [dbo].[Soal]
(
	Id INT IDENTITY(1,1),
	Judul VARCHAR(50) NOT NULL,
	Kategori VARCHAR(10) NOT NULL,
	Jalur varchar(10),
	Target VARCHAR(20),
	JumlahPertanyaan TINYINT DEFAULT 0,
	BatasWaktu TINYINT,
	Deskripsi Varchar(100),
	Status VARCHAR(10) NOT NULL DEFAULT 'ENABLE',
	IsUsed BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Soal] PRIMARY KEY(Id)
)
