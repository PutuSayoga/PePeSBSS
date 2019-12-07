CREATE TABLE [dbo].[MutasiKeluar]
(
	SiswaId INT NOT NULL,
	Tujuan VARCHAR(20) NOT NULL,
	Alasan VARCHAR(50) NOT NULL,
	TanggalKeluar DATE NOT NULL,
	CONSTRAINT [UNQ_SiswaId_MutasiKeluar] UNIQUE(SiswaId),
	CONSTRAINT [FK_MutasiKeluarToSiswa]
		FOREIGN KEY (SiswaId) REFERENCES Siswa(Id)
)
