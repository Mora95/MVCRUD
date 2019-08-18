using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCRUD.Models;
using MVCCRUD.Models.ViewModels;

namespace MVCCRUD.Controllers
{
    public class tablaController : Controller
    {
        // GET: tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using (crudEntities db = new crudEntities())
            {
                 lst = (from d in db.tabla
                           select new ListTablaViewModel
                           {
                               Id = d.id,
                               Nombre = d.nombre,
                               Correo = d.correo,
                            //   Fecha_Nacimiento = Convert.ToDateTime("fecha_nacimiento")


            }).ToList();

            }


            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(tablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (crudEntities db = new crudEntities())
                    {
                        var oTabla = new tabla();
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        db.tabla.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Redirect("/");

                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}