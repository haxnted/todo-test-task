import { Button, Form, Input, Select } from "antd";
import { createIssue } from "../../api/issue/issueApi";

const { TextArea } = Input;

export function CreateIssueForm() {
  const [form] = Form.useForm();

  const onFinish = async (values: any) => {
    try {
      await createIssue({
        userId: values.userId,
        status: values.status,
        priority: values.priority,
        executorId: values.executorId || undefined,
        title: values.title,
        description: values.description,
      });

      form.resetFields();
      console.log("Задача создана");
    } catch (error) {
      console.error("Ошибка:", error);
    }
  };

  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={onFinish}
      style={{ maxWidth: 500, margin: "0 auto" }}
    >
      <Form.Item
        name="userId"
        label="ID пользователя"
        rules={[{ required: true, message: "Введите ID пользователя" }]}
      >
        <Input />
      </Form.Item>

      <Form.Item
        name="executorId"
        label="ID исполнителя"
      >
        <Input />
      </Form.Item>

      <Form.Item
        name="status"
        label="Статус"
        rules={[{ required: true, message: "Выберите статус" }]}
      >
        <Select
          options={[
            { value: 0, label: "Открыта" },
            { value: 1, label: "В работе" },
            { value: 2, label: "Закрыта" },
          ]}
        />
      </Form.Item>

      <Form.Item
        name="priority"
        label="Приоритет"
        rules={[{ required: true, message: "Выберите приоритет" }]}
      >
        <Select
          options={[
            { value: 0, label: "Низкий" },
            { value: 1, label: "Средний" },
            { value: 2, label: "Высокий" },
          ]}
        />
      </Form.Item>

      <Form.Item
        name="title"
        label="Название"
        rules={[{ required: true, message: "Введите название задачи" }]}
      >
        <Input />
      </Form.Item>

      <Form.Item
        name="description"
        label="Описание"
      >
        <TextArea rows={4} />
      </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit">
          Создать задачу
        </Button>
      </Form.Item>
    </Form>
  );
}
