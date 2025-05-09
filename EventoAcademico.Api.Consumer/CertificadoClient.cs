using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventoAcademico.Modelos;
using Newtonsoft.Json;

namespace EventoAcademico.Api.Consumer
{
    public static class CertificadoClient
    {
        private static readonly HttpClient _http = new HttpClient();

        /// <summary>
        /// Llama al endpoint POST /api/Certificados/emitir/{inscripcionId}
        /// </summary>
        public static Certificado Emitir(string baseUrl, int inscripcionId, Certificado dto)
        {
            // Construye la URL completa
            var url = $"{baseUrl.TrimEnd('/')}/emitir/{inscripcionId}";
            // Serializa el body
            var content = new StringContent(
                JsonConvert.SerializeObject(dto),
                Encoding.UTF8,
                "application/json"
            );
            // Ejecuta la llamada
            var resp = _http.PostAsync(url, content).Result;
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Error al emitir: {resp.StatusCode}");

            // Deserializa la respuesta
            return JsonConvert
                .DeserializeObject<Certificado>(
                    resp.Content.ReadAsStringAsync().Result
                )!;
        }
    }

}
