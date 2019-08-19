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
                            //  Fecha_Nacimiento = Convert.ToDateTime("fecha_nacimiento")


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
                    return Redirect("/tabla");

                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Editar
        public ActionResult Editar(int Id)
        {

            tablaViewModel model = new tablaViewModel();

            using (crudEntities db = new crudEntities())
            {
                var oTabla = db.tabla.Find(Id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento = oTabla.fecha_nacimiento.Value;
                model.Id = oTabla.id;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(tablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (crudEntities db = new crudEntities())
                    {
                        var oTabla = db.tabla.Find(model.Id);
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("/tabla");

                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //Eliminar
        [HttpGet]
        public ActionResult Eliminar(int Id)
        {


            using (crudEntities db = new crudEntities())
            {
                var oTabla = db.tabla.Find(Id);
                db.tabla.Remove(oTabla);
                db.SaveChanges();
               

            }
            return Redirect("/tabla");
        }

    }
}