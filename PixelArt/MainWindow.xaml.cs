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
        public Brush pincelActual = Brushes.Black;
        public MainWindow()
        {
            InitializeComponent();
            radioNegro.IsChecked = true;
            
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
                borde.Background = pincelActual;
            }
        }

        private void pintar(object sender, MouseButtonEventArgs e)
        {
            Border borde = sender as Border;
            borde.Background = pincelActual;
        }

        private void rellenar(object sender, RoutedEventArgs e)
        {
            foreach (Border b in lienzo.Children)
            {
                b.Background = pincelActual;
            }
        }

        private void cambiaPincel(object sender, RoutedEventArgs e)
        {

            //#{0-9a-f}6 creo
            RadioButton botonPulsado = sender as RadioButton;
            if (!botonPulsado.Name.Equals("radioPersonalizado"))
            {
                personalizado.IsEnabled = false;
                personalizado.Text = "";
                pincelActual = (SolidColorBrush)new BrushConverter().ConvertFromString(botonPulsado.Tag.ToString());

            }
                
            else
            {
                personalizado.IsEnabled = true;
            }
            
        }

        private void introduceColor(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {
                    pincelActual = (SolidColorBrush)new BrushConverter().ConvertFromString("#" + personalizado.Text);
                }
                catch { }
            }
            
        }
    }

}
