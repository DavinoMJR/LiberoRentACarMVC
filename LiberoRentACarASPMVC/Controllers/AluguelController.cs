using System;
using System.Net;
using System.Web.Mvc;
using LiberoRentACar.Model;
using AutoMapper;
using LiberoRentACarASPMVC.ViewModels;
using LiberoRentACar.Model.Services;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using LiberoRentACarModel;

namespace LiberoRentACarASPMVC.Controllers
{
    public class AluguelController : Controller
    {
        private readonly IAluguelService service;

        public AluguelController(IAluguelService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Index()
        {
            List<Aluguel> alugueis = service.Listar().ToList();
            List<ReservaAluguelViewModel> listDest = Mapper.Map<List<Aluguel>, List<ReservaAluguelViewModel>>(alugueis);

            return View(listDest);
        }

       [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Aluguel aluguel = service.Buscar(id);
                ReservaAluguelViewModel aluguelVM = Mapper.Map<Aluguel, ReservaAluguelViewModel>(aluguel);
                return View(aluguelVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        [ChildActionOnly]
        public ActionResult DatasRetiradaEDevolucao()
        {
            var model = new ReservaAluguelViewModel();
            IEnumerable<TimeSpan> tempos = Util.Tempos();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var t in tempos)
            {
                lista.Add(new SelectListItem { Value = t.Hours.ToString(), Text = String.Format("{0}:{1:D2}", (int)t.TotalHours, t.Minutes, t.Seconds) });
            }

            model.SelecaoHoraAluguel = lista;
            model.SelecaoHoraDevolucao = lista;
            return PartialView("_ReservaAluguel",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReservaData(ReservaAluguelViewModel aluguel)
        {
            aluguel.DataAluguel = aluguel.DataAluguel.AddHours(aluguel.SelectedHoraAluguel);
            aluguel.DataDevolucao = aluguel.DataDevolucao.AddHours(aluguel.SelectedHoraDevolucao);
            return RedirectToAction("EscolhaOCarro",aluguel);
        }

        public ActionResult EscolhaOCarro(ReservaAluguelViewModel aluguelVM)
        {
            IEnumerable<Carro> carros = service.ListarCarrosDisponiveis();
            IEnumerable<CarroViewModel> carrosVM = Mapper.Map<IEnumerable<Carro>, IEnumerable<CarroViewModel>>(carros);
            aluguelVM.CarrosDisponiveis = carrosVM;
            return View(aluguelVM);
        }


        public ActionResult Devolucao(int? id)
        {
            if (id != null)
            {
                try
                {
                    service.DevolverCarro(id);
                    return RedirectToAction("Index");
                }
                catch (BusinessException ex)
                {
                    return HttpNotFound(ex.Message);
                }
            }
            else
            {
                return HttpNotFound();
            }
            

        }

        [Authorize]
        public ActionResult Reservar()
        {          
            DateTime dataAluguel = DateTime.ParseExact(DateTime.UtcNow.ToString(Request.QueryString["dataAluguel"].ToString(), CultureInfo.InvariantCulture)
                 ,"MM/dd/yyyy hh:mm:ss", null);
            DateTime dataDevolucao = DateTime.ParseExact(DateTime.UtcNow.ToString(Request.QueryString["dataDevolucao"].ToString(), CultureInfo.InvariantCulture)
                 ,"MM/dd/yyyy hh:mm:ss", null);
            int carroID = int.Parse(Request.QueryString["CarroID"].ToString());
            string userID = User.Identity.Name;
                
            try
            {
                service.Reservar( dataAluguel, dataDevolucao,carroID, userID);
                return RedirectToAction("Index", "Home");
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message); //TODO: TELA CUSTOMIZACADA PARA ERRO
            }
        }

        [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Create()
        {
            ReservaAluguelViewModel aluguelVM = new ReservaAluguelViewModel();
            IEnumerable<TimeSpan> tempos = Util.Tempos();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var t in tempos)
            {
                lista.Add(new SelectListItem { Value = t.Hours.ToString(), Text = String.Format("{0}:{1:D2}", (int)t.TotalHours, t.Minutes, t.Seconds) });
            }

            aluguelVM.SelecaoHoraAluguel = lista;
            aluguelVM.SelecaoHoraDevolucao = lista;
            aluguelVM.SelecaoCarros = new SelectList(service.ListarCarrosDisponiveis(), "CarroID", null, aluguelVM.Carro);
            return View(aluguelVM);
        }

        [Authorize(Roles = "Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaAluguelViewModel aluguelVM)
        {
            if (ModelState.IsValid)
            {
                aluguelVM.DataAluguel = aluguelVM.DataAluguel.AddHours(aluguelVM.SelectedHoraAluguel);
                aluguelVM.DataDevolucao = aluguelVM.DataDevolucao.AddHours(aluguelVM.SelectedHoraDevolucao);
                Aluguel aluguel = Mapper.Map<ReservaAluguelViewModel, Aluguel>(aluguelVM);
              
                service.Adicionar(aluguel);
                return RedirectToAction("Index");
            }
            aluguelVM.SelecaoCarros = new SelectList(service.ListarCarrosDisponiveis());
            return View(aluguelVM);
        }

       [Authorize(Roles = "Admin,Funcionario")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Aluguel aluguel = service.Buscar(id);
                ReservaAluguelViewModel aluguelVM = new ReservaAluguelViewModel()
                {
                    SelecaoCarros = new SelectList(service.ListarCarrosDisponiveis())
                };
              
                return View(aluguelVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Funcionario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservaAluguelViewModel aluguelVM)
        {
            if (ModelState.IsValid)
            {
                Aluguel aluguel = Mapper.Map<ReservaAluguelViewModel, Aluguel>(aluguelVM);
                service.Editar(aluguel);       
                return RedirectToAction("Index");
            }

            aluguelVM.SelecaoCarros = new SelectList(service.ListarCarrosDisponiveis());
            return View(aluguelVM);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Aluguel aluguel = service.Buscar(id);
                ReservaAluguelViewModel aluguelVM = Mapper.Map<Aluguel, ReservaAluguelViewModel>(aluguel);
                return View(aluguelVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
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
