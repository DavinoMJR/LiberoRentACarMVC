using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using LiberoRentACar.Model;
using LiberoRentACarASPMVC.ViewModels;
using AutoMapper;
using LiberoRentACar.Model.Services;
using LiberoRentACarModel;

namespace LiberoRentACarASPMVC.Controllers
{
    [Authorize(Roles = "Admin,Funcionario")]
    public class ClienteController : Controller
    {

        private IClienteService service;
        private IUsuarioService identityService;
        public ClienteController(IClienteService service, IUsuarioService idService)
        {
            this.service = service;
            this.identityService = idService;
        }

        // GET: Cliente
        public ActionResult Index()
        {
            IEnumerable<Cliente> clientes = service.Listar();               
            IEnumerable<ClienteViewModel> clientesVM = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(clientes);
            return View(clientesVM);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Cliente cliente = service.Buscar(id);
                ClienteViewModel clienteVM = Mapper.Map<Cliente, ClienteViewModel>(cliente);
                return View(clienteVM);
            }
            catch (BusinessException)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        // GET: Cliente/Create
        public ActionResult Create()
        {
            ClienteViewModel clienteVM = new ClienteViewModel();
            clienteVM.PessoaID = User.Identity.Name;
            return View(clienteVM);
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Cliente cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteVM);
                    identityService.CadastrarUsuario(cliente,User.Identity.Name);
                }
                catch (BusinessException ex)
                {
                    HttpNotFound(ex.Message);
                }
            }

            clienteVM.PessoaID = User.Identity.Name;
            return View(clienteVM);
        }

       
        // GET: Cliente/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Cliente cliente = service.Buscar(id);
                ClienteViewModel clienteVM = Mapper.Map<Cliente, ClienteViewModel>(cliente);
                return View(clienteVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Cliente cliente = Mapper.Map<ClienteViewModel, Cliente>(clienteVM);
                    service.Editar(cliente);
                    return RedirectToAction("Index");
                }
                catch (BusinessException)
                {
                    HttpNotFound();
                }
            }
          
            return View(clienteVM);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Cliente cliente = service.Buscar(id);
                ClienteViewModel clienteVM = Mapper.Map<Cliente, ClienteViewModel>(cliente);
                return View(clienteVM);
            }
            catch (BusinessException ex)
            {
                return HttpNotFound(ex.Message);
            }            
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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
