using Fablab.Models.Domain;
using Fablab.Models.DTO;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Fablab.Models.DTO.TagDTO;
using Fablab.Models.DTO.PictureDTO;
using Fablab.Models.DTO.SpecificationDTO;
using Fablab.Models.DTO.ProjectFolder;

namespace Fablab.Helpers.Mapper
{
    public class MappingConf: Profile
	{
		public MappingConf() 
		{
			CreateMap<Borrow, PostBorrowDTO>().ReverseMap();


			CreateMap<Project, SearchProjectDTO>().ReverseMap();
			CreateMap<Project, ProjectDTO>().ReverseMap();
			CreateMap<Project, PostProjectDTO>().ReverseMap();
			CreateMap<Project, ApproveProject>().ReverseMap();


			CreateMap<Supplier, SupplierDTO>().ReverseMap();
			CreateMap<Location, LocationDTO>().ReverseMap();
			CreateMap<EquipmentType, EquipmentTypeDTO2>().ReverseMap();
			CreateMap<Equipment, EquipmentDTO>().ReverseMap();
			CreateMap<Equipment, PostEquipmentDTO>().ReverseMap();
			CreateMap<Equipment, PutEquipmentDTO>().ReverseMap();
			CreateMap<Equipment, SearchEquipmentDTO>().ReverseMap();

			CreateMap<Borrow, GetBorrowDTO>().ReverseMap();
			CreateMap<Borrow, PutBorrowDTO>().ReverseMap();
			CreateMap<Borrow, BorrowDTO>().ReverseMap();

			CreateMap<Tag, TagDTO>().ReverseMap();
			CreateMap<Picture, PictureDTO>().ReverseMap();
			CreateMap<Specification, SpecificationDTO>().ReverseMap();
		}

	}
}
