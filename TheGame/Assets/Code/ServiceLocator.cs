using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        Type type = typeof(T);

        if (!services.ContainsKey(type))
        {
            services.Add(type, service);
        }
    }

    public static void Set<T>(T newService)  
    {
        Type type = typeof(T);

        if (services.ContainsKey(type))
        {
            services[type] = newService;
        }
    }

    public static T Get<T>()
    {
        Type type = typeof(T);

        if (services.TryGetValue(type, out object service))
        {
            return (T)service;
        }

        throw new Exception($"El servicio de tipo {type} no está registrado.");
    }

    public static bool IsRegistered<T>() 
    {
        Type type = typeof(T);

        return services.TryGetValue(type, out object service);
    }

    public static void Clear()
    {
        services.Clear();
    }

    public static void Unregister<T>()
    {
        services.Remove(typeof(T));
    }
}

