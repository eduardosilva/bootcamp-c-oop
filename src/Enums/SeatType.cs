using System.ComponentModel;

namespace Exemplo.Enums
{
    public enum SeatType
    {
        [Description("Assento normal")]
        Regular = 1,

        [Description("Sofá lado esquerdo")]
        SofaLeft = 2,

        [Description("Sofá lado direito")]
        SofaRight = 3,

        [Description("Assento para acompanhante")]
        Companion = 4,

        [Description("Assento com movimento")]
        Motion = 5,

        [Description("Assento VIP")]
        VIP = 6,

        [Description("Assento para deficientes")]
        Handicap = 7,

        [Description("Assento para mobilidade reduzida")]
        ReducedMobility = 8,

        [Description("Assento para obesos")]
        Obese = 9
    }
}