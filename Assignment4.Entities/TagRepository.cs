using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        private readonly KanbanContext _context;
        public TagRepository(KanbanContext context)
        {
            _context = context;
        }
        
        public Tag Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return read(tag.Id); //kunne være fedt hvis vi kunne return read(tag.id) NOICE
        }

        public Tag read(int tagId)
        {
            var returnTag= from tag in _context.Tags
                                        where tag.Id == tagId
                                        select tag;
            
            return returnTag.FirstOrDefault();
            
        }

        public bool Update(Tag tag)
        {
            Tag tagToUpdate = read(tag.Id);
            if (tagToUpdate == null)
            {
                return false;
            }

            tagToUpdate.Id = tag.Id;
            tagToUpdate.Name = tag.Name;
            tagToUpdate.Tasks = tag.Tasks;

            _context.SaveChanges();
            
            return true;
        }

        public bool delete(int tagId)
        {
            Tag tagToRemove = read(tagId);
            if (tagToRemove == null)
            {
                return false;
            }
            
            _context.Tags.Remove(tagToRemove);
            _context.SaveChanges();
            
            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}