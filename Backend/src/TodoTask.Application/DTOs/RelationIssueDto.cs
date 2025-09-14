namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для связанной задачи.
/// </summary>
/// <param name="RelatedIssueId">Идентификатор связанной задачи.</param>
public record RelationIssueDto(Guid RelatedIssueId);