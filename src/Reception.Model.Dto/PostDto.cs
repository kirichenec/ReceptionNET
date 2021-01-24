using Reception.Model.Interface;

namespace Reception.Model.Dto
{
    public class PostDto : IPost
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Name { get; set; }
    }
}