import { configureStore } from '@reduxjs/toolkit';
import tasksReducer from '../features/tasks/tasks.slice';
import projectsReducer from '../features/projects/projects.slice';

export const store = configureStore({
  reducer: {
    projects: projectsReducer,
    tasks: tasksReducer,
  },
});
