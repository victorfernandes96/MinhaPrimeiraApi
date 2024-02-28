using AutoMapper;
using FluentAssertions;
using MinhaPrimeiraApi.Entidades.Entidades;
using MinhaPrimeiraApi.Repositorios.Interfaces;
using MinhaPrimeiraApi.Repositorios.UoW;
using MinhaPrimeiraApi.Servicos.Dto;
using MinhaPrimeiraApi.Servicos.Servicos;
using MongoDB.Bson;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MinhaPrimeiraApi.Servico.Tests
{
    public class ProdutoServicoTest
    {
        private readonly Mock<IProdutoRepositorio> _repositorio;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapper;

        private readonly ProdutoServico _servico;

        public ProdutoServicoTest()
        {
            _repositorio = new Mock<IProdutoRepositorio>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();

            _servico = new ProdutoServico(_repositorio.Object, _unitOfWork.Object, _mapper.Object);
        }

        [Fact]
        public void GetAllAsync_ReturnsRespectively()
        {
            var produtos = new List<Produto>
            {
                new Produto { Descricao= "teste", Nome = "laranja", Preco = 50},
                new Produto { Descricao= "teste2", Nome = "abacaxi", Preco = 60}
            };

            var produtosDto = new List<ProdutoDto>
            {
                new ProdutoDto { Descricao= "teste", Nome = "laranja", Preco = 50},
                new ProdutoDto { Descricao= "teste2", Nome = "abacaxi", Preco = 60}
            };

            _repositorio.Setup(c => c.GetAllAsync())
                .ReturnsAsync(produtos);

            _mapper.Setup(c => c.Map<IEnumerable<ProdutoDto>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns(produtosDto);

            var result = _servico.ObterTodosProdutos().Result;

            result.Should().SatisfyRespectively(
                first =>
                {
                    first.Descricao.Should().Be("teste");
                },
                second =>
                {
                    second.Descricao.Should().Be("teste2");
                });
        }

        [Fact]
        public void GetByIdAsync_ReturnsRespectively()
        {
            var produtoDto = new List<ProdutoDto>
            {
                new ProdutoDto { Descricao= "teste", Nome = "laranja", Preco = 50},
                new ProdutoDto { Descricao= "teste2", Nome = "abacaxi", Preco = 60}
            };

            _repositorio.Setup(c => c.GetByIdAsync(It.IsAny<ObjectId>()))
                .ReturnsAsync(new Produto { Descricao = "teste", Nome = "laranja", Preco = 50 });

            _mapper.Setup(c => c.Map<ProdutoDto>(It.IsAny<Produto>()))
                .Returns(new ProdutoDto { Descricao = "teste", Nome = "laranja", Preco = 50 });

            var result = _servico.ObterProdutoPorId(It.IsAny<ObjectId>()).Result;

            result.Descricao.Should().Be("teste");
            result.Preco.Should().Be(50);
        }

        [Fact]
        public void SalvarProduto_VerifyWasCalled()
        {
            _repositorio.Setup(c => c.AddAsync(It.IsAny<Produto>()));

            _mapper.Setup(c => c.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(new Produto { Descricao = "teste", Nome = "laranja", Preco = 50 });

            var result = _servico.SalvarProduto(It.IsAny<ProdutoInserirDto>()).Result;

            _repositorio.Verify(t => t.AddAsync(It.IsAny<Produto>()), Times.Once, "AddAsync não foi invocado !");
            _unitOfWork.Verify(t => t.CommitAsync(), Times.Once, "CommitAsync não foi invocado !");
        }

        [Fact]
        public void AlterarProduto_VerifyWasCalled()
        {
            _repositorio.Setup(c => c.UpdateAsync(It.IsAny<Produto>()));

            _mapper.Setup(c => c.Map<Produto>(It.IsAny<ProdutoDto>()))
                .Returns(new Produto { Descricao = "teste", Nome = "laranja", Preco = 50 });

            _repositorio.Setup(c => c.GetByIdAsync(It.IsAny<ObjectId>()))
                .ReturnsAsync(new Produto { Descricao = "teste", Nome = "laranja", Preco = 50 });

            var result = _servico.AlterarProduto(It.IsAny<ProdutoDto>()).Result;

            _repositorio.Verify(t => t.UpdateAsync(It.IsAny<Produto>()), Times.Once, "UpdateAsync não foi invocado !");
            _repositorio.Verify(t => t.GetByIdAsync(It.IsAny<ObjectId>()), Times.Once, "GetByIdAsync não foi invocado !");
            _unitOfWork.Verify(t => t.CommitAsync(), Times.Once, "CommitAsync não foi invocado !");
        }

        [Fact]
        public void DeletarProdutoPorId_VerifyWasCalled()
        {
            _repositorio.Setup(c => c.DeleteAsync(It.IsAny<ObjectId>()));

            _ = _servico.DeletarProdutoPorId(It.IsAny<ObjectId>());

            _repositorio.Verify(t => t.DeleteAsync(It.IsAny<ObjectId>()), Times.Once, "DeleteAsync não foi invocado !");
            _unitOfWork.Verify(t => t.CommitAsync(), Times.Once, "CommitAsync não foi invocado !");
        }
    }
}