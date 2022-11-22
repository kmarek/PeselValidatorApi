using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TodoController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly TodoContext _context;

        /// <summary>
        /// Controller responsible for managing ToDo items
        /// </summary>
        /// <param name="context"></param>
        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1abc" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/ToDo
        ///     
        /// </remarks>
        /// <returns>Deleted item</returns>
        /// <response code="200">Returns all items</response>    
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItem()
        {
            System.Diagnostics.Trace.TraceError("GET ALL");
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// Get item by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/ToDo/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Get item</returns>
        /// <response code="200">Return item</response>    
        /// <response code="404">If item does not exist.</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        /// <summary>
        /// Update item by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/ToDo/1
        ///     {  
        ///       "id": 1,
        ///       "name":"some name",
        ///       "isComplete":false
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <response code="200">Return item</response>  
        /// <response code="400">If Id in URL does not match Id in body.</response>    
        /// <response code="404">If item does not exist.</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/ToDo
        ///     {        
        ///       "Name":"some name",
        ///       "IsComplete":false
        ///     }
        /// </remarks>
        /// <param name="todoItem"></param>
        /// <returns>Added item</returns>
        /// <response code="201">Returns added item</response>
        /// <response code="400">If body is incorrect</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        /// <summary>
        /// Delete an item by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/ToDo/1
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Deleted item</returns>
        /// <response code="200">Returns deleted item</response>    
        /// <response code="403">If the item has id = 1</response> 
        /// <response code="404">If the item does not exist</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            if (id == 1)
                return StatusCode(403);

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
