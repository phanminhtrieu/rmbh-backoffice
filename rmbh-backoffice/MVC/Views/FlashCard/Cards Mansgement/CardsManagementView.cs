﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rmbh_backoffice.MVC.Views;

namespace rmbh_backoffice.VC.Views
{
    public partial class CardsManagementIView : Form, IView
    {
        public CardsManagementIView()
        {
            InitializeComponent();
        }
        public Form Form
        {
            get
            {
                return this;
            }
        }

        public string Title
        {
            get
            {
                return Text;
            }

            set
            {
                Text = value;
            }
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}