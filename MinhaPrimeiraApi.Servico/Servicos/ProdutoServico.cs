using AutoMapper;
using MinhaPrimeiraApi.Entidades.Entidades;
using MinhaPrimeiraApi.Repositorios.Interfaces;
using MinhaPrimeiraApi.Repositorios.UoW;
using MinhaPrimeiraApi.Servicos.Dto;
using MinhaPrimeiraApi.Servicos.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Servicos.Servicos
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProdutoDto> AlterarProduto(ProdutoDto produto)
        {
            var produtoParaAlterar = _mapper.Map<Produto>(produto);

            var produtoExistente = await _produtoRepositorio.GetByIdAsync(produtoParaAlterar.Id);

            produtoParaAlterar.DataAlteracao = DateTime.Now;
            produtoParaAlterar.DataCriacao = produtoExistente.DataCriacao;

            await _produtoRepositorio.UpdateAsync(produtoParaAlterar);
            await _unitOfWork.CommitAsync();

            return produto;
        }

        public async Task DeletarProdutoPorId(ObjectId id)
        {
            await _produtoRepositorio.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ProdutoDto> ObterProdutoPorId(ObjectId id)
        {
            return _mapper.Map<ProdutoDto>(await _produtoRepositorio.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodosProdutos()
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepositorio.GetAllAsync());
        }

        public async Task<ProdutoInserirDto> SalvarProduto(ProdutoInserirDto novoProduto)
        {
            var produtoParaInserir = _mapper.Map<Produto>(novoProduto);

            produtoParaInserir.DataCriacao = DateTime.Now;
            produtoParaInserir.DataAlteracao = DateTime.Now;

            await _produtoRepositorio.AddAsync(produtoParaInserir);
            await _unitOfWork.CommitAsync();

            return novoProduto;
        }
    }
}