using CMS.Elements.Api.Contracts.Entities;
using CMS.Elements.Api.Contracts.Interfaces.BusinessLogicLayers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace CMS.Elements.Api.Tests
{
    public class ContactServiceTest
    {
        private ServiceProvider _serviceProvide;

        public ContactServiceTest()
        {
            _serviceProvide = new Startup().ServiceProvider;
        }

        [Fact]
        public async void GetContsactsTest()
        {
            var context = _serviceProvide.GetService<IContactService>();
            var response = await context.GetContacts();
            Assert.NotNull(response);
        }
        [Fact]
        public async void GetContsactByIdTest()
        {
            var context = _serviceProvide.GetService<IContactService>();
            var response = await context.GetContactById(1);
            Assert.NotNull(response);
        }
        [Fact]
        public async void DeleteContactTest()
        {
            var context = _serviceProvide.GetService<IContactService>();
            var response = await context.DeleteContact(new List<int>() { 1 });
            Assert.True(response.Status);
        }
        [Fact]
        public async void UpdateContactTest()
        {
            var request = new List<Contact>
            {
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Chnaged"
                }
            };
            var context = _serviceProvide.GetService<IContactService>();
            var response = await context.UpdateContact(request);
            Assert.True(response.Status);
        }
        [Fact]
        public async void AddContactTest()
        {
            var request = new List<Contact>
            {
                new Contact
                {
                    FirstName = "TestF",
                    LastName = "TestL",
                    Email ="Test@gmail.com",
                    PhoneNumber = "123456789",
                    Status = true
                }
            };
            var context = _serviceProvide.GetService<IContactService>();
            var response = await context.AddContact(request);
            Assert.NotNull(response);
        }
    }
}
