using Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SageAPI.Controllers
{
    public class ContactPersonController : ApiController
    {
        public IEnumerable<KHKAnsprechpartner> Get()
        {
            using(SageAPIEntities entities = new SageAPIEntities())
            {
                return entities.KHKAnsprechpartner.ToList();
            }
        }
        public HttpResponseMessage Get(int id )
        {
            using (SageAPIEntities entities = new SageAPIEntities())
            {
                var entity = entities.KHKAnsprechpartner.FirstOrDefault(e => e.Nummer == id);
                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact Person with Id = " + id.ToString() + " Not Found");
                }
            }
        }
        public HttpResponseMessage Post([FromBody]KHKAnsprechpartner contactPerson)
        {
            try
            {
                using (SageAPIEntities entities = new SageAPIEntities())
                {
                    entities.KHKAnsprechpartner.Add(contactPerson);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, contactPerson);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + contactPerson.Nummer);
                    return message;

                }

            }
            catch (Exception ex)
            {

               return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }
    }
}
