using System.Collections.Generic;

namespace ContactsProject.Models
{
    public interface IContactRepository
    {
        List<ContactModel> GetContactList();
        ContactModel GetContactById(int ContactId);
    }
}
