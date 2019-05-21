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
    public class TransactionController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var transManager = new TransactionManager())
            {
                // Get customers from business layer (Core App)
                List<Transactions> transactions = transManager.SelectAll();

                // Prepare a content
                var content = new ResponseContent<Transactions>(transactions);

                // Return content as a json and proper http response
                return new StandartResult<Transactions>(content, Request);
            }
        }


        public IHttpActionResult Get(int id)
        {
            ResponseContent<Transactions> content;

            using (var transManager = new TransactionManager())
            {
                // Get customer from business layer (Core App)
                List<Transactions> transactions = null;
                try
                {
                    var c = transManager.SelectById(id);
                    if (c != null)
                    {
                        transactions = new List<Transactions>();
                        transactions.Add(c);
                    }

                    // Prepare a content
                    content = new ResponseContent<Transactions>(transactions);

                    // Return content as a json and proper http response
                    return new XmlResult<Transactions>(content, Request);
                }
                catch (Exception)
                {
                    // Prepare a content
                    content = new ResponseContent<Transactions>(null);
                    return new XmlResult<Transactions>(content, Request);
                }
            }
        }


        public IHttpActionResult Post([FromBody]Transactions trans)
        {
            var content = new ResponseContent<Transactions>(null);
            if (trans != null)
            {
                using (var transManager = new TransactionManager())
                {
                    content.Result = transManager.Insert(trans) ? "1" : "0";

                    return new StandartResult<Transactions>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Transactions>(content, Request);
        }

        public IHttpActionResult Put(int id, [FromBody]Transactions trans)
        {
            var content = new ResponseContent<Transactions>(null);

            if (trans != null)
            {
                using (var transManager = new TransactionManager())
                {
                    content.Result = transManager.Update(trans) ? "1" : "0";

                    return new StandartResult<Transactions>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Transactions>(content, Request);
        }

        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Transactions>(null);

            using (var transManager = new TransactionManager())
            {
                content.Result = transManager.DeletedById(id) ? "1" : "0";

                return new StandartResult<Transactions>(content, Request);
            }
        }
    }
}