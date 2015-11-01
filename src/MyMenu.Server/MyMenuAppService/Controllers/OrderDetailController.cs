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
    public class OrderDetailController : TableController<OrderDetail>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyMenuContext context = new MyMenuContext();
            DomainManager = new EntityDomainManager<OrderDetail>(context, Request);
        }

        // GET tables/OrderDetail
        public IQueryable<OrderDetail> GetAllOrderDetail()
        {
            return Query(); 
        }

        // GET tables/OrderDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<OrderDetail> GetOrderDetail(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/OrderDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<OrderDetail> PatchOrderDetail(string id, Delta<OrderDetail> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/OrderDetail
        public async Task<IHttpActionResult> PostOrderDetail(OrderDetail item)
        {
            OrderDetail current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/OrderDetail/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteOrderDetail(string id)
        {
             return DeleteAsync(id);
        }

    }
}