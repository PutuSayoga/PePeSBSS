﻿CREATE TABLE [dbo].[Rapor]
(
	CalonSiswaId INT NOT NULL,
	MataPelajaran VARCHAR(50) NOT NULL,
	Semester1 FLOAT,
	Semester2 FLOAT,
	Semester3 FLOAT,
	Semester4 FLOAT,
	Semester5 FLOAT,
	CONSTRAINT [FK_RaporToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
