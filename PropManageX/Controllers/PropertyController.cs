using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.PropertyDTOs.Property;
using PropManageX.Services.PropertyListingInventoryManagement.PropertyService.PropertyService;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // Anyone who is logged in can view all properties
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertyService.GetAllProperties();
            return Ok(properties);
        }

        // Anyone logged in can view property by id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _propertyService.GetPropertyById(id);

            if (property == null)
                return NotFound("Property not found");

            return Ok(property);
        }


        //Anyone logged in can search properties by name, location or type

        
        [HttpGet("search")]
        public async Task<IActionResult> SearchProperties([FromQuery] string? q, [FromQuery] string? location, [FromQuery] string? type)
        {
            var properties = await _propertyService.SearchProperties(q, location, type);
            return Ok(properties);
        }


        // Only Admin can create property
        [HttpPost("create-property")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto dto)
        {
            var property = await _propertyService.CreateProperty(dto);
            return Ok(property);
        }

        // Only Admin can update property
        [Authorize(Roles = " Admin , Agent ")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, UpdatePropertyDto dto)
        {
            var property = await _propertyService.UpdateProperty(id, dto);

            if (property == null)
                return NotFound("Property not found");

            return Ok(property);
        }

        // Only Admin can delete property
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var result = await _propertyService.DeleteProperty(id);

            if (!result)
                return NotFound("Property not found");

            return Ok("Property deleted successfully");
        }
    }
}
