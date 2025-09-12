using TodoTask.Domain.Exceptions;

namespace TodoTask.Domain.ValueObjects;

/// <summary>
/// Объект-значение, который представляет собой заголовок задачи.
/// </summary>
public sealed class Title : IEquatable<Title>, IComparable<Title>
{
    /// <summary>
    /// Минимальная длина заголовка задачи.
    /// </summary>
    public static readonly int MIN_TITLE_LENGTH = 5;

    /// <summary>
    /// Максимальная длина заголовка задачи.
    /// </summary>
    public static readonly int MAX_TITLE_LENGTH = 200;

    /// <summary>
    /// Значение заголовка встречи.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор, который используется в фабричном методе.
    /// </summary>
    /// <param name="value">Значение.</param>
    private Title(string value) => Value = value;

    /// <summary>
    /// Фабричный метод для создания заголовка задачи.
    /// </summary>
    /// <param name="value">Значение.</param>
    /// <returns>Экземпляр <see cref="Title"/></returns>
    /// <exception cref="IssueException">
    /// Если заголовок пустой,
    /// Если заголовок превышает допустимый диапазон по символам
    /// </exception>
    public static Title Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value.Trim()))
        {
            throw new IssueException("Заголовок не должен быть пустым");
        }

        if (value.Length < MIN_TITLE_LENGTH || value.Length > MAX_TITLE_LENGTH)
        {
            throw new IssueException($"Длина заголовка должна быть между {MIN_TITLE_LENGTH} и {MAX_TITLE_LENGTH} символами.");
        }

        return new(value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Title);

    /// <inheritdoc/>
    public bool Equals(Title? other) => other is not null && Value.Equals(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public int CompareTo(Title? other)
    {
        if (other is null) return 1;

        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }
}