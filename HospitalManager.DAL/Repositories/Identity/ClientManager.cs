using System.Linq;
using HospitalManager.DAL.EF;
using HospitalManager.DAL.Entities.Identity;
using HospitalManager.DAL.Interfaces;
using System.Data.Entity;

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

        public ClientProfile Get(string id)
        {
            var clientProfile = Database.ClientProfiles.Find(id);

            return clientProfile;
        }

        public IQueryable<ClientProfile> GetAll()
        {
            var clientProfiles = Database.ClientProfiles;
            
            return clientProfiles;
        }
    }
}