import connection from '../../signalr/projectHubConnection';
import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import {
  fetchProjects,
  deleteProject,
  projectAdded,
  projectUpdated,
  projectDeleted,
} from '../projects/projects.slice';
import { Box, Button, Stack } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';

export default function ProjectsTable() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { projects, status, error } = useSelector((state) => state.projects);

  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  // Listen for SignalR events
  useEffect(() => {
    if (!connection) return;

    connection.on('ProjectCreated', (project) => {
      dispatch(projectAdded(project));
    });

    connection.on('ProjectUpdated', (project) => {
      dispatch(projectUpdated(project));
    });

    connection.on('ProjectDeleted', (id) => {
      dispatch(projectDeleted(id));
    });

    return () => {
      connection.off('ProjectCreated');
      connection.off('ProjectUpdated');
      connection.off('ProjectDeleted');
    };
  }, [dispatch]);

  const columns = [
    //{ field: 'id', headerName: 'ID', width: 50 },
    { field: 'name', headerName: 'Name', width: 200 },
    { field: 'projectNumber', headerName: 'Project Number', width: 150 },
    { field: 'description', headerName: 'Description', flex: 2 },
    {
      field: 'actions',
      headerName: '',
      width: 95,
      renderCell: (params) => (
        <>
          <IconButton
            color="primary"
            size="small"
            sx={{ p: 0.4 }}
            onClick={() => navigate(`/projects/${params.row.id}/edit`)}
          >
            <EditIcon fontSize="small" />
          </IconButton>

          <IconButton
            color="error"
            size="small"
            sx={{ p: 0.4 }}
            onClick={() => dispatch(deleteProject(params.row.id))}
          >
            <DeleteIcon fontSize="small" />
          </IconButton>
        </>
      ),
    },
  ];

  if (status === 'loading') return <div>Loading projects...</div>;
  if (status === 'failed') return <div>Error: {error}</div>;

  return (
    <Box
      sx={{
        width: '100%',
        p: 0,
        m: 0,
        backgroundColor: '#f2e2cb',
        borderRadius: 0,
      }}
    >
      <Stack
        direction="row"
        alignitems="center"
        justifycontent="flex-start"
        sx={{ mb: 1, gap: 1 }}
      >
        <Button
          variant="contained"
          color="primary"
          size="small"
          onClick={() => navigate('/projects/create')}
          sx={{ textTransform: 'none', px: 1.5, py: 0.65, fontSize: '0.82rem' }}
        >
          Create Project
        </Button>
      </Stack>
      <div style={{ width: '100%', margin: 0, padding: 0 }}>
        <DataGrid
          autoHeight
          rows={projects}
          columns={columns}
          pageSize={10}
          density="compact"
          rowsPerPageOptions={[10, 20, 50]}
          rowHeight={40}
          headerHeight={40}
          components={{ Toolbar: GridToolbar }}
          componentsProps={{
            toolbar: {
              csvOptions: {
                fileName: 'projects_export',
                utf8WithBom: true,
                allColumns: true,
              },
            },
          }}
          sx={{
            backgroundColor: '#f2e2cb',
            border: 'none',
            borderRadius: 0,
            color: '#333',
            '& .MuiDataGrid-root': {
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-main': {
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-virtualScroller': {
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-window': {
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-cell': {
              borderBottom: 'none',
              py: 0.3,
              fontSize: '0.82rem',
              lineHeight: 1.25,
              backgroundColor: 'transparent',
              display: 'flex',
              alignItems: 'center',
            },
            '& .MuiDataGrid-columnHeaders': {
              borderBottom: '1px solid rgba(0, 0, 0, 0.08)',
              backgroundColor: '#f2e2cb',
              fontWeight: 600,
              color: '#1f2937',
              fontSize: '0.82rem',
              borderTopLeftRadius: 0,
              borderTopRightRadius: 0,
            },
            '& .MuiDataGrid-row': {
              borderBottom: '1px solid rgba(0, 0, 0, 0.06)',
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-row:hover': {
              backgroundColor: 'rgba(25, 118, 210, 0.08)',
            },
            '& .MuiDataGrid-columnSeparator': {
              display: 'none',
            },
            '& .MuiDataGrid-footerContainer': {
              borderTop: 'none',
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-row:nth-of-type(odd)': {
              backgroundColor: '#f2e2cb',
            },
            '& .MuiDataGrid-row:nth-of-type(even)': {
              backgroundColor: '#f2e2cb',
            },
          }}
        />
      </div>
    </Box>
  );
}
