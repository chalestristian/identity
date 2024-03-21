using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Api.Controller.Base;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiControllerBase : ControllerBase { }