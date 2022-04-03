using EGS.Application.Common.Interfaces;

namespace EGS.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

    }
}
