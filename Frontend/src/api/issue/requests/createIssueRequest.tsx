// Запрос на создание задачи
export interface CreateIssueRequest {
  userId: string;
  status: number;      
  priority: number;   
  executorId?: string;
  title: string;
  description: string;
}