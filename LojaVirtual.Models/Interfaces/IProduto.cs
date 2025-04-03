using LojaVirtualAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Models.Interfaces
{
    public interface IProduto
    {
        Task<List<Produto>> GetAllProdutosAsync(); // Buscar todos os produtos
        Task<Produto> GetProdutoByIdAsync(int id); // Buscar produto por ID
        Task<Produto> AddProdutoAsync(Produto produto); // Adicionar um novo produto
        Task<Produto> UpdateProdutoAsync(int id, Produto produto); // Atualizar um produto existente
        Task<bool> DeleteProdutoAsync(int id); // Deletar um produto
    }
}