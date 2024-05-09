using ChartsServer.Hubs;
using ChartsServer.model;

using Microsoft.AspNetCore.SignalR;
using System.Data.SqlClient;
using TableDependency.SqlClient;

namespace ChartsServer.Subscription
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);

    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        IConfiguration _configuration;
        IHubContext<SalesHub> _hubContext;
        

        public DatabaseSubscription(IConfiguration configuration,IHubContext<SalesHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        SqlTableDependency<T> _tableDependency;
        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("DefaultConnection"), tableName);
            _tableDependency.OnChanged += async (o, e) =>
            {
                SalesDbContext context = new();
                var data = (from Employee in context.Employees
                            join Sale in context.Sales
                            on Employee.Id equals Sale.EmployeeId
                            select new{
                                Employee,
                                Sale
                            }).ToList();
             
                List<object> datas = new List<object>();
                var employeeNames = data.Select(d => d.Employee.Name).Distinct().ToList();
                employeeNames.ForEach(e =>
                {
                    datas.Add(new
                    {
                        name = e,
                        data = data.Where(s => s.Employee.Name == e).Select(s => s.Sale.Price).ToList()
                    });
            });
                await _hubContext.Clients.All.SendAsync("receiveMessage", datas);
            };
            _tableDependency.OnError += (o, e) => { };
            _tableDependency.Start();
        }
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }

    }
}

