using System.Runtime.Serialization;

namespace Unidas.MS.Maintenance.Infra.CrossCutting.Shared.Common
{
    public class AppSettings
    {
        public AXConnector AXConnector { get; set; } = new AXConnector();
        public SalesForce SalesForce { get; set; }
        public SmtpServer SmtpServer { get; set; }
        public string ServerUserUrl { get; set; }
        public int Take_ServiceOrder { get; set; }
        public int Take_Attachment { get; set; }
        public int Take_CurrentKm { get; set; }
        public int Take_Jira { get; set; }
        public bool Deploy { get; set; }
        public int Retries_Limit { get; set; }
        public int Retries_Limit_Max { get; set; }
        public string EmailSupport { get; set; }
        public NextFleet NextFleet { get; set; }
        public InfoBip InfoBip { get; set; }

        public string UserApplication { get; set; }
        public JiraCAC JiraCAC { get; set; }
        public string Environment { get; set; }
        public string FacilAssistUsername { get; set; }
        public string FacilAssistPassword { get; set; }
        public string FacilAssistAddressUri { get; set; }
    }

    public class AXConnector
    {
        public string CreateServiceOrderUrl { get; set; }
        public string UpdateServiceOrderUrl { get; set; }
        public string CreateCheckInCheckOutUrl { get; set; }

        public string CreateAttachmentUrl { get; set; }
        public string InsertDeviceUsageUrl { get; set; }
        public string CreateDevicePurchaseIntentionUrl { get; set; }
        public string CreateServiceOrderEprocToAxUrl { get; set; }
        public string CreateAttachmentServiceOrderEprocToAxUrl { get; set; }
        public string CreateOperationalReserveUrl { get; set; }
        public string UpdateServiceInternalOrderUrl { get; set; }
    }

    public class SalesForce
    {
        public string Url { get; set; }
        public string UrlQuery { get; set; }
        public string UrlQueryCase { get; set; }
        public string UrlAppOnRoad { get; set; }
        public string OwnerId { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
        public string Product_RecordTypeId { get; set; }
        public string ComercialService_RecordTypeId { get; set; }
        public string OperacionalService_RecordTypeId { get; set; }
        public string Site_RecordTypeId { get; set; }
        public string UrlCustom { get; internal set; }
        /// <summary>
        /// Utilizado para Pesados Ordem Planejada
        /// </summary>
        public string RecordTypeId_CACSolictacao { get; set; }
        /// <summary>
        /// CAC Filho Pesados
        /// </summary>
        public string RecordTypeId_CACFilho { get; set; }
        public string Product_Tax_RecordTypeId { get; set; }

        public string RecordType_CAC_Filho_Corretiva { get; set; }
        public string RecordType_CAC_Filho_Preventiva { get; set; }
    }

    public class SmtpServer
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SecureSocketOptions { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class NextFleet
    {
        public string Hash { get; set; }
        public string HeaderApi { get; set; }
        public string LineApi { get; set; }
    }

    public class InfoBip
    {
        public string InfoBipAuthorization { get; set; }
        public string InfoBipUrlAPI { get; set; }
    }

    public class JiraCAC
    {
        public string CreateAttendanceDataUrl { get; set; }
    }
}
