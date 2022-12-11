using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.ViewModels;

namespace DesafioAutoGlass.APPLICATION.Configuration
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<Fornecedor, FornecedorViewModel>()
                // .ForMember(dest => dest.Produtos, config => config.MapFrom(src => src.Produto))
                .ForMember(dest => dest.DescricaoFornecedor, config => config.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Cnpj, config => config.MapFrom(src => src.CNPJ))
                .ForMember(dest => dest.Status, config => config.MapFrom(src => src.Status))
                .ReverseMap();

            CreateMap<PaginationResult<Fornecedor>, PaginationResult<FornecedorViewModel>>()
                .ForMember(dest => dest.Data, config => config.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.Fornecedor, config => config.MapFrom(src => src.Fornecedor))
                .ForMember(dest => dest.StatusProduto, config => config.MapFrom(src => src.Status))
                .ForMember(dest => dest.DataFabricacao, config => config.MapFrom(src => src.DataFabricacao))
                .ForMember(dest => dest.DataVencimento, config => config.MapFrom(src => src.DataValidade))
                .ReverseMap();
            
            CreateMap<PaginationResult<Produto>, PaginationResult<ProdutoViewModel>>()
                .ForMember(dest => dest.Data, config => config.MapFrom(src => src.Data))
                .ReverseMap();
        }
    }
}