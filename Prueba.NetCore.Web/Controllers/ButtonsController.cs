﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.NetCore.Web.Controllers
{
    public class ButtonsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
