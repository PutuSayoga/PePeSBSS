using BackEnd.Domains;
using System.Collections.Generic;

namespace BackEnd.Abstraction
{
    public interface IStaff
    {
        IEnumerable<Staff> GetAllStaff();
        Staff DetailStaff(int id);
        string AddStaff(Staff newStaff);
        void DeleteStaff(int id);
        void UpdateStaff(Staff newData);
        void AddPanitiaToStaff(Panitia newPanitia);
        void DeletePanitiaFromStaff(int staffId);
    }
}
