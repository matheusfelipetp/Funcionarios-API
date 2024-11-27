using System.Text.Json.Serialization;

namespace WebApi_CRUD.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartamentoEnum
    {
        TI,
        RH,
        Financeiro,
        Comercial,
        Atendimento
    }
}
