using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    public static class ExtensaoInteiro
    {
        /// <summary>
        /// Troca o valor de um inteiro na posição <paramref name="posicaoUm"/>
        /// por outro na posição <paramref name="posicaoDois"/> da lista de
        /// <paramref name="inteiros"/>.
        /// </summary>
        public static void TrocarInteiro(this int[] inteiros, int posicaoUm, int posicaoDois)
        {
            int temporario;

            temporario = inteiros[posicaoUm];
            inteiros[posicaoUm] = inteiros[posicaoDois];
            inteiros[posicaoDois] = temporario;
        }
    }
}
