using System.Collections.Generic;
using HospitalManager.BLL.DTO;

namespace HospitalManager.BLL.Interfaces
{
    public interface ITempService
    {
        TempDto Get(int id);
        IEnumerable<TempDto> GetAll();
        void Create(TempDto tempDto);
        void Edit(TempDto tempDto);
        void Delete(int id);
    }
}
