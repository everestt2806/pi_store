using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pi_store.DataAccess;
using pi_store.Models;

namespace pi_store.Controllers
{
    public class OrderController
    {
        private OrderDAO orderDAO;

        public OrderController()
        {
            orderDAO = new OrderDAO();
        }

        public List<Order> GetAllOrders()
        {
            return orderDAO.GetAllOrders();
        }

        public Order GetClientById(string id)
        {
            return orderDAO.GetOrderByID(id);
        }

        public string AddOrder(Order order)
        {
            return orderDAO.AddOrder(order);
        }


        public string AddOrderItem(OrderItem orderItem)
        {
            return orderDAO.AddOrderItem(orderItem);
        }
        public void UpdateOrder(Order order)
        {
            orderDAO.UpdateOrder(order);
        }

        public void DeleteOrder(string id)
        {
            orderDAO.DeleteOrder(id);
        }

        public List<Order> SearchOrders(string searchString)
        {
            return orderDAO.SearchOrders(searchString);
        }
    }
}
