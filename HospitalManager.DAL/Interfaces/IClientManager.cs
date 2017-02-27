using System;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
