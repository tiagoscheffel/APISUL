using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvaAdmissionalCSharpApisul
{
    class Program
    {
        public enum Periodo { Matutino = 'M', Vespertino = 'V', Noturno = 'N'}

        static void Main(string[] args)
        {
            ElevadorService service = new ElevadorService(Directory.GetCurrentDirectory() + @"\input.json");

            //01 - Cabeçalho e Pergunta A
            WriteLineColor("\n------------------[Sistema de controle dos elevadores]------------------\n\n\n", ConsoleColor.Green);
            WriteLineColor("A) Qual é o andar menos utilizado pelos usuários?\n", ConsoleColor.Yellow);

            foreach (int andar in service.andarMenosUtilizado())
                WriteLineColor(andar + "º andar", ConsoleColor.White);

            //02 - Pergunta B
            WriteLineColor("\nB) Qual é o elevador mais frequentado e o período que se encontra maior fluxo?\n", ConsoleColor.Yellow);

            foreach (char elevador in service.elevadorMaisFrequentado())
                WriteLineColor("Elevador:" + elevador, ConsoleColor.White);

            foreach (char periodo in service.periodoMaiorFluxoElevadorMaisFrequentado())
                WriteLineColor("Periodo:" + (Periodo)periodo, ConsoleColor.White);

            //03 - Pergunta C
            WriteLineColor("\nC) Qual é o elevador menos frequentado e o período que se encontra menor fluxo?\n", ConsoleColor.Yellow);

            foreach (char elevador in service.elevadorMenosFrequentado())
                WriteLineColor("Elevador:" + elevador, ConsoleColor.White);
            
            foreach (char periodo in service.periodoMenorFluxoElevadorMenosFrequentado())
                WriteLineColor("Periodo:" + (Periodo)periodo, ConsoleColor.White);

            //04 - Pergunta D
            WriteLineColor("\nD) Qual o período de maior utilização do conjunto de elevadores?\n", ConsoleColor.Yellow);

            foreach (char periodo in service.periodoMaiorUtilizacaoConjuntoElevadores())
                WriteLineColor("Periodo:" + (Periodo)periodo, ConsoleColor.White);

            //04 - Pergunta E
            WriteLineColor("\nE) Qual o percentual de uso de cada elevador com relação a todos os serviços prestados?\n", ConsoleColor.Yellow);

            WriteLineColor("Elevador A: " + service.percentualDeUsoElevadorA().ToString() + "%", ConsoleColor.White);
            WriteLineColor("Elevador B: " + service.percentualDeUsoElevadorB().ToString() + "%", ConsoleColor.White);
            WriteLineColor("Elevador C: " + service.percentualDeUsoElevadorC().ToString() + "%", ConsoleColor.White);
            WriteLineColor("Elevador D: " + service.percentualDeUsoElevadorD().ToString() + "%", ConsoleColor.White);
            WriteLineColor("Elevador E: " + service.percentualDeUsoElevadorE().ToString() + "%", ConsoleColor.White);

            Console.ReadKey();

            /// <summary>
            /// WriteLineColor
            /// </summary>
            /// <param name="value"></param>
            void WriteLineColor(string value, ConsoleColor Color)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
                Console.ResetColor();
            }
        }
    }
}
