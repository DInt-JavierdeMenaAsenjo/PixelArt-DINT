using System;
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

namespace PixelArt
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newLienzo(object sender, RoutedEventArgs e)
        {
            Thickness borde = new Thickness(0.5);
            Button boton = (sender as Button);
            int size = int.Parse(boton.Tag.ToString());
            lienzo.Children.Clear();
            for (int j = 0; j < size; j++)

                for (int i = 0; i < size; i++)
                {
                    Border pixel = new Border();
                    pixel.MouseLeftButtonDown += pintar;
                    pixel.MouseEnter += Pixel_MouseEnter; ;
                    pixel.BorderThickness = borde;
                    pixel.Background = Brushes.White;
                    pixel.BorderBrush = Brushes.Black;

                    lienzo.Children.Add(pixel);
                }
            
        }

        private void Pixel_MouseEnter(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Border borde = sender as Border;
                borde.Background = Brushes.Black;
            }
        }

        private void pintar(object sender, MouseButtonEventArgs e)
        {
            Border borde = sender as Border;
            borde.Background = Brushes.Black;
        }

    }

}
