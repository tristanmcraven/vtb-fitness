﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vtb_fitness_client.UserControls
{
    /// <summary>
    /// Interaction logic for TariffPriceUserControl.xaml
    /// </summary>
    public partial class TariffPriceUserControl : UserControl
    {
        private double? _price;
        public TariffPriceUserControl(double? price)
        {
            InitializeComponent();
            _price = price;
            InitView();
        }

        private void InitView()
        {
            price_TextBlock.Text = _price.ToString() + "₽";
        }
    }
}
