using RealEstate.Entities.Entites;
using RealEstate.Entities.ModelView;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RealEstate.Api.Controllers
{
    [RoutePrefix("api/demo")]
    public class ValuesController : ApiController
    {
        IUserService _userService;
        public ValuesController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET api/values
        [Route("get")]
        public async Task<IEnumerable<AppUserEntity>> Get()
        {
            return await _userService.GetAllUserIsBirthDay();
        }

        // GET api/values/5
        [Route("getid/{id}")]
        public AppUser Get(string id)
        {
            return _userService.GetById(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
