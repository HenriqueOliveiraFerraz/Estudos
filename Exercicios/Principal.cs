using ExercicioOrdenacao;
using System;

namespace Exercicios
{
    public class Principal
    {
        static void Main(string[] args)
        {
            string letras = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            var random = new Random();
            var ordenador = new Ordenador(letras, random.Next(15, 20));
            bool caseSensitive = false;
            var tipoOrdem = TipoOrdenacaoEnum.Ascendente;
            ordenador.OrdenarPalavra(ordenador.PalavraParaOrdenar, tipoOrdem, caseSensitive);
        }
    }
}
