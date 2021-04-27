using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Elements.Api.Contracts.Entities;
using CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers;
using CMS.Elements.Api.Contracts.Interfaces.Repositories;

namespace CM.Elements.Api.BLL.BusinessServices
{
    public class ContactService : IContactService
    {
        private IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            if (contactRepository != null)
                _contactRepository = contactRepository;            
        }
        
        public async Task<List<Contact>> GetContacts()
        {
            return await _contactRepository.GetContacts();
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await _contactRepository.GetContactById(id);
        }

        public async Task<List<int>> AddContact(List<Contact> inputEt)
        {
            return await _contactRepository.AddContact(inputEt);
        }

        public async Task<ResponseData> UpdateContact(List<Contact> inputEt)
        {
           return await _contactRepository.UpdateContact(inputEt);
        }

        public async Task<ResponseData> DeleteContact(List<int> ids)
        {
            return await _contactRepository.DeleteContact(ids);
        }
    }
}
