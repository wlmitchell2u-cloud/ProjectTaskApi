import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import {
  Box,
  Paper,
  Stack,
  TextField,
  Typography,
  SpeedDial,
  SpeedDialAction,
} from '@mui/material';

import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Cancel';

import {
  fetchProjectById,
  updateProject,
  createProject,
  resetForm,
  setFormField,
} from '../projects/projects.slice';

const ProjectForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();

  const isEditMode = Boolean(id);
  const projectForm = useSelector((state) => state.projects.projectForm);

  // Track touched fields for Save-click validation
  //const [touched, setTouched] = useState({});

  useEffect(() => {
    if (isEditMode) {
      dispatch(fetchProjectById(id));
    } else {
      dispatch(resetForm());
    }
  }, [id, isEditMode, dispatch]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    dispatch(setFormField({ field: name, value }));
  };

  const handleSubmit = () => {
    // Mark all fields as touched on Save click
    /* const newTouched = {
      name: true,
      description: true,
    };
    setTouched(newTouched); */

    // Trimmed validation
    //const nameIsValid = projectForm.name?.trim().length > 0;
    //const descIsValid = projectForm.description?.trim().length > 0;

    // Block submit if invalid
    //if (!nameIsValid || !descIsValid) return;

    // Submit
    if (isEditMode) {
      dispatch(updateProject({ id, project: projectForm }));
    } else {
      dispatch(createProject(projectForm));
    }

    navigate('/');
  };

  return (
    <Box
      sx={{
        width: '100%',
        p: 1,
        backgroundColor: '#f2e2cb',
        position: 'relative',
      }}
    >
      <Typography
        variant="h5"
        mb={2}
        sx={{ fontSize: '1.4rem', fontWeight: 600 }}
      >
        {isEditMode ? 'Edit Project' : 'Create Project'}
      </Typography>

      <Paper sx={{ p: 1.5, backgroundColor: '#f2e2cb', border: 'none' }}>
        <Stack spacing={1}>
          <Stack direction="row" spacing={1}>
            <TextField
              label="Project Name"
              name="name"
              value={projectForm?.name || ''}
              onChange={handleChange}
              fullWidth
              size="small"
              sx={{ '& .MuiInputBase-input': { fontSize: '0.82rem' } }}
            />

            <TextField
              label="Project Number"
              name="projectNumber"
              value={projectForm?.projectNumber || ''}
              onChange={handleChange}
              fullWidth
              size="small"
              sx={{ '& .MuiInputBase-input': { fontSize: '0.82rem' } }}
            />
          </Stack>

          <TextField
            label="Description"
            name="description"
            value={projectForm?.description || ''}
            onChange={handleChange}
            fullWidth
            multiline
            minRows={2}
            size="small"
            sx={{ '& .MuiInputBase-input': { fontSize: '0.82rem' } }}
          />
        </Stack>
      </Paper>

      <SpeedDial
        ariaLabel="Project Actions"
        sx={{
          position: 'absolute',
          bottom: 0,
          right: 16,
          '& .MuiFab-primary': {
            width: 40,
            height: 40,
          },
        }}
        icon={<SaveIcon fontSize="small" />}
      >
        <SpeedDialAction
          icon={<SaveIcon fontSize="small" />}
          onClick={handleSubmit}
          sx={{ width: 40, height: 40 }}
          slotProps={{
            tooltip: {
              open: true,
              title: 'Save',
            },
          }}
        />

        <SpeedDialAction
          icon={<CancelIcon fontSize="small" />}
          onClick={() => navigate('/')}
          sx={{ width: 40, height: 40 }}
          slotProps={{
            tooltip: {
              open: true,
              title: 'Cancel',
            },
          }}
        />
      </SpeedDial>
    </Box>
  );
};

export default ProjectForm;
