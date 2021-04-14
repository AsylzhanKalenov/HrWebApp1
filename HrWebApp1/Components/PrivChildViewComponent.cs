using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrWebApp1.Components
{
    public class PrivChildViewComponent : ViewComponent
    {
        public PrivChildViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View("ChildForm");
        }
    }
}
