CREATE TABLE [dbo].[Penunjang]
(
	CalonSiswaId INT,
	Pembiaya VARCHAR(20) NOT NULL,
	StatusTempatTinggal VARCHAR(20),
	DayaListrik INT,
	JarakTempuh FLOAT,
	WaktuTempuh TINYINT,
	Transportasi VARCHAR(20),
	CONSTRAINT [UNQ_CalonSiswaId_Penunjang] UNIQUE (CalonSiswaId),
	CONSTRAINT [FK_PenunjangToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
