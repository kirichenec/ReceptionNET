namespace Reception.Server.Data.Model
{
    public class Person : BaseModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string PhotoPath { get; set; }

        public string Post { get; set; }

        public string SecondName { get; set; }
    }
}