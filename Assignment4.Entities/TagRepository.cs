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
            return tag; //kunne være fedt hvis vi kunne return read(tag.id) NOICE
        }

        public Tag read(int tagId)
        {
            return null;
            
        }

        public void Update(Tag tag)
        {
        }

        public void delete(int tagId)
        {
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}