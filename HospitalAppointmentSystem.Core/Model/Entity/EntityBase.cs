namespace HospitalAppointmentSystem.Core.Model.Entity;

public abstract class EntityBase<TId>
{
    public TId Id { get; private set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    protected EntityBase()
    {
        if (typeof(TId) == typeof(string))
        {
            Id = (TId)(object)Guid.NewGuid().ToString();
        }
        else if (typeof(TId) == typeof(int))
        {
            Id = (TId)(object)0;
        }
    }
}