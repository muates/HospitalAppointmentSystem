namespace HospitalAppointmentSystem.EntityLayer.Abstract;

public abstract class EntityBase<TId>
{
    public TId Id { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    protected EntityBase()
    {
        if (typeof(TId) == typeof(string))
        {
            Id = (TId)(object)Guid.NewGuid().ToString();
        }
    }
    
    public void Update() => UpdatedAt = DateTime.UtcNow;
}
