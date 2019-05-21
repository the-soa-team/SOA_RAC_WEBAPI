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
    public class CarController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var carManager = new CarManager())
            {
                // Get cars from business layer (Core App)
                List<Cars> cars = carManager.SelectAll();

                // Prepare a content
                var content = new ResponseContent<Cars>(cars);

                // Return content as a json and proper http response
                return new StandartResult<Cars>(content, Request);
            }
        }


        public IHttpActionResult Get(int id)
        {
            ResponseContent<Cars> content;

            using (var carManager = new CarManager())
            {
                // Get car from business layer (Core App)
                List<Cars> cars = null;
                try
                {
                    var c = carManager.SelectById(id);
                    if (c != null)
                    {
                        cars = new List<Cars>();
                        cars.Add(c);
                    }

                    // Prepare a content
                    content = new ResponseContent<Cars>(cars);

                    // Return content as a json and proper http response
                    return new XmlResult<Cars>(content, Request);
                }
                catch (Exception)
                {
                    // Prepare a content
                    content = new ResponseContent<Cars>(null);
                    return new XmlResult<Cars>(content, Request);
                }
            }
        }


        public IHttpActionResult Post([FromBody]Cars car)
        {
            var content = new ResponseContent<Cars>(null);
            if (car != null)
            {
                using (var carManager = new CarManager())
                {
                    content.Result = carManager.Insert(car) ? "1" : "0";

                    return new StandartResult<Cars>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Cars>(content, Request);
        }

        public IHttpActionResult Put(int id, [FromBody]Cars car)
        {
            var content = new ResponseContent<Cars>(null);

            if (car != null)
            {
                using (var carManager = new CarManager())
                {
                    content.Result = carManager.Update(car) ? "1" : "0";

                    return new StandartResult<Cars>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Cars>(content, Request);
        }

        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Cars>(null);

            using (var carManager = new CarManager())
            {
                content.Result = carManager.DeletedById(id) ? "1" : "0";

                return new StandartResult<Cars>(content, Request);
            }
        }
    }
}