using System;
using System.Linq;
using HospitalManager.DAL.Entities.Identity;

namespace HospitalManager.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        ClientProfile Get(string id);

        void Create(ClientProfile item);

        IQueryable<ClientProfile> GetAll();
    }
}