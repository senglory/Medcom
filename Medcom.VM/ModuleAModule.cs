using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Medcom.VM.DTO;
using AutoMapper;
using BLL;


namespace Medcom.VM
{
    public class ModuleAModule
    {
        IUnityContainer _container;

        static readonly MapperConfiguration config;
        static readonly IMapper mapper;

        public static  IMapper TheMapper { get { return mapper; } }

        static ModuleAModule()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseObj, BaseObjDto>();
                cfg.CreateMap<BaseObjDto, BaseObj>();
                cfg.CreateMap<Note, NoteDto>();
                cfg.CreateMap<NoteDto, Note>();
                cfg.CreateMap<CreditCard, CreditCardDto>();
                cfg.CreateMap<CreditCardDto, CreditCard>();
                cfg.CreateMap<WebAcc, WebAccDto>();
                cfg.CreateMap<WebAccDto, WebAcc>();
                cfg.IgnoreUnmapped();
            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
    }
}
