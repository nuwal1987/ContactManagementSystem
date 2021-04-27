using CMS.Elements.Api.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Elements.Api.Contracts.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
        Task<List<int>> AddContact(List<Contact> inputEt);
        Task<ResponseData> UpdateContact(List<Contact> inputEt);
        Task<ResponseData> DeleteContact(List<int> ids);
    }
}
