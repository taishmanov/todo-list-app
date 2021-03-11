using Company1.ToDoApp.Application.Common.Exceptions;
using Company1.ToDoApp.Application.Tasks.Commands;
using Company1.ToDoApp.Application.Tasks.Queries;
using Company1.ToDoApp.WebMvc.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company1.ToDoApp.WebMvc.Controllers
{
    public class TasksController : Controller
    {
        // TODO: get from domain
        private readonly IList<SelectListItem> AvailableStatusses = new List<SelectListItem>
        {
            new SelectListItem {Text = "Not Started", Value = "NotStarted"},
            new SelectListItem {Text = "In Progress", Value = "InProgress"},
            new SelectListItem {Text = "Completed", Value = "Completed"}
        };

        private readonly IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = await _mediator.Send(new GetTasksListQuery());

            return View(new TasksListViewModel() 
            {
                //TODO: paging info
                Items = tasks.Items.Select(it => new TaskViewModel()
                { 
                    Name = it.Name,
                    Priority = it.Priority,
                    StatusCode = it.StatusCode
                }),
                AvailableStatusses = AvailableStatusses
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TaskCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTaskId = await _mediator.Send(new CreateTaskCommand()
                    {
                        Name = model.Name,
                        Priority = model.Priority,
                        Statues = "NotStarted"
                    });

                    return RedirectToAction("Index");
                }
            }
            catch (AppException ex)
            {
                ModelState.AddModelError("app_error", ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            try
            {
                var task = await _mediator.Send(new GetTaskByIdQuery()
                {
                    Id = name
                });

                return View(new TaskUpdateModel()
                {
                    CurrentName = task.Name,
                    Name = task.Name,
                    Priority = task.Priority,
                    StatusCode = task.StatusCode,
                    AvailableStatusses = AvailableStatusses
                });
            }
            catch (NotFoundException)
            {
                return NotFound();
                // TODO: global handlers for 404, 500
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTaskId = await _mediator.Send(new UpdateTaskCommand()
                    {
                        Id = model.CurrentName,
                        Name = model.Name,
                        Priority = model.Priority,
                        Status = model.StatusCode
                    });

                    return RedirectToAction("Index");
                }
            }
            catch (NotFoundException)
            {
                return NotFound();
                // TODO: global handlers for 404, 500
            }
            catch (AppException ex)
            {
                ModelState.AddModelError("app_error", ex.Message);
            }

            model.AvailableStatusses = AvailableStatusses;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskDeleteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleted = await _mediator.Send(
                        new DeleteTaskCommand()
                        {
                            Id = model.Name
                        });
                }
            }
            catch (AppException ex)
            {
                return View("DeleteError", ex.Message); // TODO: ErrorViewModel
            }
            
            return RedirectToAction("Index");
        }

    }
}
