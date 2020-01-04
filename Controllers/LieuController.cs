using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tourisme_MVC_projet.Models;

namespace Tourisme_MVC_projet.Controllers
{
    public class LieuController : Controller
    {
        // GET: Lieu
        mydbEntities4 db = new mydbEntities4();

        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Commenting out original code to show how to use a raw SQL query.
            //Department department = await db.Departments.FindAsync(id);

            // Create and execute raw SQL query.
            string query = "SELECT * FROM Lieu WHERE zoneGeo = monastir";
            Lieux lieu = await db.Lieux.SqlQuery(query, id).SingleOrDefaultAsync();

            if (lieu == null)
            {
                return HttpNotFound();
            }
            return View(lieu);
        }
    }
}