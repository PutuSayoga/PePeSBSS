CREATE TABLE [dbo].[HasilTesAkademik]
(
	AkunPendaftaranId INT,
	NilaiMipa FLOAT,
	NilaiIps FLOAT,
	NilaiTpa Float,
	CONSTRAINT [FK_HasilTesAkademikToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
