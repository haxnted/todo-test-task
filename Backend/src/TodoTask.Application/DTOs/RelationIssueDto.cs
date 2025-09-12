namespace TodoTask.Application.DTOs;

/// <summary>
/// DTO для связанной задачи.
/// </summary>
public record RelationIssueDto(Guid RelatedIssueId);