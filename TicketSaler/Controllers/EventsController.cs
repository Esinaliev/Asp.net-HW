using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketSaler.Models;

namespace TicketSaler.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDBContext _context;

        public EventsController(AppDBContext context)
        {
            _context = context;
        }
        

        // GET: Events/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.EventsId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }
        [Authorize(Roles = "admin,manager")]
        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin,manager")]
        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventsId,TicketPrice,Name,Description,EventTime,EventAdress,AgeRating,MaxCapacity,SoldPlace")] Events events)
        {
            
            {
                events.EventsId = Guid.NewGuid();
                _context.Add(events);
                await _context.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            return View(events);
        }
        [Authorize(Roles = "admin,manager")]
        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }
         [Authorize(Roles = "admin, manager")]
        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EventsId,TicketPrice,Name,Description,EventTime,EventAdress,AgeRating,MaxCapacity,SoldPlace")] Events events)
        {
            if (id != events.EventsId)
            {
                return NotFound();
            }

           
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.EventsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Home/Index");
            }
            return View(events);
        }
        [Authorize(Roles = "admin, manager")]
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .FirstOrDefaultAsync(m => m.EventsId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }
        [Authorize(Roles = "admin, manager")]
        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'AppDBContext.Events'  is null.");
            }
            var events = await _context.Events.FindAsync(id);
            if (events != null)
            {
                _context.Events.Remove(events);
            }
            
            await _context.SaveChangesAsync();
            return Redirect("/Home/Index");
        }

        private bool EventsExists(Guid id)
        {
          return (_context.Events?.Any(e => e.EventsId == id)).GetValueOrDefault();
        }
        public IActionResult Buy(Guid id)
        {
            Events?events= _context.Events.FirstOrDefault(x => x.EventsId==id);
            User?user=_context.Users.FirstOrDefault(x =>x.UserId.ToString()==User.Identity.Name);
            if (events == null || user == null) 
            {
                return 
              Redirect("/Home/Index");
                
            }
            if (events.MaxCapacity>events.SoldPlace) 
            {
                events.SoldPlace += 1;
                UsersEvent usersEvent = new UsersEvent() 
                {
                    EventsId = id,
                    Events = events,
                    UserId = user.UserId,
                    User = user


                };
                _context.UsersEvent.Add(usersEvent);_context.Update(events); _context.SaveChanges();
            }
            return
            Redirect("/Home/Index");
        }
    }
}
