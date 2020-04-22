using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP01M06.Persistance;
using BO;
using TP01M06.Models;

namespace TP01M06.Controllers
{
    public class SamouraiController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }

            return View(samourai);
        }

        public ActionResult Create()
        {
            var samouraiGestionVM = new SamouraiViewModel();
            samouraiGestionVM.Armes = db.Armes.ToList();

            return View(samouraiGestionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiViewModel samouraiGestionVM)
        {
            if (ModelState.IsValid)
            {
                if (samouraiGestionVM.IdArmeDisponible.HasValue)
                {
                    samouraiGestionVM.Samourai.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiGestionVM.IdArmeDisponible.Value);
                }
                db.Samourais.Add(samouraiGestionVM.Samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            samouraiGestionVM.Armes = db.Armes.ToList();
            return View(samouraiGestionVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            var samouraiGestionVM = new SamouraiViewModel();
            samouraiGestionVM.Armes = db.Armes.ToList();
            samouraiGestionVM.Samourai = samourai;

            if (samourai.Arme != null)
            {
                samouraiGestionVM.IdArmeDisponible = samourai.Arme.Id;
            }
            return View(samouraiGestionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiViewModel samouraiGestionVM)
        {
            if (ModelState.IsValid)
            {
                var samouraidb = db.Samourais.Find(samouraiGestionVM.Samourai.Id);
                samouraidb.Force = samouraiGestionVM.Samourai.Force;
                samouraidb.Nom = samouraiGestionVM.Samourai.Nom;
                samouraidb.Arme = null;

                if (samouraiGestionVM.IdArmeDisponible.HasValue)
                {
                    samouraidb.Arme = db.Armes.FirstOrDefault(a => a.Id == samouraiGestionVM.IdArmeDisponible.Value);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            samouraiGestionVM.Armes = db.Armes.ToList();
            return View(samouraiGestionVM);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Samourai samourai = db.Samourais.Find(id);

            if (samourai == null)
            {
                return HttpNotFound();
            }

            return View(samourai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}
