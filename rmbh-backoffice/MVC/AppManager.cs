﻿using rmbh_backoffice.MVC.Controllers;
using rmbh_backoffice.MVC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rmbh_backoffice.MVC
{
    public class AppManager
    {
        private static bool _started;

        private static AppManager _instance;

        public static AppManager Instance
        {
            get
            {
                if (!_started)
                    throw new Exception("Tried to call the singleton instance of the AppManager before the AppManager started.");

                return _instance;
            }
        }

        /// <summary>
        /// The current WinForms-Form instance
        /// </summary>
        private IView _currentView;

        /// <summary>
        /// The controller factory for creating controllers
        /// </summary>
        private readonly ControllerFactory _controllerFactory;

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private AppManager(ControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private AppManager() { }

        /// <summary>
        /// Starts the AppManager and creates a singleton for this class
        /// </summary>
        public static void Start<T>(ControllerFactory controllerFactory)
            where T : BaseController
        {
            if (_started) return;

            _started = true;

            // Create Controller without ControllerFactory
            //T controller = Activator.CreateInstance<T>();

            // Create Controller with ControllerFactory
            T controller = controllerFactory.CreateController<T>();

            if (controller != null)
            {
                _instance = new AppManager(controllerFactory)
                {
                    _currentView = controller.View
                };

                _instance.openForm();
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Loads a Controller, handles the loading state
        /// </summary>
        /// <typeparam name="T">Generic Type where T extends Controller</typeparam>
        public void Load<T>()
            where T : BaseController
        {
            //T controller = Activator.CreateInstance<T>();
            T controller = _controllerFactory.CreateController<T>();

            if (controller != null)
            {
                if (controller.Loadable())
                {
                    controller.OnLoadSuccess(EventArgs.Empty);
                }
                else
                {
                    controller.OnLoadFailure(EventArgs.Empty);
                }
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Shows the View of the Controller parameter  
        /// </summary>
        /// <param name="controller">The controller instance</param>
        public void Show(BaseController controller)
        {
            if (_currentView != null)
            {
                _currentView.Close();
                _currentView.Form.Dispose();
            }

            _currentView = controller.View;

            Thread th = new Thread(openForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        /// <summary>
        /// Used as single-threaded appartment thread-callback to keep the main thread running
        /// </summary>
        private void openForm()
        {
            Application.Run(_currentView.Form);
        }
    }
}
