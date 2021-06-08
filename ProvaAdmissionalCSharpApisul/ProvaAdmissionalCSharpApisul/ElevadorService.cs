using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAdmissionalCSharpApisul
{
    public class ElevadorService : IElevadorService
    {
        private List<RegistroModel> ListaRegistros = new List<RegistroModel>();
        public enum Elevador { A, B, C, D, E  }

        /// <summary>
        /// ElevadorService
        /// </summary>
        public ElevadorService(string arquivo)
        {
            if(File.Exists(arquivo))
            {
                this.ListaRegistros = JsonConvert.DeserializeObject<List<RegistroModel>>(System.IO.File.ReadAllText(arquivo));
            }
        }

        /// <summary>
        /// andarMenosUtilizado
        /// </summary>
        /// <returns></returns>
        public List<int> andarMenosUtilizado()
        {
            var group = this.ListaRegistros.GroupBy(t => t.Andar).Select(t => new{ Andar = t.Key, Count = t.Count()}).ToList();
            return group.Where(item => item.Count <= group.Min(x => x.Count)).Select(t => t.Andar).ToList<int>();
        }

        /// <summary>
        /// elevadorMaisFrequentado
        /// </summary>
        /// <returns></returns>
        public List<char> elevadorMaisFrequentado()
        {
            var group = this.ListaRegistros.GroupBy(t => t.Elevador).Select(t => new { Elevador = t.Key, Count = t.Count() }).ToList();
            return group.Where(item => item.Count >= group.Max(x => x.Count)).Select(t => t.Elevador[0]).ToList<char>();
        }

        /// <summary>
        /// elevadorMenosFrequentado
        /// </summary>
        /// <returns></returns>
        public List<char> elevadorMenosFrequentado()
        {
            var group = this.ListaRegistros.GroupBy(t => t.Elevador).Select(t => new { Elevador = t.Key, Count = t.Count()}).ToList();
            return group.Where(item => item.Count <= group.Min(x => x.Count)).Select(t => t.Elevador[0]).ToList<char>();
        }

        /// <summary>
        /// percentualDeUsoElevadorA
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorA()
        {
            return percentualDeUsoElevador(Elevador.A);
        }

        /// <summary>
        /// percentualDeUsoElevadorB
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorB()
        {
            return percentualDeUsoElevador(Elevador.B);
        }

        /// <summary>
        /// percentualDeUsoElevadorC
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorC()
        {
            return percentualDeUsoElevador(Elevador.C);
        }

        /// <summary>
        /// percentualDeUsoElevadorD
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorD()
        {
            return percentualDeUsoElevador(Elevador.D);
        }

        /// <summary>
        /// percentualDeUsoElevadorE
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorE()
        {
            return percentualDeUsoElevador(Elevador.E);
        }

        /// <summary>
        /// periodoMaiorFluxoElevadorMaisFrequentado
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            return this.ListaRegistros.GroupBy(t => new { t.Elevador, t.Turno }).Where(a => a.Key.Elevador == elevadorMaisFrequentado().First().ToString()).Select(t => new {t.Key, Count = t.Count()}).OrderByDescending(x => x.Count).Select(t=> t.Key.Turno[0]).ToList<char>();
        }

        /// <summary>
        /// periodoMaiorUtilizacaoConjuntoElevadores
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            return this.ListaRegistros.GroupBy(t => new { t.Turno }).Select(t => new { t.Key, Count = t.Count() }).OrderByDescending(x => x.Count).Select(t => t.Key.Turno[0]).ToList<char>();
        }

        /// <summary>
        /// periodoMenorFluxoElevadorMenosFrequentado
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            return this.ListaRegistros.GroupBy(t => new { t.Elevador, t.Turno }).Where(a => a.Key.Elevador == elevadorMenosFrequentado().First().ToString()).Select(t => new { t.Key, Count = t.Count() }).OrderBy(x => x.Count).Select(t => t.Key.Turno[0]).ToList<char>();
        }


        /// <summary>
        /// percentualDeUsoElevador
        /// </summary>
        /// <param name="elevador"></param>
        /// <returns></returns>
        private float percentualDeUsoElevador(Elevador elevador)
        {
            var group = this.ListaRegistros.GroupBy(t => t.Elevador).Select(t => new { Elevador = t.Key, Count = t.Count() }).ToList();
            return (float)Math.Round(group.Where(item => item.Elevador == elevador.ToString()).Select(item => (item.Count / (double)group.Sum(x => x.Count)) * 100).FirstOrDefault<double>(), 2);
        }
    }
}
