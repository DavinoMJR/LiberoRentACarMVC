using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LiberoRentACar.Model;
using LiberoRentACarASPMVC.ViewModels;
using AutoMapper;
using LiberoRentACar.Model.Services;
using LiberoRentACarModel;
using System.Text;

namespace LiberoRentACarASPMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarroController : Controller
    {
        private readonly ICarroService service;

        public CarroController(ICarroService service)
        {
            this.service = service;
        }

        // GET: Carro
        public ActionResult Index()
       {
            List<Carro> carros = service.Listar().ToList();
            List<CarroViewModel> listDest = Mapper.Map<List<Carro>, List<CarroViewModel>>(carros);        
            return View(listDest);
        }

        // GET: Carro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Carro carro = service.Buscar(id);
                CarroViewModel carroVM = Mapper.Map<Carro, CarroViewModel>(carro);
                return View(carroVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
           
        }

        // GET: Carro/Create
        public ActionResult Create()
        {
            CarroViewModel carroVM = new CarroViewModel();
            carroVM.ListaModelos = new SelectList(service.ListarModelosCarro(), "ModeloID", null, carroVM.ModeloCarro);
            return View(carroVM);
        }

        // POST: Carro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarroViewModel carroVM)
        {
            if (ModelState.IsValid)
            {
                Carro carro = Mapper.Map<CarroViewModel, Carro>(carroVM);
                service.Adicionar(carro);
                return RedirectToAction("Index");
            }
            //deu ruim
            carroVM.ListaModelos = new SelectList(service.ListarModelosCarro(), "ModeloID", null);
            return View(carroVM);
        }

        // GET: Carro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Carro carro = service.Buscar(id);
                CarroViewModel carroVM = Mapper.Map<Carro, CarroViewModel>(carro);
                carroVM.ListaModelos = new SelectList(service.ListarModelosCarro(), "ModeloID", "Nome", carroVM.ModeloID);
                return View(carroVM);
            }
            catch (BusinessException)
            {
                return HttpNotFound();
            }
          
           
        }

        // POST: Carro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarroViewModel carroVM)
        {
            if (ModelState.IsValid)
            {
                Carro carro = Mapper.Map<CarroViewModel, Carro>(carroVM);
                service.Editar(carro);
                return RedirectToAction("Index");
            }
            carroVM.ListaModelos = new SelectList(service.ListarModelosCarro(), "ModeloID", "Nome", carroVM.ModeloID);
            //ViewBag.ModeloID = new SelectList(db.Modelos, "ModeloID", "Nome", carro.ModeloID);
            return View(carroVM);
        }

        // GET: Carro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Carro carro = service.Buscar(id);
                CarroViewModel carroVM = Mapper.Map<Carro, CarroViewModel>(carro);
                return View(carroVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // POST: Carro/Delete/5
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
