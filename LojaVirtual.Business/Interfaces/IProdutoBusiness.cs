using LojaVirtualAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtual.Business
{
    public interface IProdutoBusiness
    {
        Task<List<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(int id, Produto produto);
        Task<bool> DeleteProdutoAsync(int id);
    }
}
