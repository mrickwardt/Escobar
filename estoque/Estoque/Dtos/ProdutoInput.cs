using Estoque.Dtos.Enums;

namespace Estoque.Dtos
{
    public class ProdutoInput
    {
        public string Nome{ get; set; }
        public int Quantidade { get; set; }
        public TipoProduto Tipo { get; set; }
        public double ValorBase { get; set; }
    }
}
