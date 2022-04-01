using EGS.Application.Common.Abstractions;

namespace EGS.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

    }
}
