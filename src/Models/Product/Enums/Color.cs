using System.ComponentModel;

namespace Exemplo.Models.Products.Enums {
    public enum Color {
        [Description("Preto")]
        Black = 0,

        [Description("Branco")]
        White = 1,

        [Description("Vermelho")]
        Red = 2,

        [Description("Limão")]
        Lime = 3,

        [Description("Azul")]
        Blue = 4,

        [Description("Amarelo")]
        Yellow = 5,

        [Description("Ciano")]
        Cyan = 6,

        [Description("Fúcsia")]
        Magenta = 7,

        [Description("Prata")]
        Silver = 8,

        [Description("Cinza")]
        Gray = 9,

        [Description("Castanho")]
        Maroon = 10,

        [Description("Oliva")]
        Olive = 11,

        [Description("Verde")]
        Green = 12,

        [Description("Roxo")]
        Purple = 13,

        [Description("Verde-petróleo")]
        Teal = 14,

        [Description("Azul-marinho")]
        Navy = 15
    }
}