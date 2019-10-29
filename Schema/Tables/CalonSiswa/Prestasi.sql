﻿CREATE TABLE [dbo].[Prestasi]
(
	CalonSiswaId INT,
	NamaKejuaraan VARCHAR(50) NOT NULL,
	Jenis VARCHAR(20) NOT NULL,
	Tingkat VARCHAR(20) NOT NULL,	
	Peringkat VARCHAR(20) NOT NULL,
	Tahun DATE NOT NULL,
	Penyelenggara VARCHAR(20) NOT NULL,
	CONSTRAINT [FK_PrestasiToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
