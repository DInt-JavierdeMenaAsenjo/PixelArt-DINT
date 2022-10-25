using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Brush pincelActual = Brushes.Black;
        private bool modificado = false;
        private string patron = @"^([0-9a-fA-F]{6})$";
        public MainWindow()
        {
            InitializeComponent();
            radioNegro.IsChecked = true;
            pintaLienzo(25);


        }
        public void pintaLienzo(int tamaño)
        {
            Thickness borde = new Thickness(0.5);
            lienzo.Children.Clear();
            for (int j = 0; j < tamaño; j++)

                for (int i = 0; i < tamaño; i++)
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
        private void newLienzo(object sender, RoutedEventArgs e)
        {
            string mensaje = "¿Seguro que quieres borrarlo todo?";
            string titulo = "Nuevo PixelArt";
            if (modificado)
            {
                if(MessageBox.Show(mensaje,titulo,MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Button boton = (sender as Button);
                    int size = int.Parse(boton.Tag.ToString());
                    pintaLienzo(size);
                }
            }else
            {
                Button boton = (sender as Button);
                int size = int.Parse(boton.Tag.ToString());
                pintaLienzo(size);
            }
            
            
        }

        private void Pixel_MouseEnter(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                modificado = true;
                Border borde = sender as Border;
                borde.Background = pincelActual;
            }
        }

        private void pintar(object sender, MouseButtonEventArgs e)
        {
            modificado = true;
            Border borde = sender as Border;
            borde.Background = pincelActual;
        }

        private void rellenar(object sender, RoutedEventArgs e)
        {
            modificado = true;
            foreach (Border b in lienzo.Children)
            {
                b.Background = pincelActual;
            }
        }

        private void cambiaPincel(object sender, RoutedEventArgs e)
        {
            
            //^#(?:[0-9a-fA-F]{6})$
            RadioButton botonPulsado = sender as RadioButton;
            if (!botonPulsado.Name.Equals("radioPersonalizado"))
            {
                personalizado.IsEnabled = false;
                personalizado.Foreground = Brushes.Gray;
                pincelActual = (SolidColorBrush)new BrushConverter().ConvertFromString(botonPulsado.Tag.ToString());

            }
                
            else
            {
                personalizado.IsEnabled = true;
            }
            
        }


        private void hexadecimalModificado(object sender, TextChangedEventArgs e)
        {
            if(personalizado.Text.Length == 6 && radioPersonalizado.IsChecked == true)
            {
                if (Regex.IsMatch(personalizado.Text, patron, RegexOptions.IgnoreCase))
                    pincelActual = (SolidColorBrush)new BrushConverter().ConvertFromString("#" + personalizado.Text);
                else 
                {
                   MessageBox.Show("Vuelva a intentarlo. Ejemplo Amarillo = #FFFF00", "Error. Color no reconocido");
                }
                
            }
        }

        private void quitarHint(object sender, RoutedEventArgs e)
        {

            personalizado.Text = "";
            personalizado.Foreground = Brushes.Black;
        }
    }

}
