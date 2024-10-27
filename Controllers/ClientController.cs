using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pi_store.DataAccess;
using pi_store.Models;

namespace pi_store.Controllers
{
    public class ClientController
    {
        private ClientDAO clientDAO;

        public ClientController()
        {
            clientDAO = new ClientDAO();
        }

        public List<Client> GetAllClients()
        {
            return clientDAO.GetAllClients();
        }

        public Client GetClientById(string id)
        {
            return clientDAO.GetClientById(id);
        }

        public void AddClient(Client client)
        {
            clientDAO.AddClient(client);
        }

        public void UpdateClient(Client client)
        {
            clientDAO.UpdateClient(client);
        }

        public void DeleteClient(string id)
        {
            clientDAO.DeleteClient(id);
        }

        public List<Client> SearchClient(string searchString)
        {
            return clientDAO.SearchClients(searchString);
        }

        public List<CustomerInfo> GetTopCustomers()
        {
            return clientDAO.GetTopCustomers();
        }
    }
}
