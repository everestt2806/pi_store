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

        public void AddOrder(Order order)
        {
            orderDAO.AddOrder(order);
        }

        public String GenerateNewOrderID() {
            return orderDAO.GenerateNewOrderID();
        }

        public String GenerateNewOrderItemID()
        {
            return orderDAO.GenerateNewOrderItemID();
        }


        public void AddOrderItem(OrderItem orderItem)
        {
            orderDAO.AddOrderItem(orderItem);
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
