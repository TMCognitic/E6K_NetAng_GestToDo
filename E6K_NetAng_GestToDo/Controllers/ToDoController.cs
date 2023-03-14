using E6K_NetAng_GestToDo.Models.Entities;
using E6K_NetAng_GestToDo.Models.Forms;
using E6K_NetAng_GestToDo.Models.Repositoryies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E6K_NetAng_GestToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        // GET: ToDoController
        public ActionResult Index()
        {
            return View(_toDoRepository.Get());
        }

        // GET: ToDoController/Details/5
        public ActionResult Details(int id)
        {
            ToDo? todo = _toDoRepository.Get(id);
            if (todo is null)
                return RedirectToAction("Index");

            return View(todo);
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateToDoForm form)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);


                _toDoRepository.Insert(new ToDo() { Title = form.Title });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(form);
            }
        }

        // GET: ToDoController/Edit/5
        public ActionResult Edit(int id)
        {
            ToDo? todo = _toDoRepository.Get(id);
            if (todo is null)
                return RedirectToAction("Index");

            return View(new EditToDoForm() { Id = todo.Id, Title = todo.Title });
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditToDoForm form)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(form);

                if(id != form.Id)
                    return RedirectToAction("Index");


                _toDoRepository.Update(new ToDo() { Id = form.Id, Title = form.Title });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(form);
            }
        }

        // GET: ToDoController/Delete/5
        public ActionResult Delete(int id)
        {
            ToDo? todo = _toDoRepository.Get(id);
            if (todo is null)
                return RedirectToAction("Index");

            return View(todo);
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _toDoRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
