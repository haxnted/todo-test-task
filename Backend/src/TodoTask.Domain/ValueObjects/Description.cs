using TodoTask.Domain.Exceptions;

namespace TodoTask.Domain.ValueObjects;

/// <summary>
/// Объект-значение, который представляет собой описание задачи.
/// </summary>
public sealed class Description : IEquatable<Description>, IComparable<Description>
{
    /// <summary>
    /// Минимальная длина описания задачи.
    /// </summary>
    private static readonly int MIN_DESCRIPTION_LENGTH = 5;

    /// <summary>
    /// Максимальная длина описания задачи.
    /// </summary>
    public static readonly int MAX_DESCRIPTION_LENGTH = 700;

    /// <summary>
    /// Значение описания задачи.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private Description(string value) => Value = value;


    /// <summary>
    /// Фабричный метод для создания описания задачи.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="Description"/></returns>
    /// <exception cref="IssueException">
    /// Если описание задачи пустое, 
    /// </exception>
    public static Description Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new IssueException("Описание задачи не должно быть пустым");
        }

        if (value.Length < MIN_DESCRIPTION_LENGTH || value.Length > MAX_DESCRIPTION_LENGTH)
        {
            throw new IssueException($"Длина описания задачи должна быть между {MIN_DESCRIPTION_LENGTH} и {MAX_DESCRIPTION_LENGTH} символами.");
        }
        
        return new(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Description);

    /// <inheritdoc/>
    public bool Equals(Description? other) =>
        other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(Description? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}