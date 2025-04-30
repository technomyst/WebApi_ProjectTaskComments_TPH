using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_ProjectTaskComments.Models;
using WebApi_ProjectTaskComments.Models.Dto.Comments;

namespace WebApi_ProjectTaskComments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommentsController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Comments
        [HttpGet("Project/{projectId}/GetProjectCommentsFromProjectCommentsClass")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectCommentsFromProjectCommentsClass(int projectId)
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            return await _context.ProjectComments.Where(x=>x.ProjectId== projectId).ToListAsync();
            //return await _context.Comments.Where(x => x.SourceId == projectId&&x.SourceType==Models.Enums.CommentSourceTypeEnum.Project).ToListAsync();
        }

        // GET: api/Comments
        [HttpGet("Project/{projectId}/GetProjectCommentsFromCommentsClass")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectCommentsFromCommentsClass(int projectId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.Where(x => x.SourceId == projectId && x.SourceType == Models.Enums.CommentSourceTypeEnum.Project).ToListAsync();
        }

        [HttpGet("Project/{projectId}/GetProjectCommentsFromTypeOf")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectCommentsFromTypeOf(int projectId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.OfType<ProjectComment>().Where(x => x.SourceId == projectId && x.SourceType == Models.Enums.CommentSourceTypeEnum.Project).ToListAsync();
            //Возможно не оптимизированно. получим ВСЕ ProjectComment
            //Надо посмотреть вариант
            //
            //return await _context.Comments.Where(x=>x.Id== projectId).OfType<ProjectComment>().ToListAsync();
            //Хоть и выглядит не очень логично (наглядно)
        }


        // GET: api/Comments
        [HttpGet("ProjectTask/{projectTaskId}/GetProjectTaskCommentsFromProjectTaskCommentClass")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectTaskCommentsFromProjectTaskCommentClass(int projectTaskId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.ProjectTaskComments.Where(x => x.ProjectTaskId == projectTaskId).ToListAsync();

        }

        // GET: api/Comments
        [HttpGet("ProjectTask/{projectTaskId}/GetProjectTaskCommentsFromCommentClass")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectTaskCommentsFromCommentClass(int projectTaskId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.Where(x => x.SourceId == projectTaskId && x.SourceType == Models.Enums.CommentSourceTypeEnum.ProjectTask).ToListAsync();

            //Context.Person.OfType<Person>()

        }

        [HttpGet("ProjectTask/{projectTaskId}/GetProjectTaskCommentsFromTypeOf")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetProjectTaskCommentsFromTypeOf(int projectTaskId)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.OfType<ProjectTaskComment>().Where(x => x.ProjectTaskId == projectTaskId).ToListAsync();
            //Context.Person.OfType<Person>()

        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
          if (_context.Comments == null)
          {
              return NotFound();
          }
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentDto commentDto)
        {
          if (_context.Comments == null)
          {
              return Problem("Entity set 'AppDbContext.Comments'  is null.");
          }

            var comment=_mapper.Map<Comment>(commentDto);
            commentDto.CreatedDate = DateTime.UtcNow;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = commentDto.Id }, commentDto);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            //_context.Comments.Remove(comment);
            comment.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
