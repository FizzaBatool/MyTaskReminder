using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo_list.Data;
using ToDo_list.Models;

namespace ToDo_list.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDo_listContext context;
        public ToDoController(ToDo_listContext context) 
        {
            this.context = context; 
            
        }
        //GET/
        public async Task<ActionResult> Index()
        {
            IQueryable<ToDoList> items = from i in context.ToDoLists orderby i.Id select i;
            List<ToDoList> todolist = await items.ToListAsync();
            return View(todolist);
        }
        //GET/todo/create
        public IActionResult Create() => View();
        //POST /Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "Item added succesfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET/todo/Update
        public async Task<ActionResult> Edit(int id)
        {
         ToDoList item = await context.ToDoLists.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        //POST /Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ToDoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "Item updated succesfully";
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //GET/todo/Delete
        public async Task<ActionResult> Delete(int id)
        {
            ToDoList item = await context.ToDoLists.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "Item does not existed";
            }
            else
            {
                context.ToDoLists.Remove(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "Item deleted successfully";
            }
            return RedirectToAction("Index");
        }

    }
}
