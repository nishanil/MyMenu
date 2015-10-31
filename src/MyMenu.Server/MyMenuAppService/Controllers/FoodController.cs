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
    public class FoodController : TableController<Food>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyMenuContext context = new MyMenuContext();
            DomainManager = new EntityDomainManager<Food>(context, Request);
        }

        // GET tables/Food
        public IQueryable<Food> GetAllFood()
        {
            return Query(); 
        }

        // GET tables/Food/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Food> GetFood(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Food/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Food> PatchFood(string id, Delta<Food> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Food
        public async Task<IHttpActionResult> PostFood(Food item)
        {
            Food current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Food/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFood(string id)
        {
             return DeleteAsync(id);
        }

    }
}