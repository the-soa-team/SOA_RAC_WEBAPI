using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using RentACar.Bll.Concretes;
using RentACar.Model.EntityModels;
using RentACarWebApi.Models;
using RentACarWebApi.Result;
using Microsoft.Ajax.Utilities;
using BankAppWebApi.Resultss;

namespace RentACarWebApi.Controllers
{
    public class CustomerController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var customerManager = new CustomerManager())
            {
                // Get customers from business layer (Core App)
                List<Customers> customers = customerManager.SelectAll();

                // Prepare a content
                var content = new ResponseContent<Customers>(customers);

                // Return content as a json and proper http response
                return new StandartResult<Customers>(content, Request);
            }
        }


        public IHttpActionResult Get(int id)
        {
            ResponseContent<Customers> content;

            using (var customerManager = new CustomerManager())
            {
                // Get customer from business layer (Core App)
                List<Customers> customers = null;
                try
                {
                    var c = customerManager.SelectById(id);
                    if (c != null)
                    {
                        customers = new List<Customers>();
                        customers.Add(c);
                    }

                    // Prepare a content
                    content = new ResponseContent<Customers>(customers);

                    // Return content as a json and proper http response
                    return new XmlResult<Customers>(content, Request);
                }
                catch (Exception)
                {
                    // Prepare a content
                    content = new ResponseContent<Customers>(null);
                    return new XmlResult<Customers>(content, Request);
                }
            }
        }


        public IHttpActionResult Post([FromBody]Customers customer)
        {
            var content = new ResponseContent<Customers>(null);
            if (customer != null)
            {
                using (var customerManager = new CustomerManager())
                {
                    content.Result = customerManager.Insert(customer) ? "1" : "0";

                    return new StandartResult<Customers>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Customers>(content, Request);
        }

        public IHttpActionResult Put(int id, [FromBody]Customers customer)
        {
            var content = new ResponseContent<Customers>(null);

            if (customer != null)
            {
                using (var customerManager = new CustomerManager())
                {
                    content.Result = customerManager.Update(customer) ? "1" : "0";

                    return new StandartResult<Customers>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Customers>(content, Request);
        }

        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Customers>(null);

            using (var customerManager = new CustomerManager())
            {
                content.Result = customerManager.DeletedById(id) ? "1" : "0";

                return new StandartResult<Customers>(content, Request);
            }
        }


    }
}