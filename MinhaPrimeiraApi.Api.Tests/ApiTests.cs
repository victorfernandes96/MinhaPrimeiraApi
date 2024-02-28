using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Api.Controllers;
using MinhaPrimeiraApi.Api.Tests.Helper;
using MinhaPrimeiraApi.Servicos.Dto;
using MinhaPrimeiraApi.Servicos.Interfaces;
using MongoDB.Bson;
using Moq;
using System.Net;
using Xunit;

namespace MinhaPrimeiraApi.Api.Tests
{
    public class ApiTests
    {
        private readonly Mock<IProdutoServico> _produtoServicoMock;
        private readonly ProdutosController _controller;

        public ApiTests()
        {
            _produtoServicoMock = new Mock<IProdutoServico>();

            _controller = new ProdutosController(_produtoServicoMock.Object);
        }

        [Fact]
        public void ObterTodosProdutos_ReturnsExpectedStatusCode()
        {
            var actionResult = _controller.ObterTodosProdutos().Result;

            actionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void ObterTodosProdutos_ReturnsExpectedRoute()
        {
            const string expectedRoute = "[controller]/";

            var route = RouteHelperController.GetRoute<ProdutosController>(nameof(ProdutosController.ObterTodosProdutos));

            route.Should().Be(expectedRoute);
        }

        [Fact]
        public void ObterTodosProdutos_VerifyServiceWasCalled()
        {
            _ = _controller.ObterTodosProdutos().Result;

            _produtoServicoMock.Verify(t => t.ObterTodosProdutos(), Times.Once, "ObterTodosProdutos não foi invocado !");
        }

        [Fact]
        public void ObterProdutoPorId_ReturnsExpectedStatusCode()
        {
            var actionResult = _controller.ObterProdutoPorId("614217378f64fbf51ed1d5cc").Result;

            actionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void ObterProdutoPorId_ReturnsExpectedRoute()
        {
            const string expectedRoute = "[controller]/id";

            var route = RouteHelperController.GetRoute<ProdutosController>(nameof(ProdutosController.ObterProdutoPorId));

            route.Should().Be(expectedRoute);
        }

        [Fact]
        public void ObterProdutoPorId_VerifyServiceWasCalled()
        {
            _ = _controller.ObterProdutoPorId("614217378f64fbf51ed1d5cc").Result;

            _produtoServicoMock.Verify(t => t.ObterProdutoPorId(ObjectId.Parse("614217378f64fbf51ed1d5cc")), Times.Once, "ObterProdutoPorId não foi invocado !");
        }

        [Fact]
        public void SalvarProduto_ReturnsExpectedStatusCode()
        {
            var actionResult = _controller.SalvarProduto(new ProdutoInserirDto()).Result;

            actionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void SalvarProduto_ReturnsExpectedRoute()
        {
            const string expectedRoute = "[controller]/";

            var route = RouteHelperController.GetRoute<ProdutosController>(nameof(ProdutosController.SalvarProduto));

            route.Should().Be(expectedRoute);
        }

        [Fact]
        public void SalvarProduto_VerifyServiceWasCalled()
        {
            _ = _controller.SalvarProduto(It.IsAny<ProdutoInserirDto>()).Result;

            _produtoServicoMock.Verify(t => t.SalvarProduto(It.IsAny<ProdutoInserirDto>()), Times.Once, "SalvarProduto não foi invocado !");
        }

        [Fact]
        public void AlterarProduto_ReturnsExpectedStatusCode()
        {
            var actionResult = _controller.AlterarProduto(new ProdutoDto()).Result;

            actionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void AlterarProduto_ReturnsExpectedRoute()
        {
            const string expectedRoute = "[controller]/";

            var route = RouteHelperController.GetRoute<ProdutosController>(nameof(ProdutosController.AlterarProduto));

            route.Should().Be(expectedRoute);
        }

        [Fact]
        public void AlterarProduto_VerifyServiceWasCalled()
        {
            _ = _controller.AlterarProduto(It.IsAny<ProdutoDto>()).Result;

            _produtoServicoMock.Verify(t => t.AlterarProduto(It.IsAny<ProdutoDto>()), Times.Once, "AlterarProduto não foi invocado !");
        }

        [Fact]
        public void DeletarProdutoPorId_ReturnsExpectedStatusCode()
        {
            var actionResult = _controller.DeletarProdutoPorId("614217378f64fbf51ed1d5cc").Result;

            actionResult.Should().BeOfType<OkResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void DeletarProdutoPorId_ReturnsExpectedRoute()
        {
            const string expectedRoute = "[controller]/id";

            var route = RouteHelperController.GetRoute<ProdutosController>(nameof(ProdutosController.DeletarProdutoPorId));

            route.Should().Be(expectedRoute);
        }

        [Fact]
        public void DeletarProdutoPorId_VerifyServiceWasCalled()
        {
            _ = _controller.DeletarProdutoPorId("614217378f64fbf51ed1d5cc").Result;

            _produtoServicoMock.Verify(t => t.DeletarProdutoPorId(ObjectId.Parse("614217378f64fbf51ed1d5cc")), Times.Once, "DeletarProdutoPorId não foi invocado !");
        }
    }
}