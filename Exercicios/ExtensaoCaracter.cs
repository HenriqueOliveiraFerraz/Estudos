using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    public static class ExtensaoCaracter
    {
        /// <summary>
        /// Troca o valor de um caracter na posição <paramref name="posicaoUm"/>
        /// por outro na posição <paramref name="posicaoDois"/> da lista de
        /// <paramref name="caracteres"/>.
        /// </summary>
        public static void TrocarCaracter(this char[] caracteres, int posicaoUm, int posicaoDois)
        {
            char temporario;

            temporario = caracteres[posicaoUm];
            caracteres[posicaoUm] = caracteres[posicaoDois];
            caracteres[posicaoDois] = temporario;
        }
    }
}
