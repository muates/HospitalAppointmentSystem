using HospitalAppointmentSystem.ApplicationLayer.Service.Abstract;

namespace HospitalAppointmentSystem.ApplicationLayer.Scheduler;

public class AppointmentScheduler(IAppointmentService appointmentService, Timer timer) : IHostedService, IDisposable
{
    private Timer _timer = timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CleanUpAppointments, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void CleanUpAppointments(object? state)
    {
        try
        {
            await appointmentService.CancelExpiredAppointmentsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error cleaning up appointments: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}