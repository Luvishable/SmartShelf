namespace SmartShelf.Domain.Common;

public static class Guard
{

    public static void AgainstNull(object? value, string name)
    {
        if (value is null)
            throw new ArgumentNullException(name, $"{name} cannot be null.");
    }
    public static void AgainstNullOrEmpty(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
    }

    public static void AgainstNonPositive(decimal value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"{parameterName} must be greater than zero.", parameterName);
    }

    public static void AgainstNonPositive(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentException($"{parameterName} must be greater than zero.", parameterName);
    }

    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0)
            throw new ArgumentException($"{parameterName} cannot be negative.", parameterName);
    }

    public static void AgainstFutureDate(DateTime value, string parameterName)
    {
        if (value > DateTime.UtcNow)
            throw new ArgumentException($"{parameterName} cannot be in the future.", parameterName);
    }

    public static void AgainstEarlierThan(DateTime? value, DateTime reference, string parameterName)
    {
        if (value.HasValue && value.Value < reference)
            throw new ArgumentException($"{parameterName} cannot be earlier than {reference}.", parameterName);
    }
}