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
            var returnTag = _context.Tags.Find(tagId);
            return new TagDTO(returnTag.Id, returnTag.Name);
        }

        public Response Update(TagUpdateDTO tag)
        {
            var tagToUpdate = _context.Tags.Find(tag.Id);
            if (tagToUpdate == null) return Response.NotFound;
            
            _context.Tags.Update(tagToUpdate);
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
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TagDTO> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}