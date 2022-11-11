using Newtonsoft.Json;

namespace OuroVerde.Maintenance.Application.ViewModel
{
    public class EaiVwConsultarItemSubMarca2ViewModel
    {
        public EaiVwConsultarItemSubMarca2ViewModel()
        {

        }

        [JsonProperty("partiton")]
        public long Partition { get; private set; }

        [JsonProperty("dataAreaId")]
        public string DataAreaId { get; private set; }

        [JsonProperty("marcaId")]
        public string MarcaId { get; private set; }

        [JsonProperty("marcaNome")]
        public string MarcaNome { get; private set; }

        [JsonProperty("partNumber")]
        public string PartNumber { get; private set; }

        [JsonProperty("tamanho")]
        public string Tamanho { get; private set; }

        [JsonProperty("product")]
        public long Product { get; private set; }

        [JsonProperty("idSalesForce")]
        public string IdSalesForce { get; private set; }
    }
}
