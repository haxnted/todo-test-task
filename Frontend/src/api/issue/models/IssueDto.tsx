import type { SubIssueDto } from "./SubIssueDto";
import type { RelationIssueDto } from "./RelationIssueDto";
import type { IssueStatus } from "./IssueStatus";
import type { IssuePriority } from "./IssuePriority";

export interface IssueDto {
    id: string;
    title: string;
    description?: string;
    status: IssueStatus;
    priority: IssuePriority;
    executorId?: string;
    subIssues: SubIssueDto[];
    relatedIssues: RelationIssueDto[];
}


