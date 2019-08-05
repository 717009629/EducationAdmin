using Microsoft.AspNetCore.Antiforgery;
using EducationAdmin.Controllers;

namespace EducationAdmin.Web.Host.Controllers
{
    public class AntiForgeryController : EducationAdminControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
