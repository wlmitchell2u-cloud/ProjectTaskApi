import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import {
  Box,
  Button,
  Stack,
  TextField,
  Typography,
  MenuItem,
} from '@mui/material';

import {
  createTask,
  updateTask,
  setTaskFormField,
  fetchTaskById,
} from '../tasks/tasks.slice';

const priorityOptions = [
  { value: 1, label: 'Low' },
  { value: 2, label: 'Medium' },
  { value: 3, label: 'High' },
];

const statusOptions = [
  { value: 1, label: 'New' },
  { value: 2, label: 'Active' },
  { value: 3, label: 'Completed' },
];

export default function TaskForm() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditMode = Boolean(id);

  const taskForm = useSelector((state) => state.tasks.taskForm);
  const projects = useSelector((state) => state.projects.projects);

  useEffect(() => {
    if (isEditMode && id) dispatch(fetchTaskById(id));
  }, [dispatch, id, isEditMode]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    dispatch(setTaskFormField({ field: name, value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (isEditMode) dispatch(updateTask({ id, task: taskForm }));
    else dispatch(createTask(taskForm));
    navigate('/tasks');
  };

  return (
    <Box sx={{ p: 2 }}>
      <Typography variant="h5" mb={2}>
        {isEditMode ? 'Edit Task' : 'Create Task'}
      </Typography>

      <form onSubmit={handleSubmit}>
        <Stack spacing={2}>
          {/* Row 1: Title + Task Number */}
          <Stack direction="row" spacing={2}>
            <TextField
              label="Title"
              name="title"
              value={taskForm.title || ''}
              onChange={handleChange}
              fullWidth
              size="small"
              sx={{ flex: 2 }}
              //required
            />

            <TextField
              label="Task Number"
              name="taskNumber"
              value={taskForm.taskNumber || ''}
              onChange={handleChange}
              size="small"
              sx={{ flex: 1 }}
            />

            <TextField
              label="Project Number"
              name="projectId"
              select
              value={taskForm.projectId ?? ''}
              onChange={handleChange}
              size="small"
              sx={{ minWidth: 200 }}
            >
              {projects && projects.length > 0 ? (
                projects.map((p) => (
                  <MenuItem key={p.id} value={p.id}>
                    {p.projectNumber ? `${p.projectNumber}` : p.name}
                  </MenuItem>
                ))
              ) : (
                <MenuItem value="">No projects</MenuItem>
              )}
            </TextField>
          </Stack>

          {/* Row 2: status, priority, due date, est hours, completed date */}
          <Stack direction="row" spacing={2} alignItems="center">
            <TextField
              label="Status"
              name="status"
              select
              value={taskForm.status ?? 1}
              onChange={handleChange}
              size="small"
              sx={{ minWidth: 120 }}
            >
              {statusOptions.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              label="Priority"
              name="priority"
              select
              value={taskForm.priority ?? 2}
              onChange={handleChange}
              size="small"
              sx={{ minWidth: 120 }}
            >
              {priorityOptions.map((option) => (
                <MenuItem key={option.value} value={option.value}>
                  {option.label}
                </MenuItem>
              ))}
            </TextField>

            <TextField
              label="Due Date"
              name="dueDateUtc"
              type="date"
              value={taskForm.dueDateUtc || ''}
              onChange={handleChange}
              size="small"
              InputLabelProps={{ shrink: true }}
              sx={{ minWidth: 170 }}
            />

            <TextField
              label="Est. Hours"
              name="estimatedHours"
              type="number"
              value={taskForm.estimatedHours ?? ''}
              onChange={handleChange}
              size="small"
              inputProps={{ step: 0.25, min: 0 }}
              sx={{ minWidth: 120 }}
            />

            <TextField
              label="Completed"
              name="completedDateUtc"
              type="date"
              value={taskForm.completedDateUtc || ''}
              onChange={handleChange}
              size="small"
              InputLabelProps={{ shrink: true }}
              sx={{ minWidth: 170 }}
            />
          </Stack>

          {/* Row 3: Description full width */}
          <TextField
            label="Description"
            name="description"
            value={taskForm.description || ''}
            onChange={handleChange}
            fullWidth
            multiline
            minRows={3}
            size="small"
          />

          <Stack direction="row" spacing={2}>
            <Button variant="contained" type="submit" size="small">
              Save
            </Button>
            <Button
              variant="outlined"
              onClick={() => navigate('/tasks')}
              size="small"
            >
              Cancel
            </Button>
          </Stack>
        </Stack>
      </form>
    </Box>
  );
}
