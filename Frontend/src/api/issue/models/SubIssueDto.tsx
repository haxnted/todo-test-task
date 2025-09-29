import type { IssuePriority } from "./IssuePriority";
import type { IssueStatus } from "./IssueStatus";

export interface SubIssueDto {
  id: string;
  title: string;
  description?: string;
  status: IssueStatus;
  priority: IssuePriority;
  executorId?: string;
}


