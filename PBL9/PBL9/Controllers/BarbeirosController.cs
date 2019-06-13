using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL9.Models;

namespace PBL9.Controllers
{
    public class BarbeirosController : Controller
    {
        private PBL9Context db = new PBL9Context();
        // GET: Usuarios/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Nome,Senha")] Barbeiro barbeiro)
        {
            if (ModelState.IsValid)
            {
                var u = db.Barbeiroes.Where(user => user.Nome == barbeiro.Nome && user.Senha == barbeiro.Senha).FirstOrDefault();
                if (u != null)
                {
                    Session["BarbeiroId"] = u.BarbeiroId;
                    Barbeiro barb = db.Barbeiroes.Find(u.BarbeiroId);
                    TempData["Barbeiro"] = barb;
                    return RedirectToAction("TelaInicial");
                }
            }

            return View(barbeiro);
        }
        //GET:Barbeiros/TelaInicial
        public ActionResult TelaInicial()
        {
            if (TempData["Barbeiro"] != null)
            {
                Barbeiro barb = TempData["Barbeiro"] as Barbeiro;
                ViewBag.BarbeiroNome = barb.Nome;
                int anosatual = DateTime.Today.Year;
                int anostrab = anosatual - barb.AnoEntrada;
                ViewBag.anostrab = anostrab;
            }

            return View();
        }

        // GET: Barbeiros
        public ActionResult Index()
        {
            return View(db.Barbeiroes.ToList());
        }

        // GET: Barbeiros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barbeiro barbeiro = db.Barbeiroes.Find(id);
            if (barbeiro == null)
            {
                return HttpNotFound();
            }
            return View(barbeiro);
        }

        // GET: Barbeiros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Barbeiros/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarbeiroId,Nome,Senha,AnoEntrada,Endereco")] Barbeiro barbeiro)
        {
            if (ModelState.IsValid)
            {
                db.Barbeiroes.Add(barbeiro);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(barbeiro);
        }

        // GET: Barbeiros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barbeiro barbeiro = db.Barbeiroes.Find(id);
            if (barbeiro == null)
            {
                return HttpNotFound();
            }
            return View(barbeiro);
        }

        // POST: Barbeiros/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarbeiroId,Nome,Senha,AnoEntrada,Endereco")] Barbeiro barbeiro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barbeiro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barbeiro);
        }

        // GET: Barbeiros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barbeiro barbeiro = db.Barbeiroes.Find(id);
            if (barbeiro == null)
            {
                return HttpNotFound();
            }
            return View(barbeiro);
        }

        // POST: Barbeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Barbeiro barbeiro = db.Barbeiroes.Find(id);
            db.Barbeiroes.Remove(barbeiro);
            db.SaveChanges();
            return RedirectToAction("Index");
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
