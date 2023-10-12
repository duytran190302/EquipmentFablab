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
			CreateMap<Equipment, EquipmentDTO>().ReverseMap();
			CreateMap<EquipmentType, EquipmentTypeDTO>().ReverseMap();
			CreateMap<Location,LocationDTO>().ReverseMap();
			CreateMap<Project, ProjectDTO>().ReverseMap();
			CreateMap<Supplier, SupplierDTO>().ReverseMap();
			

		}

	}
}
