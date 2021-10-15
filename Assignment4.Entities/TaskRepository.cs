using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities {
    public class TaskRepository : ITaskRepository {
        private readonly KanbanContext _context;

        public TaskRepository(KanbanContext context) {
            _context = context;
        }

        public void Dispose() {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task) {
            var now = DateTime.UtcNow;
            var newTask = new Task {
                Title = task.Title,
                Description = task.Description,
                AssignedTo = GetAssignedTo(task.AssignedToId),
                State = State.New,
                Created = now,
                StateUpdated = now,
                Tags = GetTags(task.Tags).ToList()
            };
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return (Response.Created, newTask.Id);
        }

        public IReadOnlyCollection<TaskDTO> ReadAll() =>
            _context.Tasks
                .Select(t => new TaskDTO(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Created, 
                    t.AssignedTo.Id, 
                    GetTagNames(t.Tags),
                    t.State, 
                    t.StateUpdated))
                .ToList().AsReadOnly();
        
        public IReadOnlyCollection<TaskDTO> ReadAllRemoved() => 
            _context.Tasks
                .Select(t => new TaskDTO(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Created, 
                    t.AssignedTo.Id, 
                    GetTagNames(t.Tags),
                    t.State, 
                    t.StateUpdated))
                .Where(t => t.State==State.Removed).ToList().AsReadOnly();
        
        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag) => 
            _context.Tasks
                .Select(t => new TaskDTO(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Created, 
                    t.AssignedTo.Id, 
                    GetTagNames(t.Tags),
                    t.State, 
                    t.StateUpdated))
                .Where(t => t.Tags.Contains(tag)).ToList().AsReadOnly();

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId) => 
            _context.Tasks
                .Select(t => new TaskDTO(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Created, 
                    t.AssignedTo.Id, 
                    GetTagNames(t.Tags),
                    t.State, 
                    t.StateUpdated))
                .Where(t => t.AssignedToId==userId).ToList().AsReadOnly();

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state) => 
            _context.Tasks
                .Select(t => new TaskDTO(
                    t.Id, 
                    t.Title, 
                    t.Description, 
                    t.Created, 
                    t.AssignedTo.Id, 
                    GetTagNames(t.Tags),
                    t.State, 
                    t.StateUpdated))
                .Where(t => t.State==state).ToList().AsReadOnly();

        public TaskDetailsDTO Read(int taskId) {
            var tasks = from t in _context.Tasks
                where t.Id == taskId
                select new TaskDetailsDTO(
                    t.Id,
                    t.Title,
                    t.Description,
                    t.Created,
                    t.AssignedTo.Id,
                    t.Tags.Select(ta => ta.Name).ToHashSet(),
                    t.State,
                    t.StateUpdated
                );
            return tasks.FirstOrDefault();
        }

        public Response Update(TaskUpdateDTO task) {
            var entity = _context.Tasks.Find(task.Id);

            if (entity == null)
                return Response.NotFound;
            entity.Title = task.Title;
            entity.Description = task.Description;
            entity.Created = task.Created;
            entity.AssignedTo = GetAssignedTo(task.Id);
            entity.StateUpdated = DateTime.UtcNow;
            entity.Tags = GetTags(task.Tags).ToList();

            _context.SaveChanges();

            return Response.Updated;
        }

        public Response Delete(int taskId) {
            var entity = _context.Tasks.Find(taskId);

            if (entity == null)
                return Response.NotFound;
            if (entity.State is State.Closed or State.Resolved or State.Removed)
                return Response.Conflict;
            if (entity.State is State.Active) {
                Update(new TaskUpdateDTO {
                    Id = entity.Id,
                    State = State.Removed
                });
            }

            _context.Tasks.Remove(entity);
            _context.SaveChanges();

            return Response.Deleted;        }

        private User GetAssignedTo(int? id) => id == null ? null : _context.Users.FirstOrDefault(c => c.Id == id);

        private IEnumerable<Tag> GetTags(IEnumerable<string> tags) {
            var existing = _context.Tags.Where(p => tags.Contains(p.Name)).ToDictionary(p => p.Name);
            Console.WriteLine(existing);
            foreach (var tag in tags) {
                yield return existing.TryGetValue(tag, out var p) ? p : new Tag {Name = tag};
            }
        }

        private IReadOnlyCollection<string> GetTagNames(IEnumerable<Tag> tags) =>
            new ReadOnlyCollection<string>(tags.ToList().Select(t => t.Name).ToList());
    }
}
