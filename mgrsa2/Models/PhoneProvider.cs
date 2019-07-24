namespace mgrsa2.Models
{
    public class PhoneProvider
    {

        public int PhoneProviderId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public int FromLength { get; set; }

        public int MaxLength { get; set; }

        //NAVIGATION PROPERTIES
        //-------------------------------------------------------

        //public int ProfileId { get; set; }
        //public Profile Profile { get; set; }       


    }
}
