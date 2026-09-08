//import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchTasks, createTask, updateTask, deleteTask } from './tasks.slice';

export default function TasksTest() {
  const dispatch = useDispatch();
  const { tasks, status } = useSelector((state) => state.tasks);

  const handleCreate = () => {
    const newTask = {
      title: 'Test Task ' + Date.now(),
      description: 'Created from TasksTest',
      status: 0,
      priority: 1,
      dueDateUtc: new Date().toISOString(),
      projectId: 1, // TEMP: until UI passes real projectId
    };

    dispatch(createTask(newTask));
  };

  const handleUpdate = () => {
    if (tasks.length === 0) return;

    const first = tasks[0];

    const updated = {
      ...first,
      title: first.title + ' (Updated)',
    };

    dispatch(updateTask({ id: first.id, task: updated }));
  };

  const handleDelete = () => {
    if (tasks.length === 0) return;

    const first = tasks[0];
    dispatch(deleteTask(first.id));
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Tasks Test</h2>

      <button onClick={() => dispatch(fetchTasks())}>FETCH TASKS</button>
      <button onClick={handleCreate}>CREATE TASK</button>
      <button onClick={handleUpdate}>UPDATE FIRST TASK</button>
      <button onClick={handleDelete}>DELETE FIRST TASK</button>

      <p>Status: {status}</p>

      <h3>Tasks State</h3>
      <pre>{JSON.stringify(tasks, null, 2)}</pre>
    </div>
  );
}
