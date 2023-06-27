using EventApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EventApp.DTOs.TicketDto;

namespace EventApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index()
        {
            var ticket = await _ticketService.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketRequestModel model)
        {
            var ticket = await _ticketService.CreateTicket(model);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            if(ticket == null)
            {
                NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTicketRequestModel model)
        {
            var ticket = await _ticketService.UpdateTicket(id, model);
            return RedirectToAction("View");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketById(id);
            if(ticket == null)
            {
                NotFound();
            }
            return View(ticket.Data);
        }

        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var ticket = await _ticketService.DeleteTicket(id);
            return RedirectToAction("Index");
        }

    }
}
