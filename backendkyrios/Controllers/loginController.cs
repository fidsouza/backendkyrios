using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using backendkyrios.Models;

namespace backendkyrios.Controllers
{
    public class loginController : ApiController
    {
        private ContextKyriosbd db = new ContextKyriosbd();

        // GET: api/login/5
        [ResponseType(typeof(login))]
        public async Task<bool> Getlogin(string email , string senha)
        {
            object retornoUsuario;

            retornoUsuario =  await db.login.FirstOrDefaultAsync(l => email == l.userName && senha == l.password);

            if (retornoUsuario == null)
            {
                return false;
            }

            return true;
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