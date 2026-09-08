import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { taskApi } from './tasks.api';

// Async Thunks
export const fetchTasks = createAsyncThunk('tasks/fetchTasks', async () => {
  return await taskApi.getAll();
});

export const fetchTaskById = createAsyncThunk(
  'tasks/fetchTaskById',
  async (id) => {
    return await taskApi.getById(id);
  }
);

export const createTask = createAsyncThunk('tasks/createTask', async (task) => {
  return await taskApi.create(task);
});

export const updateTask = createAsyncThunk(
  'tasks/updateTask',
  async ({ id, task }) => {
    return await taskApi.update(id, task);
  }
);

export const deleteTask = createAsyncThunk('tasks/deleteTask', async (id) => {
  await taskApi.delete(id);
  return id;
});

const initialState = {
  tasks: [],
  taskForm: {
    title: '',
    taskNumber: '',
    description: '',
    priority: 2,
    status: 1,
    dueDateUtc: '',
    estimatedHours: '',
    completedDateUtc: '',
    projectId: '',
    assignedToUserId: null,
  },
  status: 'idle',
  error: null,
};

const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {
    setTaskFormField: (state, action) => {
      const { field, value } = action.payload;
      state.taskForm[field] = value;
    },
    resetTaskForm: (state) => {
      state.taskForm = initialState.taskForm;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchTasks.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchTasks.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.tasks = action.payload;
      })
      .addCase(fetchTasks.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      })
      .addCase(fetchTaskById.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchTaskById.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.taskForm = {
          title: action.payload.title || '',
          taskNumber: action.payload.taskNumber || '',
          description: action.payload.description || '',
          priority: action.payload.priority || 2,
          status: action.payload.status || 1,
          dueDateUtc: action.payload.dueDateUtc?.substring(0, 10) || '',
          estimatedHours:
            action.payload.estimatedHours != null
              ? String(action.payload.estimatedHours)
              : '',
          completedDateUtc:
            action.payload.completedDateUtc?.substring(0, 10) || '',
          projectId: action.payload.projectId ?? '',
          assignedToUserId: action.payload.assignedToUserId ?? null,
        };
      })
      .addCase(fetchTaskById.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      })
      .addCase(createTask.fulfilled, (state, action) => {
        state.tasks.push(action.payload);
      })
      .addCase(updateTask.fulfilled, (state, action) => {
        const index = state.tasks.findIndex((t) => t.id === action.payload.id);
        if (index !== -1) {
          state.tasks[index] = action.payload;
        }
      })
      .addCase(deleteTask.fulfilled, (state, action) => {
        state.tasks = state.tasks.filter((t) => t.id !== action.payload);
      });
  },
});

export const { setTaskFormField, resetTaskForm } = tasksSlice.actions;
export default tasksSlice.reducer;
