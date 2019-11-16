using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Connections;
using Server.Dtos;
using Server.Models;
using Server.Repository;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyllabusController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly DatabaseContext _context;

        public SyllabusController(IAuthRepository repo, IConfiguration config, DatabaseContext context)
        {
            _repo = repo;
            _config = config;
            _context = context;
        }

        // GET api/Syllabus
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_context.Syllabuses.ToList());
        }

        // GET api/Syllabus/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_context.Syllabuses.Find(id));
        }

        // POST api/Syllabus
        [HttpPost]
        public ActionResult Post([FromBody] Syllabus model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            model.Status = "active";
            _context.Syllabuses.Add(model);
            _context.SaveChangesAsync();

            return Ok(model);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]  Syllabus model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return Ok(model);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                var allowedExtensions = new[] {"pdf", "doc", "docx"};
                var fileExtension = file.FileName.Split('.').LastOrDefault();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("This types of file not allowed");
                }

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest("Upload error");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [Route("trades")]
        public async Task<IActionResult> GetAllTrades()
        {
            return Ok(_context.Trades.ToList());
        }

        [Route("levels/{id}")]
        public async Task<IActionResult> GetLevelsByTradeId(int id)
        {
            return Ok(_context.Levels.Where(l => l.TradeId == id).ToList());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
