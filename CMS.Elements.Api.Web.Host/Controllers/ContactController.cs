using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers;
using CMS.Elements.Api.Web.Host.Models;
using CMS.Elements.Api.Web.Host.Models.Common;
using CMS.Elements.Api.Contracts.Entities;
using CMS.Elements.Api.Web.Host.Utilities;
using Microsoft.Extensions.Logging;

namespace CMS.Elements.Api.Web.Host.Controllers
{
    [ApiController]
    [Route("api")]
    public class ContactController : ControllerBase
    {
        private IContactService _contactServiceAgent;
        private ILogger<ContactController> _logger;
        public ContactController(IContactService contactServiceAgent, ILogger<ContactController> logger)
        {
            _contactServiceAgent = contactServiceAgent;
            _logger = logger;
        }

        [HttpGet("getcontactlist")]
        public async Task<ContactViewModelResponse> GetContactList()
        {
            _logger.LogInformation("Get contact list from method getcontactlist()");
             var resp = new ContactViewModelResponse()
            {
                ContactList = new List<ContactViewModel>()
            };
            var rtnList = await _contactServiceAgent.GetContacts();
            resp.ContactList =  AutoMapConverter.ConvertObjectCollection<Contact, ContactViewModel>(rtnList);
            _logger.LogInformation("Get contact list from method getcontactlist successful");
            return resp;
        }

        [HttpGet("GetContactById/{id:int}")]
        public async Task<ContactViewModel> GetContactById(int id)
        {
            _logger.LogInformation($"Get contact list from method GetContactById() with id : { id }");
            var eContact = await _contactServiceAgent.GetContactById(id);
            _logger.LogInformation("Get contact list from method GetContactById() successful");
            return AutoMapConverter.ConvertObject<Contact, ContactViewModel>(eContact);
        }

        [HttpPost("addcontact")]
        public async Task<AddContactsResponse> AddContacts([FromBody] List<ContactViewModel> mContact)
        {
            _logger.LogInformation("Add contact list from method AddContacts()");
            var eContact = AutoMapConverter.ConvertObjectCollection<ContactViewModel, Contact>(mContact);
            var result = await _contactServiceAgent.AddContact(eContact);
            var addContactResponse = new Models.AddContactsResponse()
            {
                ContactIdList = result
            };
            _logger.LogInformation($"Add contact list from method AddContacts() successful with count : {addContactResponse?.ContactIdList.Count}");
            return addContactResponse;
        }

        [HttpPost("updatecontact")]
        public async Task<ResponseDataViewModel> UpdateContact([FromBody] List<ContactViewModel> mContact)
        {
            _logger.LogInformation("Update contact list from method UpdateContact()");
            var eContact = AutoMapConverter.ConvertObjectCollection<ContactViewModel, Contact>(mContact);
            var result = await _contactServiceAgent.UpdateContact(eContact);
            _logger.LogInformation("Update contact list from method UpdateContact() Successful");
            return AutoMapConverter.ConvertObject<ResponseData, ResponseDataViewModel > (result);
        }
        
        [HttpPost("deletecontact")]
        public async Task<ResponseDataViewModel> DeleteContact([FromBody] List<int> ids)
        {
            _logger.LogInformation($"Delete contact list from method UpdateContact() { ids.Count}");
            var result = await _contactServiceAgent.DeleteContact(ids);
            _logger.LogInformation("Delete contact list from method UpdateContact() successful");
            return AutoMapConverter.ConvertObject<ResponseData, ResponseDataViewModel>(result);
        }
    }
}
