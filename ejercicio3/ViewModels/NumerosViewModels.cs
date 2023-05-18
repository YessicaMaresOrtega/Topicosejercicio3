using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ejercicio3.ViewModels
{ 
    public class NumerosViewModel: INotifyPropertyChanged
    {
        public ICommand AdivinarCommand { get; set; }
        public ICommand ReiniciarCommand { get; set; }
        public int NumeroAdivinar { get; set; }
        public int Intentos { get; set; }
        public string Pista { get; set; } = "";
        private string visibilidad = "Collapsed";


        public string VisibilidadReiniciar
        {
            get { return visibilidad; }
            set
            {
                visibilidad = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibilidadReiniciar)));
            }
        }

        private string numerodado = "";
        public string NumeroDado
        {
            get { return numerodado; }
            set
            {
                numerodado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumeroDado)));
            }
        }

        public NumerosViewModel()
        {
            Iniciar();
            AdivinarCommand = new RelayCommand(Adivinar);
            ReiniciarCommand = new RelayCommand(Iniciar);
        }
        Regex test = new Regex(@"^\d+$");
        private void Adivinar()
        {
            if (Intentos >= 1)
                if (test.IsMatch(NumeroDado))
                {
                    if (int.Parse(NumeroDado) == NumeroAdivinar)
                    {
                        Pista = "Ganaste";
                        VisibilidadReiniciar = "Visible";
                    }
                    else if (int.Parse(NumeroDado) > NumeroAdivinar)
                        Pista = "El número es menor";
                    else
                        Pista = "El número es mayor";
                    Intentos--;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Intentos)));
                }
                else
                    Pista = "Solo se permiten números";
            else
            {
                Pista = "Te quedaste sin intentos";
                VisibilidadReiniciar = "Visible";
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pista)));
        }
        private void Iniciar()
        {
            Random r = new Random();
            NumeroAdivinar = r.Next(1, 5001);
            Intentos = 10;
            Pista = "";
            visibilidad = "Collapsed";
            numerodado = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
