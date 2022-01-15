namespace Reception.Model.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public int PhotoId { get; set; }

        public PostDto Post { get; set; }

        public string SecondName { get; set; }
    }
}