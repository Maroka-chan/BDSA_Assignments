using System;

namespace Assignment4.Entities
{
    public interface ITagRepository : IDisposable
    {
        Tag Create(Tag tag); //should return the id of the new tag
        Tag read(int tagId); 
        bool Update(Tag tag);
        bool delete(int tagId);
    }
}