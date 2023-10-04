using Microsoft.AspNetCore.SignalR;
using SignalR_SQLTableDependency.Repositories;

namespace SignalR_SQLTableDependency.Hubs
{

    public class DashboardHub : Hub
    {
        private readonly ProductRepository _productRepository;
        private readonly SaleRepository _saleRepository;
        private readonly CustomerRepository _customerRepository;

        public DashboardHub(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SignalR_SQLTableDenpendencyDb");
            _productRepository = new ProductRepository(connectionString);
            _saleRepository = new SaleRepository(connectionString);
            _customerRepository = new CustomerRepository(connectionString);
        }
        public async Task SendProducts()
        {
            var products = _productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);

            var productsForGraph = _productRepository.GetProductsForGraph();
            await Clients.All.SendAsync("ReceivedProductsForGraph", productsForGraph);
        }


        public async Task SendSales()
        {
            var sales = _saleRepository.GetSales();
            await Clients.All.SendAsync("ReceivedSales", sales);

            var salesForGraph = _saleRepository.GetSalesForGraph();
            await Clients.All.SendAsync("ReceivedSalesForGraph", salesForGraph);
        }

        public async Task SendCustomers()
        {
            var customers = _customerRepository.GetCustomers();
            await Clients.All.SendAsync("ReceivedCustomers", customers);

            var customersForGraph = _customerRepository.GetCustomersForGraph();
            await Clients.All.SendAsync("ReceivedCustomersForGraph", customersForGraph);
        }
    }
}
