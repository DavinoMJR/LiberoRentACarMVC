using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LiberoRentACar.Model;
using LiberoRentACar.Model.Services;
using AutoMapper;
using LiberoRentACarASPMVC.ViewModels;
using LiberoRentACarModel;

namespace LiberoRentACarASPMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FabricanteController : Controller
    {
        private readonly IService<Fabricante> service;

        public FabricanteController(IService<Fabricante> service)
        {
            this.service = service;
        }

        // GET: Fabricante
        public ActionResult Index()
        {
            IEnumerable<Fabricante> fab = service.Listar();
            IEnumerable<FabricanteViewModel> fabVM = Mapper.Map<IEnumerable<Fabricante>, IEnumerable<FabricanteViewModel>>(fab);
            return View(fabVM);
        }

        // GET: Fabricante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Fabricante fabricante = service.Buscar(id);
                FabricanteViewModel fabVM = Mapper.Map<Fabricante, FabricanteViewModel>(fabricante);
                return View(fabVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // GET: Fabricante/Create
        public ActionResult Create()
        {
            FabricanteViewModel fabVM = new FabricanteViewModel();
            return View(fabVM);
        }

        // POST: Fabricante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FabricanteViewModel fabVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Fabricante fab = Mapper.Map<FabricanteViewModel, Fabricante>(fabVM);
                    service.Adicionar(fab);

                    return RedirectToAction("Index");
                }
                catch (BusinessException ex)
                {
                    return HttpNotFound(ex.Message);
                }
            }

            return View(fabVM);
        }

        // GET: Fabricante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Fabricante fabricante = service.Buscar(id);
                FabricanteViewModel fabVM = Mapper.Map<Fabricante, FabricanteViewModel>(fabricante);
                return View(fabVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // POST: Fabricante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FabricanteViewModel fabVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Fabricante fab = Mapper.Map<FabricanteViewModel, Fabricante>(fabVM);
                    service.Adicionar(fab);
                    return RedirectToAction("Index");
                }
                catch (BusinessException ex)
                {
                    return HttpNotFound(ex.Message);
                }
            }
            return View(fabVM);
        }

        // GET: Fabricante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Fabricante fab = service.Buscar(id);
                FabricanteViewModel fabVM = Mapper.Map<Fabricante, FabricanteViewModel>(fab);
                return View(fabVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }            
        }

        // POST: Fabricante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                service.Remover(id);
                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }   
    }
}
