namespace BLOG.View.Controllers
{
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController<T, T1, T2> : ControllerBase where T2 : IBaseService<T, T1>
    {
        private readonly T2 _service;

        public BaseController(T2 service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T1 createDto)
        {
            Result<T> res = await _service.Create(createDto);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IActionResult> Put(T1 updateDto, int id)
        {
            Result<T> res = await _service.Update(updateDto, id);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            Result<List<T>> res = await _service.List();
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            Result<T> res = await _service.Get(id);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }
    }
}