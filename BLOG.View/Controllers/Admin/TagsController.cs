namespace BLOG.View.Controllers.Admin
{
    using BLOG.Dtos.Tag;
    using BLOG.Services.Interfaces;

    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
    public class TagsController : BaseController<TagDto, CreateOrUpdateTagDto, ITagsService>
    {
        private readonly ITagsService _service;

        public TagsController(ITagsService service) : base(service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("pagination/{pageNumber}")]
        public async Task<IActionResult> ListWithPagination(int pageNumber)
        {
            Result<AdminPaginationTagsDto> res = await _service.ListWithPagination(pageNumber);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }
    }
}