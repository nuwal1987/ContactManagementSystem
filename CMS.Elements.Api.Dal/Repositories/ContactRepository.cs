using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Elements.Api.Dal.RA.Models;
using CMS.Elements.Api.Contracts.Interfaces.Repositories;
using CMS.Elements.Api.Contracts.Entities;
using CMS.Elements.Api.Dal.RA.Models.Utilities;
using EFCore.BulkExtensions;
using System.Linq;

namespace CMS.Elelment.Api.DAL.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private ContactContext contactDBContext;
        public ContactRepository(ContactContext context)
        {
            contactDBContext = context;
        }

        public async Task<List<Contact>> GetContacts()
        {
            using (var context = contactDBContext)
            {
                var result = await context.ContactInformation.ToListAsync();
                return TypeMapperConverter.ConvertObjectCollection<ContactInformation, Contact>(result);
            }
        }

        public async Task<Contact> GetContactById(int id)
        {
            using (var context = contactDBContext)
            {
                var result = await context.ContactInformation.FirstOrDefaultAsync(x => x.ContactId == id);
                return TypeMapperConverter.ConvertObject<ContactInformation, Contact>(result);
            }
        }

        public async Task<List<int>> AddContact(List<Contact> inputEt)
        {
            var entity = TypeMapperConverter.ConvertObjectCollection<Contact, ContactInformation>(inputEt);
            using (var context = contactDBContext)
            {
                contactDBContext.BulkInsert(entity, options =>
                {
                    options.PreserveInsertOrder = false;
                    options.SetOutputIdentity = true;
                    options.BulkCopyTimeout = 100;
                });
            }
            return entity.Select(x => x.ContactId).ToList();
        }

        public async Task<ResponseData> UpdateContact(List<Contact> inputEt)
        {
            var response = new ResponseData
            {
                Status = false
            };
            var contactIds = inputEt.Select(x => x.ContactId).ToList();
            //Get entity to be updated
            using (var context = contactDBContext)
            {
                var matchingContact = await context.ContactInformation.Where(
                    x => contactIds.Contains(x.ContactId)).ToListAsync();
                matchingContact.ForEach(x =>
                {
                    foreach (var item in inputEt)
                    {
                        if (x.ContactId == item.ContactId)
                        {
                            if (!string.IsNullOrEmpty(item.FirstName)) x.FirstName = item.FirstName;
                            if (!string.IsNullOrEmpty(item.LastName)) x.LastName = item.LastName;
                            if (!string.IsNullOrEmpty(item.PhoneNumber)) x.PhoneNumber = item.PhoneNumber;
                            if (!string.IsNullOrEmpty(item.Email)) x.Email = item.Email;
                            if (item.Status != false) x.Status = item.Status;
                        }
                    }
                });
                await contactDBContext.BulkUpdateAsync(matchingContact);
            };
            response.Status = true;
            response.Message = "Updated Contact Successfully";
            return response;
        }

        public async Task<ResponseData> DeleteContact(List<int> ids)
        {
            var response = new ResponseData
            {
                Status = false
            };
            using (var context = contactDBContext)
            {
                context.ContactInformation.RemoveRange(context.ContactInformation.Where(x => ids.Contains(x.ContactId)));
                await context.SaveChangesAsync();
            }
            response.Status = true;
            response.Message = "Deleted Contact Successfully";
            return response;
        }
    }
}
