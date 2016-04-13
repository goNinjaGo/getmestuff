using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using getmestuff.Filters;
using getmestuff.Models;

namespace getmestuff.Controllers
{
    public class LineItemController : ApiController
    {
        private GetMeStuffContext db = new GetMeStuffContext();        

        // PUT api/LineItem/5
        public HttpResponseMessage PutLineItem(int id, LineItemDto lineitemDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != lineitemDto.LineItemId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            LineItem lineItem = lineitemDto.ToEntity();
            Order order = db.Orders.Find(lineItem.OrderId);
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
            db.Entry(lineItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/LineItem
        public HttpResponseMessage PostLineItem(LineItemDto lineitemDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Order order = db.Orders.Find(lineitemDto.OrderId);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (order.UserId != User.Identity.Name)
            {
                // Trying to add a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            LineItem lineItem = lineitemDto.ToEntity();

            // Need to detach to avoid loop reference exception during JSON serialization
            db.Entry(order).State = EntityState.Detached;
            db.LineItems.Add(lineItem);
            db.SaveChanges();
            lineitemDto.LineItemId = lineItem.LineItemId;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, lineitemDto);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = lineitemDto.LineItemId }));
            return response;
            
        }

        // DELETE api/LineItem/5
        public HttpResponseMessage DeleteLineItem(int id)
        {
            LineItem lineitem = db.LineItems.Find(id);
            if (lineitem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (db.Entry(lineitem.Order).Entity.UserId != User.Identity.Name)
            {
                // Trying to delete a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            LineItemDto lineItemDto = new LineItemDto(lineitem);
            db.LineItems.Remove(lineitem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lineItemDto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}