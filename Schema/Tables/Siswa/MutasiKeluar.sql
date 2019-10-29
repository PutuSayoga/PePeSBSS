CREATE TABLE [dbo].[MutasiKeluar]
(
	SiswaId INT,
	Tujuan VARCHAR(20),
	Alasan VARCHAR(50) NOT NULL,
	Tanggungan VARCHAR(100),
	TanggalKeluar DATE NOT NULL,
	CONSTRAINT [UNQ_SiswaId_MutasiKeluar] UNIQUE(SiswaId),
	CONSTRAINT [FK_MutasiKeluarToSiswa]
		FOREIGN KEY (SiswaId) REFERENCES Siswa(Id)
)
