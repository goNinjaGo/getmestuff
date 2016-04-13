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
    [Authorize]
    public class OrderController : ApiController
    {
        private GetMeStuffContext db = new GetMeStuffContext();

        // GET api/Order
        public IEnumerable<OrderDto> GetOrders()
        {
            return db.Orders
                .Include("LineItems")
                .Include("OrderNotes")
                .Where(u => u.UserId == User.Identity.Name)
                .OrderByDescending(u => u.OrderId)
                .AsEnumerable()
                .Select(order => new OrderDto(order));
        }

        // GET api/Order/5
        public OrderDto GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            if (order.UserId != User.Identity.Name)
            {
                // Trying to modify a record that does not belong to the user
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }

            return new OrderDto(order);
        }

        // PUT api/Order/5
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PutOrder(int id, OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != orderDto.OrderId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Order order = orderDto.ToEntity();
            if (db.Entry(order).Entity.UserId != User.Identity.Name)
            {
                // Trying to modify a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            db.Entry(order).State = EntityState.Modified;

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

        // POST api/Order
        [ValidateHttpAntiForgeryToken]
        public HttpResponseMessage PostOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            orderDto.UserId = User.Identity.Name;
            Order order = orderDto.ToEntity();
            db.Orders.Add(order);
            db.SaveChanges();
            orderDto.OrderId = order.OrderId;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, orderDto);
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = orderDto.OrderId }));
            return response;
            
        }

        // DELETE api/Order/5
        public HttpResponseMessage DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (db.Entry(order).Entity.UserId != User.Identity.Name)
            {
                // Trying to delete a record that does not belong to the user
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            OrderDto orderDto = new OrderDto(order);
            db.Orders.Remove(order);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.OK, orderDto);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}