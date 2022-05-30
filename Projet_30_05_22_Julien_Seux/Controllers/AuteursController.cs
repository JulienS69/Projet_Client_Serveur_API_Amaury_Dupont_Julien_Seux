using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ORM;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Projet_30_05_22_Julien_Seux.Controllers
{
    public class AuteursController : Controller
    {
        private Model1 db = new Model1();

        // GET: Auteurs
        public async Task<ActionResult> Index()
        {
            //au lieu d'utiliser l'ORM, on utilise l'API REST
            string url = "https://localhost:44356/api/Auteurs"; // appel de l'api

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //test de succès
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Une erreur est survenue lors de l'appel de l'api");
                var Auteurs = await response.Content.ReadAsAsync<IEnumerable<Auteur>>();
                return View(Auteurs);
            }
        }

        // GET: Auteurs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auteur auteur = await db.Auteur.FindAsync(id);
            if (auteur == null)
            {
                return HttpNotFound();
            }
            return View(auteur);
        }

        // GET: Auteurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auteurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdAuteur,Nom,Prenom,DateNaissance")] Auteur auteur)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(auteur);
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://localhost:44356/api/Auteurs";
                    using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        request.Content = content;
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception(send.Content.ReadAsStringAsync().Result);
                        }
                        send.EnsureSuccessStatusCode();
                        return RedirectToAction("Index");

                    }
                }

            }

            return View(auteur);
        }

        // GET: Auteurs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auteur auteur = await db.Auteur.FindAsync(id);
            if (auteur == null)
            {
                return HttpNotFound();
            }
            return View(auteur);
        }

        // POST: Auteurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdAuteur,Nom,Prenom,DateNaissance")] Auteur auteur)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(auteur);
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://localhost:44356/api/Auteurs/{auteur.IdAuteur}";
                    using (var request = new HttpRequestMessage(HttpMethod.Put, url))
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        request.Content = content;
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception(send.Content.ReadAsStringAsync().Result);
                        }
                        send.EnsureSuccessStatusCode();
                        return RedirectToAction("Index");

                    }
                }
            }
            return View(auteur);
        }

        // GET: Auteurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (ModelState.IsValid)
            {
               
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://localhost:44356/api/Auteurs/{id}";
                    using (var request = new HttpRequestMessage(HttpMethod.Delete, url))
                    {
             
                    
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        if (!send.IsSuccessStatusCode)
                        {
                            throw new Exception(send.Content.ReadAsStringAsync().Result);
                        }
                        send.EnsureSuccessStatusCode();
                     

                    }
                }

            }
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
