CREATE TABLE [dbo].[RangkumanTesAkademik]
(
	AkunPendaftaranId INT,
	NilaiMipa FLOAT,
	NilaiIps FLOAT,
	NilaiTpa FLOAT,
	CONSTRAINT [FK_HasilTesAkademikToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
