namespace CMS.Elements.Api.Web.Host.Models.Common
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
    }
}
