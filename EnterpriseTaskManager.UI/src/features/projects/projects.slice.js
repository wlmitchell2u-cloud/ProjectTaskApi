import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import ProjectsApi from '../projects/projects.api';

// -----------------------------
// Async Thunks
// -----------------------------

export const fetchProjects = createAsyncThunk(
  'projects/fetchProjects',
  async () => {
    const response = await ProjectsApi.getAll();
    return response;
  }
);

export const fetchProjectById = createAsyncThunk(
  'projects/fetchProjectById',
  async (id) => {
    const response = await ProjectsApi.getById(id);
    return response;
  }
);

export const createProject = createAsyncThunk(
  'projects/createProject',
  async (project) => {
    const response = await ProjectsApi.create(project);
    return response;
  }
);

export const updateProject = createAsyncThunk(
  'projects/updateProject',
  async ({ id, project }) => {
    const response = await ProjectsApi.update(id, project);
    return response;
  }
);

export const deleteProject = createAsyncThunk(
  'projects/deleteProject',
  async (id) => {
    await ProjectsApi.delete(id);
    return id;
  }
);

// -----------------------------
// Initial State
// -----------------------------

const initialState = {
  projects: [],
  projectForm: {
    name: '',
    projectNumber: '',
    description: '',
    ownerId: 1,
  },
  status: 'idle',
  error: null,
};

// -----------------------------
// Slice
// -----------------------------

const projectsSlice = createSlice({
  name: 'projects',
  initialState,
  reducers: {
    setFormField(state, action) {
      const { field, value } = action.payload;
      return {
        ...state,
        projectForm: {
          ...state.projectForm,
          [field]: value,
        },
      };
    },

    resetForm(state) {
      return {
        ...state,
        projectForm: initialState.projectForm,
      };
    },

    projectAdded(state, action) {
      return {
        ...state,
        projects: [...state.projects, action.payload],
      };
    },

    projectUpdated(state, action) {
      return {
        ...state,
        projects: state.projects.map((p) =>
          p.id === action.payload.id ? action.payload : p
        ),
      };
    },

    projectDeleted(state, action) {
      return {
        ...state,
        projects: state.projects.filter((p) => p.id !== action.payload),
      };
    },
  },

  // -----------------------------
  // Extra Reducers
  // -----------------------------
  extraReducers: (builder) => {
    builder
      // fetchProjects
      .addCase(fetchProjects.pending, (state) => ({
        ...state,
        status: 'loading',
      }))
      .addCase(fetchProjects.fulfilled, (state, action) => ({
        ...state,
        status: 'succeeded',
        projects: action.payload,
      }))
      .addCase(fetchProjects.rejected, (state, action) => ({
        ...state,
        status: 'failed',
        error: action.error.message,
      }))

      // fetchProjectById → populate form for edit mode
      .addCase(fetchProjectById.fulfilled, (state, action) => ({
        ...state,
        projectForm: {
          name: action.payload.name,
          projectNumber: action.payload.projectNumber,
          description: action.payload.description,
          ownerId: action.payload.ownerId,
        },
      }))

      // createProject
      .addCase(createProject.fulfilled, (state, action) => ({
        ...state,
        projects: [...state.projects, action.payload],
      }))

      // updateProject
      .addCase(updateProject.fulfilled, (state, action) => ({
        ...state,
        projects: state.projects.map((p) =>
          p.id === action.payload.id ? action.payload : p
        ),
      }))

      // deleteProject
      .addCase(deleteProject.fulfilled, (state, action) => ({
        ...state,
        projects: state.projects.filter((p) => p.id !== action.payload),
      }));
  },
});

export const {
  projectAdded,
  projectUpdated,
  projectDeleted,
  resetForm,
  setFormField,
} = projectsSlice.actions;

export default projectsSlice.reducer;
