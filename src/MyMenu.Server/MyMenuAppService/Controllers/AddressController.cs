using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MyMenuAppService.DataObjects;
using MyMenuAppService.Models;

namespace MyMenuAppService.Controllers
{
    public class AddressController : TableController<Address>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyMenuContext context = new MyMenuContext();
            DomainManager = new EntityDomainManager<Address>(context, Request);
        }

        // GET tables/Address
        public IQueryable<Address> GetAllAddress()
        {
            return Query(); 
        }

        // GET tables/Address/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Address> GetAddress(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Address/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Address> PatchAddress(string id, Delta<Address> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Address
        public async Task<IHttpActionResult> PostAddress(Address item)
        {
            Address current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Address/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAddress(string id)
        {
             return DeleteAsync(id);
        }

    }
}