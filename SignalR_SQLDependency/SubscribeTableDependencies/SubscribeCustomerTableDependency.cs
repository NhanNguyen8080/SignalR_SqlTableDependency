using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies
{
    public class SubscribeCustomerTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Customer> _tableDependency;
        DashboardHub dashboardHub;

        public SubscribeCustomerTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Customer>(connectionString);
            _tableDependency.OnChanged += _tableDependency_OnChanged;
            _tableDependency.OnError += _tableDependency_OnError;
            _tableDependency.Start();
        }

        private void _tableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Customer)} SqlTableDependency error: {e.Error.Message}");
        }

        private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Customer> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendCustomers();
            }
        }
    }
}
