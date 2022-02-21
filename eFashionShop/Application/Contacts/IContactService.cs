using eFashionShop.Data.Entities;
using eFashionShop.ViewModels.Catalog.Contacts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFashionShop.Application.Contacts
{
    public interface IContactService
    {
        Task<bool> Update(Contact res);
        Task<List<Contact>> GetAll();
        Task<bool> Delete(int id);
        Task<bool> Create(ContactCreateVm res);
    }
}
