using AutoMapper;
using LiberoRentACar.Model;
using LiberoRentACar.Model.Services;
using LiberoRentACarASPMVC.ViewModels;
using LiberoRentACarModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LiberoRentACarASPMVC.Controllers
{
    [Authorize(Roles = "Admin,Funcionario")]
    public class ModeloController : Controller
    {
        private readonly IModeloService service;
       
        public ModeloController(IModeloService service)
        {
            this.service = service;
        }

        // GET: Modelo
        public ActionResult Index()
        {
            List<Modelo> modelos = service.Listar().ToList();
            List<ModeloViewModel> listDest = Mapper.Map<List<Modelo>, List<ModeloViewModel>>(modelos);
            return View(listDest);
        }

        // GET: Modelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Modelo modelo = service.Buscar(id);
                ModeloViewModel modeloVM = Mapper.Map<Modelo, ModeloViewModel>(modelo);
                return View(modeloVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
           
        }

        // GET: Modelo/Create
        public ActionResult Create()
        {
            ModeloViewModel modeloVM = new ModeloViewModel();

            modeloVM.ListaFabricantes = new SelectList(service.ListarFabricantes(), "FabricanteID", null, modeloVM.Fabricante);
            return View(modeloVM);
        }

        // POST: Modelo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloViewModel modeloVM)
        {
            if (ModelState.IsValid)
            {
                Modelo modelo = Mapper.Map<ModeloViewModel, Modelo>(modeloVM);
                service.Adicionar(modelo);
                return RedirectToAction("Index");
            }
            //deu ruim
            modeloVM.ListaFabricantes = new SelectList(service.ListarFabricantes(), "FabricanteID", null);
            return View(modeloVM);
        }

        // GET: Modelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Modelo modelo = service.Buscar(id);
                ModeloViewModel modeloVM = Mapper.Map<Modelo, ModeloViewModel>(modelo);
                modeloVM.ListaFabricantes = new SelectList(service.ListarFabricantes(), "FabricanteID", "Nome", modeloVM.ModeloID);
                return View(modeloVM);
            }
            catch (BusinessException)
            {
                return HttpNotFound();
            }
          
           
        }

        // POST: Modelo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModeloViewModel modeloVM)
        {
            if (ModelState.IsValid)
            {
                Modelo modelo = Mapper.Map<ModeloViewModel, Modelo>(modeloVM);
                service.Editar(modelo);
                return RedirectToAction("Index");
            }
            modeloVM.ListaFabricantes = new SelectList(service.ListarFabricantes(), "FabricanteID", "Nome", modeloVM.ModeloID);
            return View(modeloVM);
        }

        // GET: Modelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Modelo modelo = service.Buscar(id);
                ModeloViewModel modeloVM = Mapper.Map<Modelo, ModeloViewModel>(modelo);
                return View(modeloVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // POST: Modelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                service.Remover(id);
                return RedirectToAction("Index");
            }
            catch (BusinessException)
            {
                return HttpNotFound();
            }
            
        }
    }
}