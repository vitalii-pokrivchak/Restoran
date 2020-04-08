﻿using Microsoft.EntityFrameworkCore;
using RestoranClient.Data;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoranClient
{
    /// <summary>
    /// Interaction logic for OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : UserControl
    {
        public OrderEdit()
        {
            InitializeComponent();
        }

        public event Action<Order> OnSaveOrder;
        public event Action OnCancelOrder;
        public Order Order { get; set; }


        public void ShowOrderEdit(int?  id = null)
        {
           
             
            using (var context = new RestoranDbContext())
            {
                

                var directionSQL = context.Order.FromSqlRaw("SELECT abonent_id FROM dbo.[order]").ToList();
                if (id == null)
                {
                    Order = new Order
                    {
                        AbonentId = Config.Abonents[0]?.Id,
                        WaiterId = Config.WaiterId,
                        SourceId = Config.SourceItems[0]?.Id,
                        TimeOrder = DateTime.Now,
                        Details = new List<Detail>()
                    };
                }
                else
                {
                    Order = context.Order.Include("Details").SingleOrDefault(pk => pk.Id == id);
                }
                var s = new StringBuilder();
                foreach (var item in context.Order)
                {
                    s.AppendLine("new Order{" + $"Id = {item.Id} ,AbonentId = {item.AbonentId},WaiterId = {item.WaiterId},FixedSource = {item.FixedSource},TimeOrder = {item.TimeOrder},Bill = {item.Bill}" + "};\n");
                }
                File.WriteAllText($"{Environment.CurrentDirectory}/sql.txt", s.ToString());
            }
            cbAbonent.ItemsSource = Config.Abonents;
            cbAbonent.SelectedValue = Order.AbonentId;

            cbSource.ItemsSource = Config.SourceItems;
 

            cbSource.SelectedValue = Order.SourceId;
            
            dpStart.Text = Order.TimeOrder.ToString("H.mm.ss");
            dpEnd.Text = Order.EndOrder?.ToString("H.mm.ss") ?? "Активний";

            //grid
            cbitem.ItemsSource = Config.FoodItems;

            dgOrder.ItemsSource = new ObservableCollection<Detail>(Order.Details);
            Visibility = Visibility.Visible;
        }

        private void SaveOrder(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Order.AbonentId = (int)cbAbonent.SelectedValue;
            Order.SourceId = (int)cbSource.SelectedValue;
            Order.FixedSource = (FixSource)(Order.SourceId - 1);
            var cl = dgOrder.ItemsSource as ObservableCollection<Detail>;
            decimal bill = 0;
            foreach (var d in cl)
            {
                d.Price = Config.FoodItems.Single(k => k.Id == d.ItemsId).Price;
                d.Bill = d.Price * d.Count;
                bill = bill + d.Bill;
            }
            Order.Bill = bill;
            Order.Details = cl.ToList();
            OnSaveOrder?.Invoke(Order);

        }

        private void CancelOrder(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            OnCancelOrder?.Invoke();
        }


    }
}
