import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {
  fetchProjects,
  createProject,
  updateProject,
  deleteProject,
} from './projects.slice';

import { Button, Stack, Typography, Paper, Divider } from '@mui/material';

export default function ProjectsTest() {
  const dispatch = useDispatch();
  const { projects, status, error } = useSelector((state) => state.projects);

  //load projects on mount
  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  const handleCreate = () => {
    dispatch(
      createProject({
        name: 'Test Project ' + Date.now(),
        description: 'Created from ProjectsTest',
        status: 0,
        ownerId: 1,
      })
    );
  };

  const handleUpdate = () => {
    if (projects.length === 0) return;

    const first = projects[0];

    dispatch(
      updateProject({
        id: first.id,
        project: { ...first, name: first.name + ' (Updated)' },
      })
    );
  };

  const handleDelete = () => {
    if (projects.length === 0) return;

    dispatch(deleteProject(projects[0].id));
  };

  return (
    <Paper elevation={4} sx={{ padding: 3, margin: 3 }}>
      <Typography variant="h5" gutterBottom>
        Projects Slice Test
      </Typography>

      <Typography variant="body1" sx={{ mb: 2 }}>
        Status: {status}
      </Typography>

      {error && (
        <Typography color="error" sx={{ mb: 2 }}>
          Error: {error}
        </Typography>
      )}
      <Stack direction="row" spacing={2} sx={{ mb: 2 }}>
        <Button variant="contained" color="primary" onClick={handleCreate}>
          Create Project
        </Button>

        <Button variant="contained" color="warning" onClick={handleUpdate}>
          Update First Project
        </Button>

        <Button variant="contained" color="error" onClick={handleDelete}>
          Delete First Project
        </Button>
      </Stack>

      <Divider sx={{ my: 2 }} />

      <Typography variant="h6">Projects State</Typography>
      <pre>{JSON.stringify(projects, null, 2)}</pre>
    </Paper>
  );
}
