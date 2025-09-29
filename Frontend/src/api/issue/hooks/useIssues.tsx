// src/api/issue/hooks/useIssues.ts
import { useEffect, useState, useCallback } from "react";
import { getAllIssues, updateIssue as apiUpdateIssue, deleteIssue as apiDeleteIssue } from "../issueApi";
import type { IssueDto } from "../models/IssueDto";

export function useIssues(page: number, pageSize: number) {
  const [data, setData] = useState<IssueDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchIssues = useCallback(() => {
    setLoading(true);
    getAllIssues(page, pageSize)
      .then(setData)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [page, pageSize]);

  useEffect(() => {
    fetchIssues();
  }, [fetchIssues]);

  const updateIssue = async (issueId: string, updateData: Partial<IssueDto>) => {
    try {
      setLoading(true);
      await apiUpdateIssue(issueId, updateData);
      fetchIssues(); // Обновляем список после изменения
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const deleteIssue = async (issueId: string) => {
    try {
      setLoading(true);
      await apiDeleteIssue(issueId);
      fetchIssues(); // Обновляем список после удаления
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { data, loading, error, updateIssue, deleteIssue };
}
