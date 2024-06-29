namespace Reception.Server.Data.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public PersonAdditional AdditionalInfo { get; set; }

        public string Comment { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public Post Post { get; set; }

        public string SecondName { get; set; }
    }
}
