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
        int Mandant = 1;
        public IEnumerable<KHKAnsprechpartner> Get()
        {
            using (SageAPIEntities entities = new SageAPIEntities())
            {
                return entities.KHKAnsprechpartner.ToList();
            }
        }
        // Get method
        public HttpResponseMessage Get(int id)
        {
            using (SageAPIEntities entities = new SageAPIEntities())
            {
                var entity = entities.KHKAnsprechpartner.FirstOrDefault(e => e.Nummer == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact Person with Id = " + id.ToString() + " Not Found");
                }
            }
        }
        // Post method
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

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        // Delete Method
        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                using (SageAPIEntities entities = new SageAPIEntities())
                {
                    var entity = entities.KHKAnsprechpartner.FirstOrDefault(e => e.Nummer == Id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact Person with Id = " + Id.ToString() + " Not Found");

                    }
                    else
                    {
                        entities.KHKAnsprechpartner.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        //Put method
        public HttpResponseMessage Put(int Id, [FromBody] KHKAnsprechpartner contactPerson)
        {
            using (SageAPIEntities entities = new SageAPIEntities())
            {
                try
                {
                    var entity = entities.KHKAnsprechpartner.FirstOrDefault(e => e.Nummer == Id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact Person with Id = " + Id.ToString() + " Not Found to update");

                    }
                    else if (entity.Mandant != Mandant)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Mandant must be " + Mandant);


                    }
                    else
                    {
                        
                        entity.Adresse = contactPerson.Adresse;
                        entity.Ansprechpartner = contactPerson.Ansprechpartner;
                        entity.Gruppe = contactPerson.Gruppe;
                        entity.Titel = contactPerson.Titel;
                        entity.Vorname = contactPerson.Vorname;
                        entity.Nachname = contactPerson.Nachname;
                        entity.Position = contactPerson.Position;
                        entity.Abteilung = contactPerson.Abteilung;
                        entity.Anrede = contactPerson.Anrede;
                        entity.Telefon = contactPerson.Telefon;
                        entity.Telefax = contactPerson.Telefax;
                        entity.Mobilfunk = contactPerson.Mobilfunk;
                        entity.EMail = contactPerson.EMail;
                        entity.Geburtsdatum = contactPerson.Geburtsdatum;
                        entity.Memo = contactPerson.Memo;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);


                    }
                }
                    
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}