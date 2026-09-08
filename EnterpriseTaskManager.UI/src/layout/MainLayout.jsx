import { Outlet, Link } from 'react-router-dom';
import {
  AppBar,
  Toolbar,
  Typography,
  Drawer,
  List,
  ListItemButton,
  ListItemText,
  Box,
} from '@mui/material';

const drawerWidth = 150;

export default function MainLayout() {
  return (
    <Box
      sx={{
        display: 'flex',
        backgroundColor: '#f2e2cb',
        minHeight: '100vh',
        width: '100%',
        margin: 0,
        padding: 0,
      }}
    >
      {/* Top App Bar */}
      <AppBar
        position="fixed"
        sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}
      >
        <Toolbar>
          <Typography variant="h6" noWrap component="div">
            Enterprise Task Manager
          </Typography>
        </Toolbar>
      </AppBar>

      {/* Left Drawer */}
      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          [`& .MuiDrawer-paper`]: {
            width: drawerWidth,
            boxSizing: 'border-box',
            backgroundColor: '#1976d2',
            color: '#fff',
            borderRight: '1px solid rgba(255,255,255,0.2)',
          },
          '& .MuiListItemButton-root': {
            color: '#fff',
          },
          '& .MuiListItemButton-root:hover': {
            backgroundColor: 'rgba(255,255,255,0.15)',
          },
        }}
      >
        <Toolbar />
        <List>
          <ListItemButton component={Link} to="/">
            <ListItemText primary="Projects" />
          </ListItemButton>

          <ListItemButton component={Link} to="tasks">
            <ListItemText primary="Tasks" />
          </ListItemButton>

          {/* Add more navigation items here later */}
        </List>
      </Drawer>

      {/* Main Content Area */}
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          pt: 1,
          pr: 0,
          pb: 2,
          pl: 0,
          backgroundColor: '#f2e2cb',
          minHeight: '100vh',
        }}
      >
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
}
