using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EncuestasBack.Models;
using System.IO;
using EncuestasBack.Classes;

namespace EncuestasBack.Controllers.API
{
    public class ContactsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }

        // GET: api/Contacts/1
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, ContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.ContactId)
            {
                return BadRequest();
            }


            var contact = ToContact(request);
            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(ContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var contact = ToContact(request);
            db.Contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.ContactId }, contact);
        }

        private Contact ToContact(ContactRequest request)
        {
            return new Contact
            {
                ContactId = request.ContactId,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                BathroomsNumber = request.BathroomsNumber,
                IndependentWorkers = request.IndependentWorkers,
                NumberPeople = request.NumberPeople,
                RelativesAbroad = request.RelativesAbroad,
                PollDate = request.PollDate,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,  
            };
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.ContactId == id) > 0;
        }
    }
}