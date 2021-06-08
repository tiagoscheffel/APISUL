using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAdmissionalCSharpApisul
{
    public class RegistroModel
    {
        [JsonProperty("andar")]
        public int Andar { get; set; }

        [JsonProperty("elevador")]
        public string Elevador { get; set; }

        [JsonProperty("turno")]
        public string Turno { get; set; }

        /// <summary>
        /// RegistroModel
        /// </summary>
        /// <param name="Andar"></param>
        /// <param name="Elevador"></param>
        /// <param name="Turno"></param>
        public RegistroModel(int Andar, string Elevador, string Turno)
        {
            this.Andar = Andar;
            this.Elevador = Elevador;
            this.Turno = Turno;
        }
    }
}

   