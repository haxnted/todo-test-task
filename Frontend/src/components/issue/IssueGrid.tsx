// src/components/issue/IssueGrid.tsx
import { Row, Col, Spin, Alert, Pagination } from "antd";
import { useState } from "react";
import { useIssues } from "../../api/issue/hooks/useIssues";
import { IssueCard } from "./IssueCard";

export function IssueGrid() {
  const [page, setPage] = useState(1);
  const pageSize = 6;

  const { data, loading, error, updateIssue, deleteIssue } = useIssues(page, pageSize);

  if (loading)
    return <Spin tip="Загрузка..." style={{ display: "block", margin: "50px auto" }} />;
  if (error) return <Alert message="Ошибка" description={error} type="error" />;

  return (
    <>
      <Row gutter={[16, 16]}>
        {data.map((issue) => (
          <Col key={issue.id} xs={24} sm={12} md={8} lg={6}>
            <IssueCard
              issue={issue}
              onDelete={() => deleteIssue(issue.id)}
              onUpdate={(updatedData) => updateIssue(issue.id, updatedData)}
            />
          </Col>
        ))}
      </Row>
      <div style={{ marginTop: 24, textAlign: "center" }}>
        <Pagination
          current={page}
          pageSize={pageSize}
          total={50} // TODO: в будущем допилить))
          onChange={(p) => setPage(p)}
        />
      </div>
    </>
  );
}
