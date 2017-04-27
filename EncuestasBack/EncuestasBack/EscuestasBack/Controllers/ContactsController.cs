using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EncuestasBack.Models;
using EncuestasBack.Classes;

namespace EncuestasBack.Controllers
{
    public class ContactsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Contacts
        public ActionResult Index()
        {
            
            return View(db.Contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactView view)
        {
            if (ModelState.IsValid)
            {
                var contact = ToContact(view);
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Contact ToContact(ContactView view)
        {
            return new Contact
            {
                ContactId = view.ContactId,
                BathroomsNumber = view.BathroomsNumber,
                IndependentWorkers = view.IndependentWorkers,
                NumberPeople = view.NumberPeople,
                PollDate = view.PollDate,
                RelativesAbroad = view.RelativesAbroad,
                EmailAddress = view.EmailAddress,
                FirstName = view.FirstName,
                LastName = view.LastName,
                PhoneNumber = view.PhoneNumber,                   
            };
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = db.Contacts.Find(id);

            if (contact == null)
            {
                return HttpNotFound();
            }

            var view = ToView(contact);
            return View(view);
        }

        private ContactView ToView(Contact contact)
        {
            return new ContactView
            {
                ContactId = contact.ContactId,
                EmailAddress = contact.EmailAddress,
                BathroomsNumber = contact.BathroomsNumber,
                IndependentWorkers = contact.IndependentWorkers,
                RelativesAbroad = contact.RelativesAbroad,
                NumberPeople = contact.NumberPeople,
                PollDate = contact.PollDate,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactView view)
        {
            if (ModelState.IsValid)
            {
                
                var contact = ToContact(view);
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
