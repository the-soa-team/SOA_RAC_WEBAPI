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
    public class CarDetailsController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var cdManager = new CarDetailsManager())
            {
                // Get cars from business layer (Core App)
                List<CarDetail> cds = cdManager.SelectAll();

                // Prepare a content
                var content = new ResponseContent<CarDetail>(cds);

                // Return content as a json and proper http response
                return new StandartResult<CarDetail>(content, Request);
            }
        }


        public IHttpActionResult Get(int id)
        {
            ResponseContent<CarDetail> content;

            using (var cdManager = new CarDetailsManager())
            {
                // Get car from business layer (Core App)
                List<CarDetail> cds = null;
                try
                {
                    var c = cdManager.SelectById(id);
                    if (c != null)
                    {
                        cds = new List<CarDetail>();
                        cds.Add(c);
                    }

                    // Prepare a content
                    content = new ResponseContent<CarDetail>(cds);

                    // Return content as a json and proper http response
                    return new XmlResult<CarDetail>(content, Request);
                }
                catch (Exception)
                {
                    // Prepare a content
                    content = new ResponseContent<CarDetail>(null);
                    return new XmlResult<CarDetail>(content, Request);
                }
            }
        }


        public IHttpActionResult Post([FromBody]CarDetail cds)
        {
            var content = new ResponseContent<CarDetail>(null);
            if (cds != null)
            {
                using (var cdManager = new CarDetailsManager())
                {
                    content.Result = cdManager.Insert(cds) ? "1" : "0";

                    return new StandartResult<CarDetail>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<CarDetail>(content, Request);
        }

        public IHttpActionResult Put(int id, [FromBody]CarDetail cds)
        {
            var content = new ResponseContent<CarDetail>(null);

            if (cds != null)
            {
                using (var cdManager = new CarDetailsManager())
                {
                    content.Result = cdManager.Update(cds) ? "1" : "0";

                    return new StandartResult<CarDetail>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<CarDetail>(content, Request);
        }

        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<CarDetail>(null);

            using (var cdManager = new CarDetailsManager())
            {
                content.Result = cdManager.DeletedById(id) ? "1" : "0";

                return new StandartResult<CarDetail>(content, Request);
            }
        }

        public IHttpActionResult Delete([FromBody]CarDetail cds)
        {
            var content = new ResponseContent<CarDetail>(null);
            if (cds != null)
            {
                using (var cdManager = new CarDetailsManager())
                {
                    content.Result = cdManager.Delete(cds) ? "1" : "0";

                    return new StandartResult<CarDetail>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<CarDetail>(content, Request);
        }

        public IHttpActionResult DailyKmControl(int transID, int carID,DateTime date,int dailyKm )
        {
            var content = new ResponseContent<CarDetail>(null);

            using (var cdManager = new CarDetailsManager())
            {
                content.Result = cdManager.DailyKmControl(transID,carID,date,dailyKm) != null ? "1" : "0";

                return new StandartResult<CarDetail>(content, Request);
            }
        }

        public IHttpActionResult CarDetailsAddTable(Cars car, Transactions trans, DateTime date, int dailyKm)
        {
            var content = new ResponseContent<CarDetail>(null);

            using (var cdManager = new CarDetailsManager())
            {
                content.Result = cdManager.CarDetailsAddTable(car, trans, date, dailyKm) != null ? "1" : "0";

                return new StandartResult<CarDetail>(content, Request);
            }
        }
    }
}