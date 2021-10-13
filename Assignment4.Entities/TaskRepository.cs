using System.Collections.Generic;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO Read(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TaskUpdateDTO task)
        {
            throw new System.NotImplementedException();
        }

        public Response Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}
