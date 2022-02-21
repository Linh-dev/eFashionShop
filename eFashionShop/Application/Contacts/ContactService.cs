using eFashionShop.Data.EF;
using eFashionShop.Data.Entities;
using eFashionShop.Exceptions;
using eFashionShop.ViewModels.Catalog.Contacts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Application.Contacts
{
    public class ContactService : IContactService
    {
        private readonly EShopDbContext _context;

        public ContactService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ContactCreateVm res)
        {

            if (res == null) throw new EShopException("Create contact fail!");
            var contact = new Contact
            {
                Name = res.Name,
                Email = res.Email,
                PhoneNumber = res.PhoneNumber,
                Message = res.Message,
                Status = Data.Enums.Status.InActive,
            };
            _context.Contacts.Add(contact);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            if (id < 0) throw new EShopException("Delete contact fail!");
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Contact>> GetAll()
        {
            var res = from c in _context.Contacts select c;
            return await res.ToListAsync();
        }

        public async Task<bool> Update(Contact res)
        {
            if (res == null) throw new EShopException("Update fail!");
            _context.Contacts.Update(res);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
