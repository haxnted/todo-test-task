import type { CreateIssueRequest } from "./requests/createIssueRequest";
import { API_URL } from "../apiUrl";
import type { IssueDto } from "./models/IssueDto";

const URL_TO_CONTROLLER = API_URL + "/issues";

export async function createIssue(data: CreateIssueRequest): Promise<void> {
  const response = await fetch(URL_TO_CONTROLLER, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error(`Ошибка при создании задачи: ${response.status}`);
  }
}

export async function getAllIssues(page: number, pageSize: number): Promise<IssueDto[]> {
  const response = await fetch(
    `${URL_TO_CONTROLLER}/paginate?pageIndex=${page}&pageSize=${pageSize}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  if (!response.ok) {
    throw new Error(`Ошибка при получении задач: ${response.status}`);
  }

  return (await response.json()) as IssueDto[];
}

export async function updateIssue(issueId: string, data: Partial<IssueDto>): Promise<void> {
  const response = await fetch(`${URL_TO_CONTROLLER}/${issueId}`, {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  });

  if (!response.ok) {
    throw new Error(`Ошибка при обновлении задачи: ${response.status}`);
  }
}

export async function deleteIssue(issueId: string): Promise<void> {
  const response = await fetch(`${URL_TO_CONTROLLER}/${issueId}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(`Ошибка при удалении задачи: ${response.status}`);
  }
}
