using LojaVirtual.Repository.Context;
using LojaVirtual.Repository.Interfaces;
using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtual.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly LojaContext _context;

        public ProdutoRepository(LojaContext context)
        {
            _context = context;
        }

        // Retorna todos os produtos
        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        // Busca um produto por ID
        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        // Adiciona um novo produto
        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        // Atualiza um produto existente
        public async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            var existingProduto = await _context.Produtos.FindAsync(produto.Id);
            if (existingProduto == null) return null;

            _context.Entry(existingProduto).CurrentValues.SetValues(produto);
            await _context.SaveChangesAsync();
            return existingProduto;
        }

        // Remove um produto
        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
