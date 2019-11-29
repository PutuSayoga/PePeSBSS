CREATE TABLE [dbo].[AkademikTerakhir]
(
	CalonSiswaId INT,
	NamaSekolah VARCHAR(50) NOT NULL,
	JenisSekolah VARCHAR(5) NOT NULL,
	StatusSekolah VARCHAR(10) NOT NULL,
	AlamatSekolah VARCHAR(100) NOT NULL,
	NoPesertaUn VARCHAR(20) NULL,
	NoSeriSkhun VARCHAR(20) NULL,
	NoSeriIjazah VARCHAR(20) NULL,
	CONSTRAINT [UNQ_CalonSiswaId_AkademikTerakhir] UNIQUE(CalonSiswaId), 
	CONSTRAINT [FK_AkademikTerakhirToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
