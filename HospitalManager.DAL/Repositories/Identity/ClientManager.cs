using HospitalManager.DAL.EF;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Interfaces;

namespace HospitalManager.DAL.Repositories.Identity
{
    public class ClientManager : IClientManager
    {
        public DatabaseContext Database { get; set; }
        public ClientManager(DatabaseContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
