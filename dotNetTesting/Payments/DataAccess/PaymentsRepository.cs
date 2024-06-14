using dotNetTesting.Payments.Model;
using Microsoft.EntityFrameworkCore;

namespace dotNetTesting.Payments.DataAccess;

public class PaymentsRepository : IPaymentsRepository
{
    private PaymentsDbContext _context;

    public PaymentsRepository(PaymentsDbContext context)
    {
        _context = context;
    }

    public async Task Add(Payment payment) 
    {
        // await _context.Payments.AddAsync(payment);
        // await _context.SaveChangesAsync();
    }
}