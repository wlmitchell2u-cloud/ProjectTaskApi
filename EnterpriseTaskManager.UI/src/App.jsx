import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { useEffect } from 'react';
import connection from './signalr/projectHubConnection';
import MainLayout from './layout/MainLayout';
import ProjectsTable from './features/projects/ProjectsTable';
import ProjectForm from './features/projects/ProjectForm';
import TasksTable from './features/tasks/TasksTable';
import TaskForm from './features/tasks/TaskForm';

export default function App() {
  useEffect(() => {
    if (connection.state === 'Disconnected') {
      connection
        .start()
        .then(() => console.log('SignalR Connected'))
        .catch((err) => console.error('SignalR Connection Error: ', err));
    }
  }, []);

  return (
    <BrowserRouter>
      <Routes>
        <Route element={<MainLayout />}>
          {/* PROJECT ROUTES */}
          <Route index element={<ProjectsTable />} />
          <Route path="projects/create" element={<ProjectForm />} />
          <Route path="projects/:id/edit" element={<ProjectForm />} />
          {/* TASK ROUTES */}
          <Route path="tasks" element={<TasksTable />} />
          <Route path="tasks/create" element={<TaskForm />} />
          <Route path="tasks/:id/edit" element={<TaskForm />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
