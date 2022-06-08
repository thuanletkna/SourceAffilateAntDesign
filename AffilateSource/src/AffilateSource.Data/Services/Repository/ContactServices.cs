using AffilateSource.Data.Configuration;
using AffilateSource.Data.DataEntity;
using AffilateSource.Data.DataEntity.Entities;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Contact;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Repository
{
    public class ContactServices : IContactServices
    {
        public ApplicationDbContext context;
        private readonly SqlConnectionConfiguration _configuration;
        public ContactServices(ApplicationDbContext context, SqlConnectionConfiguration configuration)
        {
            this.context = context;
            _configuration = configuration;
        }
        public async Task<ContactVm>GetContact()
        {
            var data = await context.Contacts.Select(x => new ContactVm()
            {
                Content = x.Content,
                ContentHome = x.ContentHome,
                Phone = x.Phone,
                Address = x.Address,
                Email = x.Email,
                Status = x.Status,
                FacebookLink = x.FacebookLink,
                ZaloLink = x.ZaloLink
            }).Where(x => x.Status==true).FirstOrDefaultAsync();
            return  data;
        }
        public async Task<List<ContactVm>> GetListContact()
        {
            var data = await context.Contacts.Select(x => new ContactVm()
            {
                ID = x.ID,
                FacebookLink = x.FacebookLink,
                ZaloLink = x.ZaloLink,
                Content = x.Content,
                ContentHome = x.ContentHome,
                Phone = x.Phone,
                Address = x.Address,
                Email = x.Email,
                Status = x.Status,
            }).Where(x => x.Status == true).ToListAsync();
            return data;
        }
        public async Task<ContactVm> CreateContacts(ContactVm contactVm)
        {
            var contact = new Contact()
            {
                Content = contactVm.Content,
                Phone = contactVm.Phone,
                Email = contactVm.Email,
                Address = contactVm.Address,
                ContentHome = contactVm.ContentHome,
                Status = true
            };
            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
            return contactVm;
        }
        public async Task<ContactVm> UpdateContacts(ContactVm contactVm)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                //Nếu user hoạt động thì nghỉ việc tạm thời sẽ bằng false và ngược lại. Set cứng ở frontend
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Id", contactVm.ID);
                ObjParm.Add("@Content", contactVm.Content);
                ObjParm.Add("@Status", contactVm.Status);
                ObjParm.Add("@Email", contactVm.Email);
                ObjParm.Add("@Address", contactVm.Address);
                ObjParm.Add("@Phone", contactVm.Phone);
                ObjParm.Add("@ContentHome", contactVm.ContentHome);
                ObjParm.Add("@FacebookLink", contactVm.FacebookLink);
                ObjParm.Add("@ZaloLink", contactVm.ZaloLink);
                await conn.ExecuteAsync("[CONTACT_Update]", ObjParm, commandType: CommandType.StoredProcedure);
                conn.Close();
                return contactVm;
            }
        }
        public async Task<ContactVm>GetContactById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.Value))
                {

                    ContactVm productQuickViewModel;
                    DynamicParameters ObjParm = new DynamicParameters();
                    ObjParm.Add("@Id", id);
                    productQuickViewModel = await conn.QuerySingleOrDefaultAsync<ContactVm>("[CONTACT_GetDetailById]", ObjParm, commandType: CommandType.StoredProcedure);
                    conn.Close();
                    return productQuickViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
