CREATE TABLE [dbo].[RangkumanTesAkademik]
(
	AkunPendaftaranId INT,
	NilaiMipa FLOAT DEFAULT 0,
	NilaiIps FLOAT DEFAULT 0,
	NilaiTpa FLOAT DEFAULT 0,
	CONSTRAINT [FK_HasilTesAkademikToAkunPendaftaran]
		FOREIGN KEY (AkunPendaftaranId) REFERENCES AkunPendaftaran(Id)
)
