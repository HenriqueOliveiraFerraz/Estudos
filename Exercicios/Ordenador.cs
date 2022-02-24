using Exercicios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExercicioOrdenacao
{
    /// <summary>Classe para testar algoritmos de ordenação.</summary>
    public class Ordenador
    {
        private const string LetrasASC = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
        private const string LetrasDSC = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

        public Random Random { get; private set; } = new Random();
        public int TamanhoPalavra { get; private set; }
        public string LetrasPermitidas { get; private set; }
        public string PalavraParaOrdenar { get; private set; }
        public string PalavraOrdenada { get; private set; }

        public Ordenador()
        {
            GerarPalavraAleatoria(LetrasASC, Random.Next(5, 15));
        }

        public Ordenador(string letrasPermitidas, int tamanhoPalavra)
        {
            LetrasPermitidas = letrasPermitidas;
            TamanhoPalavra = tamanhoPalavra;
            GerarPalavraAleatoria();
        }

        public void OrdenarPalavra(string palavra, TipoOrdenacaoEnum tipoOrdenacao, bool caseSensitive)
        {
            var regex = new Regex("[^a-zA-Z]");
            palavra = regex.Replace(palavra, string.Empty);

            char[] caracteres = palavra.ToCharArray();
            var lista = Array.ConvertAll(caracteres, c => (int)c);

            OrdenarPorInsercao(caracteres, tipoOrdenacao);

            if (!caseSensitive)
                PalavraOrdenada = AjustarParaMaiusculoMinusculo(caracteres, tipoOrdenacao);
            else
                PalavraOrdenada = new string(caracteres);
        }

        private void OrdenarPorInsercao(char[] caracteres, TipoOrdenacaoEnum tipoOrdenacao)
        {
            int tamanhoArray = caracteres.Length;

            for (int index = 0; index < tamanhoArray; index++)
            {
                char valorPosicaoUm = caracteres[index];
                int indexAtual = index;

                switch (tipoOrdenacao)
                {
                    case TipoOrdenacaoEnum.Ascendente:
                        while (indexAtual > 0 && caracteres[indexAtual - 1] > valorPosicaoUm)
                        {
                            caracteres.TrocarCaracter(indexAtual, indexAtual - 1);
                            indexAtual--;
                        }
                        break;
                    case TipoOrdenacaoEnum.Descendente:
                        while (indexAtual > 0 && caracteres[indexAtual - 1] < valorPosicaoUm)
                        {
                            caracteres.TrocarCaracter(indexAtual, indexAtual - 1);
                            indexAtual--;
                        }
                        break;
                }

                caracteres[indexAtual] = valorPosicaoUm;
            }
        }

        public void OrdenacaoRapida(int[] data)
        {
            OrdenacaoRapida(data, 0, data.Length - 1);
        }

        private void OrdenacaoRapida(int[] data, int l, int r)
        {
            int i, j;
            int x;

            i = l;
            j = r;

            x = data[(l + r) / 2];
            while (true)
            {
                while (data[i] < x)
                    i++;
                while (x < data[j])
                    j--;
                if (i <= j)
                {
                    data.TrocarInteiro(i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }
            if (l < j)
                OrdenacaoRapida(data, l, j);
            if (i < r)
                OrdenacaoRapida(data, i, r);
        }

        private string AjustarParaMaiusculoMinusculo(char[] caracteres, TipoOrdenacaoEnum tipoOrdenacao)
        {
            string resultado = string.Empty;

            if (tipoOrdenacao == TipoOrdenacaoEnum.Ascendente)
            {
                for (int i = 0; i < LetrasASC.Length; i++)
                {
                    char letraAtual = LetrasASC[i];
                    var chars = caracteres.Where(w => w == letraAtual);

                    if (chars.Any())
                        resultado += string.Join("", chars);
                }
            }
            else
            {
                for (int i = LetrasDSC.Length - 1; i >= 0; i--)
                {
                    char letraAtual = LetrasDSC[i];
                    var chars = caracteres.Where(w => w == letraAtual);

                    if (chars.Any())
                        resultado += string.Join("", chars);
                }
            }


            return resultado;
        }

        private void GerarPalavraAleatoria(string letras = null, int? tamanhoPalavra = null)
        {
            PalavraParaOrdenar = new string(Enumerable.Repeat(string.IsNullOrEmpty(letras) ? LetrasPermitidas : letras, tamanhoPalavra == null ? TamanhoPalavra : tamanhoPalavra.Value)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public string ObterPalavraAleatoria(string letras = null, int? tamanhoPalavra = null)
        {
            return new string(Enumerable.Repeat(string.IsNullOrEmpty(letras) ? LetrasPermitidas : letras, tamanhoPalavra == null ? TamanhoPalavra : tamanhoPalavra.Value)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public void AlterarResultadoParaErro()
        {
            PalavraOrdenada += "qwoenNAISDQW";
        }

        /// <summary>
        /// Compara o resultado esperado com o obtido.
        /// </summary>
        public void CompararComResultado(string resultadoEsperado)
        {
            if (resultadoEsperado != PalavraOrdenada)
                throw new Exception("Erro na ordenação");
        }
    }
}
