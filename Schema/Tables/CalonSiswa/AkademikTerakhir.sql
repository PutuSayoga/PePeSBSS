CREATE TABLE [dbo].[AkademikTerakhir]
(
	CalonSiswaId INT NOT NULL,
	NamaSekolah VARCHAR(50) NOT NULL,
	JenisSekolah VARCHAR(5) NULL,
	StatusSekolah VARCHAR(10) NULL,
	AlamatSekolah VARCHAR(100) NULL,
	NoPesertaUn VARCHAR(20) NULL,
	NoSeriSkhun VARCHAR(20) NULL,
	NoSeriIjazah VARCHAR(20) NULL,
	CONSTRAINT [UNQ_CalonSiswaId_AkademikTerakhir] UNIQUE(CalonSiswaId), 
	CONSTRAINT [FK_AkademikTerakhirToCalonSiswa] 
		FOREIGN KEY (CalonSiswaId) REFERENCES CalonSiswa(Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)
