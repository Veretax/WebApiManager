﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.Library.Api;
using WebApiManager.WPF.UI.Library.Models;

namespace WebApiManager.WPF.UI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;            
        }

        protected override async void OnViewLoaded(object view)
        {
            await LoadProducts();
        }
        
        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<ProductModel> _cart;

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart); 
            }
        }

        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            get 
            {
                // TODO - Replace with Calculation

                return "$0.00";
            } 
        }

        public string Total
        {
            get
            {
                // TODO - Replace with Calculation

                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                // TODO - Replace with Calculation

                return "$0.00";
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected
                // Make sure there is an item quantity
                //if (ItemQuantity?.Length > 0)
                //{
                //    output = true;
                //}

                return output;
            }
         
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected
                //if (ItemQuantity?.Length > 0)
                //{
                //    output = true;
                //}

                return output;
            }

        }

        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                // Make sure something is in the Cart
                //if (ItemQuantity?.Length > 0)
                //{
                //    output = true;
                //}

                return output;
            }

        }

        public void CheckOut()
        {

        }


    }
}
