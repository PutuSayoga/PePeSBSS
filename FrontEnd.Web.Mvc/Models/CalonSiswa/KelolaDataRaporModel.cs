using System.Collections.Generic;

namespace FrontEnd.Web.Mvc.Models.CalonSiswa
{
    public class KelolaDataRaporModel
    {
        public KelolaDataRaporModel() => ListRapor = new CrudRapor[11];
        
        public CrudRapor[] ListRapor { get; set; }
    }
    public class CrudRapor
    {
        public string MataPelajaran { get; set; }
        public double? Semester1 { get; set; }
        public double? Semester2 { get; set; }
        public double? Semester3 { get; set; }
        public double? Semester4 { get; set; }
        public double? Semester5 { get; set; }
    }
}
