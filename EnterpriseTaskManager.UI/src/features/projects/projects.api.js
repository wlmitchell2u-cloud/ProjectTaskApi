import { api } from '../../api/ApiProvider';

const ProjectsApi = {
  async getAll() {
    return api.get('/projects');
  },

  async getById(id) {
    return api.get(`/projects/${id}`);
  },

  async create(project) {
    return api.post('/projects', project);
  },

  async update(id, project) {
    return api.put(`/projects/${id}`, project);
  },

  async delete(id) {
    return api.delete(`/projects/${id}`);
  },
};

export default ProjectsApi;
