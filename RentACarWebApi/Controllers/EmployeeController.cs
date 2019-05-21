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
    public class EmployeeController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var empManager = new EmployeeManager())
            {
                // Get customers from business layer (Core App)
                List<Employees> employees = empManager.SelectAll();

                // Prepare a content
                var content = new ResponseContent<Employees>(employees);

                // Return content as a json and proper http response
                return new StandartResult<Employees>(content, Request);
            }
        }


        public IHttpActionResult Get(int id)
        {
            ResponseContent<Employees> content;

            using (var empManager = new EmployeeManager())
            {
                // Get customer from business layer (Core App)
                List<Employees> employees = null;
                try
                {
                    var c = empManager.SelectById(id);
                    if (c != null)
                    {
                        employees = new List<Employees>();
                        employees.Add(c);
                    }

                    // Prepare a content
                    content = new ResponseContent<Employees>(employees);

                    // Return content as a json and proper http response
                    return new XmlResult<Employees>(content, Request);
                }
                catch (Exception)
                {
                    // Prepare a content
                    content = new ResponseContent<Employees>(null);
                    return new XmlResult<Employees>(content, Request);
                }
            }
        }


        public IHttpActionResult Post([FromBody]Employees emp)
        {
            var content = new ResponseContent<Employees>(null);
            if (emp != null)
            {
                using (var empManager = new EmployeeManager())
                {
                    content.Result = empManager.Insert(emp) ? "1" : "0";

                    return new StandartResult<Employees>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Employees>(content, Request);
        }

        public IHttpActionResult Put(int id, [FromBody]Employees emp)
        {
            var content = new ResponseContent<Employees>(null);

            if (emp != null)
            {
                using (var empManager = new EmployeeManager())
                {
                    content.Result = empManager.Update(emp) ? "1" : "0";

                    return new StandartResult<Employees>(content, Request);
                }
            }

            content.Result = "0";

            return new StandartResult<Employees>(content, Request);
        }

        public IHttpActionResult Delete(int id)
        {
            var content = new ResponseContent<Employees>(null);

            using (var empManager = new EmployeeManager())
            {
                content.Result = empManager.DeletedById(id) ? "1" : "0";

                return new StandartResult<Employees>(content, Request);
            }
        }

        public IHttpActionResult Login(string UserName, string Password)
        {
            var content = new ResponseContent<Employees>(null);

            using (var empManager = new EmployeeManager())
            {
                content.Result = empManager.EmployeeLogin(UserName,Password) ? "1" : "0"; //WHATS CAN I DO SOMETIMES

                return new StandartResult<Employees>(content, Request);
            }
        }
    }
}