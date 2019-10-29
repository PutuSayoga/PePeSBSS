using BackEnd.Domains;
using System.Collections.Generic;

namespace BackEnd.IServices
{
    public interface IStaff
    {
        IEnumerable<Staff> GetAllStaff();
        Staff DetailStaff(int id);
        string AddStaff(Staff newStaff);
        int DeleteStaff(int id);
        int UpdateStaff(Staff newData);
        int AddPanitiaToStaff(int staffId, Panitia newPanitia);
        int DeletePanitiaFromStaff(int staffId);
    }
}
