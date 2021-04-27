using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Elements.Api.Contracts.Entities;

namespace CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers
{
    public interface IContactService
    {
        Task<List<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
        Task<List<int>> AddContact(List<Contact> inputEt);
        Task<ResponseData> UpdateContact(List<Contact> inputEt);
        Task<ResponseData> DeleteContact(List<int> ids);
    }

}