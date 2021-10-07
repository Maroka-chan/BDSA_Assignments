using System;

namespace Assignment4.Entities
{
    public interface ITagRepository : IDisposable
    {
        int Create(Tag tag); //should return the id of the new tag
        Tag read(int tagId); 
        void Update(Tag tag);
        void delete(int tagId);
    }
}