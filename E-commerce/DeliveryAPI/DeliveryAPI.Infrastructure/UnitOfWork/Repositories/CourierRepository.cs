using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAPI.Infrastructure.UnitOfWork.Repositories
{
    public class CourierRepository : GenericRepository<Courier>, ICourierRepository
    {
        public CourierRepository(Context context) : base(context) { }
    }
}
