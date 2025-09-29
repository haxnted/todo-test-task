import { Row, Col } from "antd"
import { Header } from "./components/general/Header"
import { CreateIssueForm } from "./components/issue/CreateIssueForm"
import { IssueGrid } from "./components/issue/IssueGrid"

function App() {
  return <div id="root">
      <Header />
      <Row>
        <Col span={12}>
          <CreateIssueForm />
        </Col>
        <Col span={12}>
          <IssueGrid />
        </Col>
      </Row>
  </div>
}

export default App
