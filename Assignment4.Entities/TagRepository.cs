using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        private readonly KanbanContext _context;
        public TagRepository(KanbanContext context)
        {
            _context = context;
        }

        public TagDTO Read(int tagId)
        {
            var returnTag = from tag in _context.Tags
                where tag.Id == tagId
                select new TagDTO(tag.Id, tag.Name);

            return returnTag.FirstOrDefault();
        }

        public Response Update(TagUpdateDTO tag)
        {
            var tagToUpdate = _context.Tags.Find(tag.Id);
            if (tagToUpdate == null) return Response.NotFound;

            tagToUpdate.Name = tag.Name;
            tagToUpdate.Id = tag.Id;
            
            _context.SaveChanges();
            
            return Response.Updated;
        }

        public Response Delete(int tagId, bool force = false)
        {
            var tag = _context.Tags.Find(tagId);
            if (tag.Tasks?.Any() == true && !force) return Response.Conflict;
            
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Response.Deleted;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public (Response Response, int TagId) Create(TagCreateDTO tag)
        {
            var newTag = new Tag{Name = tag.Name};
            _context.Tags.Add(newTag);
            _context.SaveChanges();
            return (Response.Created, newTag.Id);
        }

        public IReadOnlyCollection<TagDTO> ReadAll()
        {
            var tagDTOList =_context.Tags.Select(x => new TagDTO(x.Id,x.Name)).ToList().AsReadOnly();
            return tagDTOList;
        }
    }
}