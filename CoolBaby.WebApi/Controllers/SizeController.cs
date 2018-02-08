using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace CoolBaby.WebApi.Controllers
{
    public class SizeController : ApiController
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_sizeService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] SizeViewModel sizeViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _sizeService.Add(sizeViewModel);
            _sizeService.Save();
            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Update([FromBody] SizeViewModel sizeViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _sizeService.Update(sizeViewModel);
            _sizeService.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _sizeService.Delete(id);
            _sizeService.Save();
            return new NoContentResult();
        }
    }
}