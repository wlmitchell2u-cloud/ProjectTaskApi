import { DataGrid } from '@mui/x-data-grid';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchTasks, deleteTask } from '../tasks/tasks.slice';
import { Button, Box, Stack } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';

export default function TasksTable() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { tasks, status, error } = useSelector((state) => state.tasks);

  useEffect(() => {
    dispatch(fetchTasks());
  }, [dispatch]);

  const handleEdit = (id) => {
    navigate(`/tasks/${id}/edit`);
  };

  const columns = [
    { field: 'title', headerName: 'Title', flex: 1, minWidth: 150 },
    { field: 'taskNumber', headerName: 'Task Number', flex: 1, minWidth: 130 },
    { field: 'description', headerName: 'Description', flex: 1, minWidth: 200 },
    {
      field: 'dueDateUtc',
      headerName: 'Due Date',
      width: 140,
      valueGetter: (params) =>
        params.row && params.row.dueDateUtc
          ? new Date(params.row.dueDateUtc).toLocaleDateString()
          : '',
    },
    { field: 'estimatedHours', headerName: 'Est. Hours', width: 120 },
    { field: 'status', headerName: 'Status', width: 100 },
    { field: 'priority', headerName: 'Priority', width: 100 },
    {
      //field: 'actions',
      //headerName: '',
      width: 100,
      renderCell: (params) => (
        <>
          <IconButton
            color="primary"
            size="small"
            sx={{ p: 0.4 }}
            onClick={() => handleEdit(params.row.id)}
          >
            <EditIcon fontSize="small" />
          </IconButton>

          <IconButton
            color="error"
            size="small"
            sx={{ p: 0.4 }}
            onClick={() => dispatch(deleteTask(params.row.id))}
          >
            <DeleteIcon fontSize="small" />
          </IconButton>
        </>
      ),
    },
  ];

  if (status === 'loading') return <div>Loading tasks...</div>;
  if (status === 'failed') return <div>Error: {error}</div>;

  return (
    <Box
      sx={{
        width: '100%',
        pl: 0,
        pr: 0,
        pt: 0,
        pb: 0,
        backgroundColor: '#f2e2cb',
        borderRadius: 0,
        p: 0,
      }}
    >
      <Stack
        direction="row"
        alignitems="center"
        justifycontent="flex-start"
        sx={{ mb: 1 }}
      >
        <Button
          variant="contained"
          color="primary"
          size="small"
          onClick={() => navigate('/tasks/create')}
          sx={{ textTransform: 'none', px: 1.5, py: 0.65, fontSize: '0.82rem' }}
        >
          Create Task
        </Button>
      </Stack>

      <div style={{ width: '100%', margin: 0, padding: 0 }}>
        <DataGrid
          rows={tasks}
          columns={columns}
          autoHeight
          density="compact"
          pageSize={10}
          rowsPerPageOptions={[10, 20, 50]}
          rowHeight={40}
          headerHeight={40}
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
