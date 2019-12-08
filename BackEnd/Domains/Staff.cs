namespace BackEnd.Domains
{
    public class Staff
    {
        public int Id { get; set; }
        public string Nip { get; set; }
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string NoHp { get; set; }
        public string Jabatan { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Panitia Panitia { get; set; }
    }
}
