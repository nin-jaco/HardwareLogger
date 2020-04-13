using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public HardwareController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/hardware
        [HttpGet]
        public async Task<ActionResult<List<Hardware>>> Get()
        {
            return await _dbContext.Hardware.ToListAsync();
        }

        // GET api/hardware/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hardware>> Get(string id)
        {
            return await _dbContext.Hardware.FindAsync(id);
        }

        // POST api/hardware
        [HttpPost]
        public async Task Post(Hardware model)
        {
            await _dbContext.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        // PUT api/hardware/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Hardware model)
        {
            var exists = await _dbContext.Hardware.AnyAsync(f => f.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            _dbContext.Hardware.Update(model);

            await _dbContext.SaveChangesAsync();

            return Ok();

        }

        // DELETE api/hardware/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var entity = await _dbContext.Hardware.FindAsync(id);

            _dbContext.Hardware.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}