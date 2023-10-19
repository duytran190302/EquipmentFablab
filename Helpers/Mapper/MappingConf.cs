using Fablab.Models.Domain;
using Fablab.Models.DTO;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace Fablab.Helpers.Mapper
{
	public class MappingConf: Profile
	{
		public MappingConf() 
		{
			CreateMap<Borrow, BorrowDTO>().ReverseMap();



			CreateMap<Project, ProjectDTO>().ReverseMap();
			CreateMap<Project, PostProjectDTO>().ReverseMap();
			CreateMap<Project, ApproveProject>().ReverseMap();


			CreateMap<Supplier, SupplierDTO>().ReverseMap();
			CreateMap<Location, LocationDTO>().ReverseMap();
			CreateMap<EquipmentType, EquipmentTypeDTO>().ReverseMap();
			CreateMap<Equipment, EquipmentDTO>().ReverseMap();
			CreateMap<Equipment, PostEquipmentDTO>().ReverseMap();
			CreateMap<Equipment, PutEquipmentDTO>().ReverseMap();



		}

	}
}
