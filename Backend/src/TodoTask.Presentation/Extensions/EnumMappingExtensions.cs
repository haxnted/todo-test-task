namespace TodoTask.Presentation.Extensions;

/// <summary>
/// Универсальные методы расширения для преобразования Request enum в Domain enum.
/// </summary>
public static class EnumMappingExtensions
{
    /// <summary>
    /// Преобразует enum запроса в соответствующий Domain enum по имени.
    /// </summary>
    /// <typeparam name="TDomainEnum">Целевой enum домена.</typeparam>
    /// <param name="requestEnum">Значение enum запроса.</param>
    /// <returns>Соответствующее значение Domain enum.</returns>
    /// <exception cref="ArgumentException">Если не найдено совпадение по имени.</exception>
    public static TDomainEnum ToDomain<TDomainEnum>(this Enum requestEnum)
        where TDomainEnum : struct, Enum
    {
        var name = requestEnum.ToString();

        if (!Enum.TryParse(name, out TDomainEnum domainEnum))
        {
            throw new ArgumentException($"Не найдено соответствующее значение {typeof(TDomainEnum).Name} для '{name}'");
        }

        return domainEnum;
    }
}