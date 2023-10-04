using SignalR_SQLTableDependency.Hubs;
using SignalR_SQLTableDependency.Models;
using TableDependency.SqlClient;

namespace SignalR_SQLTableDependency.SubscribeTableDependencies
{
    public class SubscribeSaleTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Sale> _tableDependency;
        DashboardHub dashboardHub;

        public SubscribeSaleTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Sale>(connectionString);
            _tableDependency.OnChanged += _tableDependency_OnChanged;
            _tableDependency.OnError += _tableDependency_OnError;
            _tableDependency.Start();
        }

        private void _tableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Sale)} SqlTableDependency error: {e.Error.Message}");
        }

        private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Sale> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendSales();
            }
        }
    }
}
