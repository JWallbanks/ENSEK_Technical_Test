namespace Web.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
