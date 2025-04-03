using LojaVirtualAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtual.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<bool> DeleteProdutoAsync(int id);
    }
}
