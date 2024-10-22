using System.Reflection;

namespace HospitalAppointmentSystem.Core.Service.Concrete;

public class AutoMapperService
{
    public static void Map<TTarget, TSource>(TTarget target, TSource source)
    {
        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        foreach (var sourceProp in sourceProperties)
        {
            var targetProp = typeof(TTarget).GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);
            
            if (targetProp != null && targetProp.CanWrite)
            {
                var sourceValue = sourceProp.GetValue(source);
                
                if (sourceValue != null && !IsDefaultValue(sourceValue))
                {
                    targetProp.SetValue(target, sourceValue);
                }
            }
        }
    }
    
    private static bool IsDefaultValue(object? value)
    {
        if (value == null)
        {
            return true;
        }

        var type = value.GetType();
        
        if (type == typeof(string))
        {
            return string.IsNullOrEmpty((string)value);
        }

        var defaultValue = Activator.CreateInstance(type);
        return type.IsValueType && defaultValue != null && defaultValue.Equals(value);
    }
}