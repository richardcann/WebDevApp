using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Housing.WebAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Housing.WebAPI.Helpers;
using Housing.WebAPI.Models.ClientServerDTO;
using Housing.WebAPI.Models.InternalDTO;

namespace Housing.WebAPI.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly HousingContext _context;

        public ImagesController(HousingContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public IEnumerable<Image> GetImage()
        {
            return _context.Image;
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.Image.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }



        // PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage([FromRoute] int id, [FromBody] Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != image.ID)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        public async Task<bool> AddImageAsync(Image image)
        {
            _context.Image.Add(image);
            await _context.SaveChangesAsync();
            return true;
        }

        // POST: api/Image
        [HttpPost]
        [Route("add")]
        [DisableFormValueModelBinding]
        [AllowAnonymous]
        public async Task<IActionResult> PostImage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FormValueProvider formModel;
           
                //string ext = Path.GetExtension()
            //formModel = await Request.StreamFile();
            var junk = new Junk();

            //var bindingSuccessful = await TryUpdateModelAsync(junk, prefix: "",
            //        valueProvider: formModel);
            

            return Ok(junk);

            //_context.Image.Add(image);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetImage", new { id = image.ID }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();

            return Ok(image);
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.ID == id);
        }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var formValueProviderFactory = context.ValueProviderFactories
                .OfType<FormValueProviderFactory>()
                .FirstOrDefault();
            if (formValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(formValueProviderFactory);
            }

            var jqueryFormValueProviderFactory = context.ValueProviderFactories
                .OfType<JQueryFormValueProviderFactory>()
                .FirstOrDefault();
            if (jqueryFormValueProviderFactory != null)
            {
                context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }


}