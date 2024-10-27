﻿using rmbh_backoffice.MVC.Views;
using rmbh_backoffice.VC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rmbh_backoffice.MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        private IView _view;

        public override IView View
        {
            get
            {
                return _view ?? new HomeView();
            }
        }

        public override bool Loadable()
        {
            return true;
        }
    }
}