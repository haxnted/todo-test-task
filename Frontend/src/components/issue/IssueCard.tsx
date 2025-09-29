// src/components/issue/IssueCard.tsx
import { Card, Tag, Typography, Collapse, Button, Modal, Form, Input, Select, Space } from "antd";
import { useState } from "react";
import type { IssueDto } from "../../api/issue/models/IssueDto";

const { Title, Paragraph } = Typography;
const { Panel } = Collapse;
const { Option } = Select;

interface IssueCardProps {
  issue: IssueDto;
  onDelete: () => void;
  onUpdate: (data: Partial<IssueDto>) => void;
}

export function IssueCard({ issue, onDelete, onUpdate }: IssueCardProps) {
  const [hovered, setHovered] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();

  const handleEdit = () => {
    form.setFieldsValue({
      title: issue.title,
      description: issue.description,
      status: issue.status,
      priority: issue.priority,
    });
    setIsModalOpen(true);
  };

  const handleOk = async () => {
    const values = await form.validateFields();
    onUpdate(values);
    setIsModalOpen(false);
  };

  return (
    <>
      <Card
        hoverable
        style={{ borderRadius: 12, boxShadow: "0 4px 12px rgba(0,0,0,0.1)" }}
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}
        extra={
          <Space>
            <Button size="small" onClick={handleEdit}>Редактировать</Button>
            <Button size="small" danger onClick={onDelete}>Удалить</Button>
          </Space>
        }
      >
        <Title level={5}>{issue.title}</Title>
        <Paragraph ellipsis={{ rows: 3 }}>{issue.description || "Нет описания"}</Paragraph>

        <div style={{ display: "flex", justifyContent: "space-between", marginTop: 8 }}>
          <Tag color={getStatusColor(issue.status)}>{getStatusLabel(issue.status)}</Tag>
          <Tag color={getPriorityColor(issue.priority)}>{getPriorityLabel(issue.priority)}</Tag>
        </div>

        {hovered && issue.subIssues.length > 0 && (
          <div style={{ marginTop: 12, paddingLeft: 12, borderLeft: "2px solid #eee" }}>
            <Collapse ghost>
              {issue.subIssues.map((sub) => (
                <Panel header={sub.title} key={sub.id}>
                  <Paragraph>{sub.description || "Нет описания"}</Paragraph>
                  <div style={{ display: "flex", gap: 8 }}>
                    <Tag color={getStatusColor(sub.status)}>{getStatusLabel(sub.status)}</Tag>
                    <Tag color={getPriorityColor(sub.priority)}>{getPriorityLabel(sub.priority)}</Tag>
                  </div>
                </Panel>
              ))}
            </Collapse>
          </div>
        )}
      </Card>

      <Modal
        title="Редактировать задачу"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={() => setIsModalOpen(false)}
        okText="Сохранить"
        cancelText="Отмена"
      >
        <Form form={form} layout="vertical">
          <Form.Item name="title" label="Название" rules={[{ required: true, message: "Введите название" }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Описание">
            <Input.TextArea rows={3} />
          </Form.Item>
          <Form.Item name="status" label="Статус" rules={[{ required: true }]}>
            <Select>
              <Option value={0}>Open</Option>
              <Option value={1}>InProgress</Option>
              <Option value={2}>Closed</Option>
            </Select>
          </Form.Item>
          <Form.Item name="priority" label="Приоритет" rules={[{ required: true }]}>
            <Select>
              <Option value={0}>Low</Option>
              <Option value={1}>Medium</Option>
              <Option value={2}>High</Option>
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}

// Статус
function getStatusColor(status: number) {
  switch (status) {
    case 0: return "green";
    case 1: return "blue";
    case 2: return "gray";
    default: return "default";
  }
}
function getStatusLabel(status: number) {
  switch (status) {
    case 0: return "Open";
    case 1: return "InProgress";
    case 2: return "Closed";
    default: return "Unknown";
  }
}

// Приоритет
function getPriorityColor(priority: number) {
  switch (priority) {
    case 0: return "cyan";
    case 1: return "orange";
    case 2: return "red";
    default: return "default";
  }
}
function getPriorityLabel(priority: number) {
  switch (priority) {
    case 0: return "Low";
    case 1: return "Medium";
    case 2: return "High";
    default: return "Unknown";
  }
}
