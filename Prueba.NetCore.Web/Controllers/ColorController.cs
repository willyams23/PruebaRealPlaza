﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.NetCore.Web.Controllers
{
    public class ColorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
