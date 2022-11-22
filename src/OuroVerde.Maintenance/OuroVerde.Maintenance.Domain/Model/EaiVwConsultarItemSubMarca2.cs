using OuroVerde.Maintenance.Domain.Core.Domain;

namespace OuroVerde.Maintenance.Domain.Model
{
    public class EaiVwConsultarItemSubMarca2: Entity
    {
        public EaiVwConsultarItemSubMarca2(long partition,
                                           string dataAreaId,
                                           string marcaId,
                                           string marcaNome,
                                           string partNumber,
                                           string tamanho,
                                           long product,
                                           string idSalesForce)
        {

        }

        public long Partition { get; private set; }
        public string DataAreaId { get; private set; }
        public string MarcaId { get; private set; }
        public string MarcaNome { get; private set; }
        public string PartNumber { get; private set; }
        public string Tamanho { get; private set; }
        public long Product { get; private set; }
        public string IdSalesForce { get; private set; }
    }
}
