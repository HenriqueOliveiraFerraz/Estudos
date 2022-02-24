using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExercicioOrdenacao;
using System;
using Exercicios;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace TestesExercicios
{
    [TestClass]
    public class TesteOrdenador
    {
        [TestMethod]
        public void TestarOrdenacaoPalavra()
        {
            string letrasPermitidas = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";
            var random = new Random();
            var ordenador = new Ordenador(letrasPermitidas, random.Next(15, 30));
            bool caseSensitive = true;
            var tipoOrdem = TipoOrdenacaoEnum.Ascendente;
            Tuple<string, double> resultadoEsperado = new Tuple<string, double>(string.Empty, 0.00);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($@"--------------------{Environment.NewLine}");
                Console.WriteLine($@"Teste n�mero: {i + 1}");

                if (i > 12 && i <= 24)
                {
                    caseSensitive = false;
                }
                else if (i > 24 && i <= 35)
                {
                    caseSensitive = true;
                    tipoOrdem = TipoOrdenacaoEnum.Descendente;
                }
                else if (i > 35 && i <= 49)
                {
                    caseSensitive = false;
                }

                Console.WriteLine
                (
                @$"Palavra a ser ordenada: {ordenador.PalavraParaOrdenar}{Environment.NewLine}Tipo da ordena��o: {tipoOrdem}."
                );

                Console.WriteLine(string.Format("{0}", caseSensitive ? "Mai�sculo e min�sculo considerado na ordena��o." : "Mai�sculo e min�sculo n�o considerado na ordena��o."));

                double tempoOrdenacao = ExecutarOrdenacaoClasse(ordenador, tipoOrdem, caseSensitive);

                Console.WriteLine("Tempo da ordena��o: {0:F3}", tempoOrdenacao);
                Console.WriteLine($@"Resultado: {ordenador.PalavraOrdenada}{Environment.NewLine}");

                try
                {
                    resultadoEsperado = ObterOrdemEsperada(ordenador.PalavraParaOrdenar, caseSensitive, tipoOrdem);
                    ordenador.CompararComResultado(resultadoEsperado.Item1);

                    Console.WriteLine("Tempo da ordena��o esperada: {0:F3}", resultadoEsperado.Item2);
                    Console.WriteLine($@"Resultado esperado: {ordenador.PalavraOrdenada}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    StringAssert.Contains(ex.Message, "Erro na ordena��o");
                    Assert.Fail(string.Format
                        (
                        "{5}" +
                        "Palavra a ser ordenada: {0}.{5}" +
                        "Tipo ordena��o: {1}.{5}" +
                        "{2}{5}" +
                        "Resultado esperado: {3}.{5}" +
                        "Resultado obtido: {4}.{5}",
                        ordenador.PalavraParaOrdenar, 
                        tipoOrdem,
                        caseSensitive ? "Mai�sculo e min�sculo considerado na ordena��o." : "Mai�sculo e min�sculo n�o considerado na ordena��o.", 
                        resultadoEsperado.Item1,
                        ordenador.PalavraOrdenada,
                        Environment.NewLine
                        ));

                    return;
                }
            }
        }

        public Tuple<string, double> ObterOrdemEsperada(string palavraParaOrdenar, bool caseSensitive, TipoOrdenacaoEnum tipoOrdenacao)
        {
            Stopwatch watch = new Stopwatch();
            double elapsedTime;
            var charArray = palavraParaOrdenar.ToCharArray();

            Comparison<char> compararCaseInsensitive = 
                (x, y) => tipoOrdenacao == TipoOrdenacaoEnum.Ascendente ? 
                    ObterRegraCaseInsensitive(x, y) : ObterRegraCaseInsensitive(y, x);

            Comparison<char> compararCaseSensitive = 
                (x, y) => tipoOrdenacao == TipoOrdenacaoEnum.Ascendente ?
                    x - y : y - x;

            watch.Reset();
            watch.Start();

            Array.Sort(charArray, caseSensitive ? compararCaseSensitive : compararCaseInsensitive);

            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000.0;

            return new Tuple<string, double>(new string(charArray), elapsedTime);
        }

        public int ObterRegraCaseInsensitive(char charUmOriginal, char charDoisOriginal)
        {
            char charUm = char.ToLower(charUmOriginal);
            char charDois = char.ToLower(charDoisOriginal);
            int valor = charUm - charDois;

            //Ordena letra mai�scula primeiro
            if (valor == 0 && charUmOriginal > charDoisOriginal)
                valor = 1;
            else if (valor == 0 && charUmOriginal < charDoisOriginal)
                valor = -1;

            return valor;
        }

        public double ExecutarOrdenacaoClasse(Ordenador ordenador, TipoOrdenacaoEnum tipoOrdem, bool caseSensitive)
        {
            Stopwatch watch = new Stopwatch();
            double elapsedTime;

            watch.Reset();
            watch.Start();

            ordenador.OrdenarPalavra(ordenador.PalavraParaOrdenar, tipoOrdem, caseSensitive);

            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds / 1000.0;

            return elapsedTime;
        }
    }
}
