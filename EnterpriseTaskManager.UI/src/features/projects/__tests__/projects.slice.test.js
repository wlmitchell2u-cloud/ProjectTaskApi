import reducer, {
  resetForm,
  setFormField,
  projectAdded,
} from '../projects.slice';

describe('projects slice', () => {
  it('returns the initial state', () => {
    const state = reducer(undefined, { type: '@@INIT' });

    expect(state.projects).toEqual([]);
    expect(state.projectForm).toEqual({
      name: '',
      description: '',
      ownerId: 1,
    });
    expect(state.status).toBe('idle');
    expect(state.error).toBeNull();
  });

  it('updates a form field', () => {
    const initialState = reducer(undefined, { type: '@@INIT' });

    const nextState = reducer(
      initialState,
      setFormField({ field: 'name', value: 'My Project' })
    );

    expect(nextState.projectForm.name).toBe('My Project');
    expect(nextState.projectForm.description).toBe('');
    expect(nextState.projectForm.ownerId).toBe(1);
  });

  it('resets the form', () => {
    const modifiedState = {
      projects: [],
      projectForm: {
        name: 'X',
        description: 'Y',
        ownerId: 1,
      },
      status: 'idle',
      error: null,
    };

    const nextState = reducer(modifiedState, resetForm());

    expect(nextState.projectForm).toEqual({
      name: '',
      description: '',
      ownerId: 1,
    });
  });

  it('adds a project', () => {
    const initialState = reducer(undefined, { type: '@@INIT' });

    const newProject = {
      id: 1,
      name: 'Test Project',
      description: 'Test description',
      ownerId: 1,
    };

    const nextState = reducer(initialState, projectAdded(newProject));

    expect(nextState.projects).toHaveLength(1);
    expect(nextState.projects[0]).toEqual(newProject);
  });
});
