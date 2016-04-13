using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using getmestuff.Models;

namespace getmestuff.Controllers
{
    public class OrderNoteController : ApiController
    {
        private GetMeStuffContext db = new GetMeStuffContext();
        
        // PUT api/OrderNote/5
        public HttpResponseMessage PutOrderNote(int id, OrderNoteDto ordernoteDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != ordernoteDto.OrderNoteId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            OrderNote orderNote = ordernoteDto.ToEntity();
            Order order = db.Orders.Find(orderNote.OrderId);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (order.UserId != User.Identity.Name)
            {
                // Trying to modify a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            // Need to detach to avoid duplicate primary key exception when SaveChanges is called
            db.Entry(order).State = EntityState.Detached;
            db.Entry(orderNote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/OrderNote
        public HttpResponseMessage PostOrderNote(OrderNoteDto ordernoteDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Order order = db.Orders.Find(ordernoteDto.OrderId);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (order.UserId != User.Identity.Name)
            {
                // Trying to add a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            OrderNote orderNote = ordernoteDto.ToEntity();

            // Need to detach to avoid loop reference exception during JSON serialization
            db.Entry(order).State = EntityState.Detached;
            db.OrderNotes.Add(orderNote);
            db.SaveChanges();
            ordernoteDto.OrderNoteId = orderNote.OrderNoteId;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ordernoteDto);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ordernoteDto.OrderNoteId }));
            return response;
        }

        // DELETE api/OrderNote/5
        public HttpResponseMessage DeleteOrderNote(int id)
        {
            OrderNote orderNote = db.OrderNotes.Find(id);
            if (orderNote == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (db.Entry(orderNote.Order).Entity.UserId != User.Identity.Name)
            {
                // Trying to delete a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            OrderNoteDto orderNoteDto = new OrderNoteDto(orderNote);
            db.OrderNotes.Remove(orderNote);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, orderNoteDto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}